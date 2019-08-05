using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;
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
using Microsoft.VisualBasic.CompilerServices;
using System.Threading.Tasks;

namespace Nethereum.TryOnBrowser
{
    //// Based on https://github.com/Suchiman/Runny all credit to him
    public class Compiler

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

        public Compiler(HttpClient client)
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

        public void WhenReady(Func<Task> action)
        {
            if (InitializationTask.Status != TaskStatus.RanToCompletion)
            {
                InitializationTask.ContinueWith(x => action());
            }
            else
            {
                action();
            }
        }

  
        public (bool success, Assembly asm, Byte[] rawAssembly) LoadSource(string source, string language)
        {
            dynamic compilation = new object();
            if(language=="csharp")
            {
                compilation = CSharpCompilation.Create("DynamicCode")
                .WithOptions(new CSharpCompilationOptions(OutputKind.ConsoleApplication))
                .AddReferences(References)
                .AddSyntaxTrees(CSharpSyntaxTree.ParseText(source));
            } else if(language=="vb") {
                compilation = VisualBasicCompilation.Create("DynamicCode")
                .WithOptions(new VisualBasicCompilationOptions(OutputKind.WindowsApplication, embedVbCoreRuntime: true))
                .AddReferences(References)
                .AddSyntaxTrees(VisualBasicSyntaxTree.ParseText(source));
            }

            ImmutableArray<Diagnostic> diagnostics  = compilation.GetDiagnostics();
            var error = false;

            foreach (var diagnostic in diagnostics)
            {
                switch (diagnostic.Severity)
                {
                    case DiagnosticSeverity.Info:
                        //Console.WriteLine(diag.ToString());
                        break;
                    case DiagnosticSeverity.Warning:
                        //Console.WriteLine(diag.ToString());
                        break;
                    case DiagnosticSeverity.Error:
                        error = true;
                        Console.WriteLine(diagnostic.ToString());
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

    }
    
}
