using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Nethereum.TryOnBrowser
{
    //// Based on https://github.com/Suchiman/Runny all credit to him
    public static class Compiler

    {

        class BlazorBoot

        {

            public string main { get; set; }

            public string entryPoint { get; set; }

            public string[] assemblyReferences { get; set; }

            public string[] cssReferences { get; set; }

            public string[] jsReferences { get; set; }

            public bool linkerEnabled { get; set; }

        }


        private static Task InitializationTask;

        private static List<MetadataReference> References;

        public static List<Assembly> Assemblies;

        public static void InitializeMetadataReferences(HttpClient client)
        {

            async Task InitializeInternal()

            {
                var response = await client.GetJsonAsync<BlazorBoot>("_framework/blazor.boot.json");

                var assemblyFiles = await Task.WhenAll(response.assemblyReferences.Where(x => x.EndsWith(".dll")).Select(x => client.GetAsync("_framework/_bin/" + x)));

                var references = new List<MetadataReference>(assemblyFiles.Length);
                var assemblies = new List<Assembly>(assemblyFiles.Length);

                foreach (var assemblyFile in assemblyFiles)
                {

                    using (var stream = await assemblyFile.Content.ReadAsStreamAsync())
                    {
                       byte[] data = new byte[stream.Length];
                       await stream.ReadAsync(data, 0, data.Length);
                       var assemblyInstance = Assembly.Load(data);
                       assemblies.Add(assemblyInstance);
                       var metadataReference = MetadataReference.CreateFromImage(data);
                       references.Add(metadataReference);
                    }

                }

                Assemblies = assemblies;
                References = references;

            }

            InitializationTask = InitializeInternal();

        }

        public static async Task WhenReady(Task action)

        {

            if (InitializationTask.Status != TaskStatus.RanToCompletion)
            {

                await InitializationTask.ContinueWith(x => action);
            }
            else
            {
                await action;
            }
        }

        //attempt to use scripting.. does not resolve assemblies
        public static async Task<object> RuWithScriptingAsync(string source)
        {

            Console.WriteLine("Start scripting");

            using (var interactiveLoader = new InteractiveAssemblyLoader())
            {
                foreach (var assembly in Assemblies)
                {
                    interactiveLoader.RegisterDependency(assembly);
                }

                var script = CSharpScript.Create(source, ScriptOptions.Default.AddReferences(References),
                 null, interactiveLoader);
                script.Compile();
                
                Console.WriteLine("Compiled");
                return script.RunAsync();
            }
        }




        public static (bool success, Assembly asm, Byte[] rawAssembly) LoadSource(string source)

        {

            var compilation = CSharpCompilation.Create("DynamicCode")

                .WithOptions(new CSharpCompilationOptions(OutputKind.ConsoleApplication))

                .AddReferences(References)


                .AddSyntaxTrees(CSharpSyntaxTree.ParseText(source));



            ImmutableArray<Diagnostic> diagnostics = compilation.GetDiagnostics();



            bool error = false;

            foreach (Diagnostic diag in diagnostics)

            {

                switch (diag.Severity)

                {

                    case DiagnosticSeverity.Info:

                        Console.WriteLine(diag.ToString());

                        break;

                    case DiagnosticSeverity.Warning:

                        Console.WriteLine(diag.ToString());

                        break;

                    case DiagnosticSeverity.Error:

                        error = true;

                        Console.WriteLine(diag.ToString());

                        break;

                }

            }

            if (error)

            {

                return (false, null, null);

            }



            using (var outputAssembly = new MemoryStream())

            {

                compilation.Emit(outputAssembly);



                return (true, Assembly.Load(outputAssembly.ToArray()), outputAssembly.ToArray());

            }

        }



        public static string Format(string source)

        {

            var tree = CSharpSyntaxTree.ParseText(source);

            var root = tree.GetRoot();

            var normalized = root.NormalizeWhitespace();

            return normalized.ToString();

        }

    }
    
}
