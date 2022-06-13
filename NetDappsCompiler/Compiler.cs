using System.Collections.Immutable;
using System.Reflection;
using System.Net.Http.Json;
using NetDapps.Compiler.Services;
using NetDapps.Compiler.NetDapps;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis;

namespace NetDapps.Compiler
{
    public class Compiler
    {
        public static Compiler Current { get; private set; }
        private static Task InitializationTask;

        public bool IsInitialised()
        {
            return InitializationTask.Status == TaskStatus.RanToCompletion;
        }

        public Compiler(HttpClient client)
        {
            Current = this;

            async Task InitializeInternal()

            {
                try
                {
                    var response = await client.GetFromJsonAsync<AssemblyLoadInfo[]>("assemblies.json");
                    ////await AssemblyCache.Current.LoadAssemblies(client, response);
                    await Task.WhenAll(response.Select(x => AssemblyCache.Current.LoadAssembly(client, x)));

                    //AssemblyCache.Current.InitialiseCurrentAssembliesFromMemory();

                    await AssemblyCache.Current.LoadAssembly(client,
                       new AssemblyLoadInfo(null, "remlib/Microsoft.CodeAnalysis.CSharp.Features.dll"));

                    //await AssemblyCache.Current.LoadAssembly(client,
                    //    new AssemblyLoadInfo(null, "remlib/Wonka.BizRulesEngine.dll"));

                    //await AssemblyCache.Current.LoadAssembly(client,
                    //    new AssemblyLoadInfo(null, "remlib/Wonka.Eth.dll"));

                    //await AssemblyCache.Current.LoadAssembly(client,
                    //    new AssemblyLoadInfo(null, "remlib/Wonka.Product.dll"));

                    //await AssemblyCache.Current.LoadAssembly(client,
                    //    new AssemblyLoadInfo(null, "remlib/Wonka.MetaData.dll"));

                    //await AssemblyCache.Current.LoadAssembly(client,
                    //    new AssemblyLoadInfo(null, "remlib/Nethereum.StandardTokenEIP20.dll"));

                    //await AssemblyCache.Current.LoadAssembly(client,
                    //    new AssemblyLoadInfo(null, "remlib/Nethereum.Quorum.dll"));

                    //await AssemblyCache.Current.LoadAssembly(client,
                    //    new AssemblyLoadInfo(null, "remlib/Nethereum.HdWallet.dll"));

                    //await AssemblyCache.Current.LoadAssembly(client,
                    //    new AssemblyLoadInfo(null, "Microsoft.AspNetCore.Components.dll"));

                    //await AssemblyCache.Current.LoadAssembly(client,
                    //    new AssemblyLoadInfo(null, "Microsoft.AspNetCore.Components.Web.dll"));

                    ProjectEditorInitialiser.InitialiseProjectsFirstInit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error:" + ex.Message);
                }
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


        public (bool success, Assembly asm, byte[] rawAssembly) LoadSource(string source, string language)
        {
            dynamic compilation = new object();

            if (language == "csharp")
            {
                compilation = CSharpCompilation.Create("DynamicCode")
                    .WithOptions(new CSharpCompilationOptions(OutputKind.ConsoleApplication))
                    .AddReferences(AssemblyCache.Current.GetAllMetadataReferences())
                    .AddSyntaxTrees(CSharpSyntaxTree.ParseText(source));
            }
            else if (language == "vb")
            {
                compilation = VisualBasicCompilation.Create("DynamicCode")
                    .WithOptions(new VisualBasicCompilationOptions(OutputKind.WindowsApplication,
                        embedVbCoreRuntime: true))
                    .AddReferences(AssemblyCache.Current.GetAllMetadataReferences())
                    .AddSyntaxTrees(VisualBasicSyntaxTree.ParseText(source));
            }

            ImmutableArray<Diagnostic> diagnostics = compilation.GetDiagnostics();
            var error = false;

            foreach (var diagnostic in diagnostics)
            {
                switch (diagnostic.Severity)
                {
                    case DiagnosticSeverity.Info:
                        Console.WriteLine(diagnostic.ToString());
                        break;
                    case DiagnosticSeverity.Warning:
                        Console.WriteLine(diagnostic.ToString());
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

                return (true, null, outputAssembly.ToArray());
            }
        }

    }



}
