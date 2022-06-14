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
using System.CommandLine.Invocation;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.VisualBasic.CompilerServices;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Completion;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Host.Mef;
using Microsoft.CodeAnalysis.QuickInfo;
using Microsoft.CodeAnalysis.Recommendations;
using Microsoft.CodeAnalysis.Text;
using NetDapps.Assemblies;
using Nethereum.Playground.Components.Monaco.MonacoDTOs;
using Nethereum.Playground.Components.Monaco.Services;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Nethereum.Playground
{
    public class PlaygroundAssemblyCacheInitialiser : AssemblyCacheInitialiser
    {
        private readonly HttpClient client;

        public PlaygroundAssemblyCacheInitialiser(HttpClient client) 
        {
            this.client = client;
            InitializationTask = InitialiseCache();
        }

        public override async Task InitialiseCache()
        {
            try
            {
                var response = await client.GetFromJsonAsync<AssemblyLoadInfo[]>("assemblies.json");
                await Task.WhenAll(response.Select(x => AssemblyCache.Current.LoadAssembly(client, x)));

                var response2 = await client.GetFromJsonAsync<AssemblyLoadInfo[]>("core-assemblies.json");
                await Task.WhenAll(response2.Select(x => AssemblyCache.Current.LoadAssembly(client, x)));
                //AssemblyCache.Current.InitialiseCurrentAssembliesFromMemory();

                await AssemblyCache.Current.LoadAssembly(client,
                   new AssemblyLoadInfo(null, "res2/System.Private.CoreLib.dll"));

                //var response2 = await client.GetFromJsonAsync<BlazorBoot>("_framework/blazor.boot.json");

                //foreach (var assKey in response2.resources.assembly.Keys)
                //{
                //    if (assKey.StartsWith("System") || (assKey.StartsWith("Microsoft") && !assKey.StartsWith("Microsoft.Extensions") && !assKey.StartsWith("Microsoft.AspNetCore"))
                //        || assKey.StartsWith("mscorlib") || assKey.StartsWith("netstandard"))
                //    {
                //        await AssemblyCache.Current.LoadInternalAssembly(client,
                //                    new AssemblyLoadInfo(assKey, "res/" + assKey));
                //    }
                //}

                ProjectEditorInitialiser.InitialiseProjectsFirstInit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }
        }


        class BlazorBoot
        {
            public bool cacheBootResources { get; set; }
            public object[] config { get; set; }
            public bool debugBuild { get; set; }
            public string entryAssembly { get; set; }
            public bool linkerEnabled { get; set; }
            public Resources resources { get; set; }
        }

        class Resources
        {
            public Dictionary<string, string> assembly { get; set; }
            public Dictionary<string, string> pdb { get; set; }
            public Dictionary<string, string> runtime { get; set; }
        }
    }


    public class Compiler
    {
        public static Compiler Current { get; private set; }
       
        public Compiler()
        {
            Current = this;
        }


        public (bool success, Assembly asm, Byte[] rawAssembly) LoadSource(string source, string language)
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
                        //Console.WriteLine(diagnostic.ToString());
                        break;
                    case DiagnosticSeverity.Warning:
                       // Console.WriteLine(diagnostic.ToString());
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
