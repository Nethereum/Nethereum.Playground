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

namespace Nethereum.Playground
{
    //// Based on https://github.com/Suchiman/Runny all credit to him
    public class Compiler
    {
        public static Compiler Current { get; private set; }
        private static Task InitializationTask;


        public Compiler(HttpClient client)
        {
            Current = this;

            async Task InitializeInternal()

            {
                try
                {
                    var response = await client.GetJsonAsync<AssemblyLoadInfo[]>("assemblies.json");
                    //await AssemblyCache.Current.LoadAssemblies(client, response);
                    await Task.WhenAll(response.Select(x => AssemblyCache.Current.LoadAssembly(client, x)));

                    //await Task.WhenAll(GetLocalAssembliesLoadInfo().Select(x => AssemblyCache.Current.LoadAssembly(client, x)));

                    await AssemblyCache.Current.LoadAssembly(client,
                       new AssemblyLoadInfo(null, "remlib/Microsoft.CodeAnalysis.CSharp.Features.dll"));

                    await AssemblyCache.Current.LoadAssembly(client,
                        new AssemblyLoadInfo(null, "remlib/Wonka.BizRulesEngine.dll"));

                    await AssemblyCache.Current.LoadAssembly(client,
                        new AssemblyLoadInfo(null, "remlib/Wonka.Eth.dll"));

                    await AssemblyCache.Current.LoadAssembly(client,
                        new AssemblyLoadInfo(null, "remlib/Wonka.Product.dll"));

                    await AssemblyCache.Current.LoadAssembly(client,
                        new AssemblyLoadInfo(null, "remlib/Wonka.MetaData.dll"));

                    await AssemblyCache.Current.LoadAssembly(client,
                        new AssemblyLoadInfo(null, "remlib/Nethereum.StandardTokenEIP20.dll"));

                    await AssemblyCache.Current.LoadAssembly(client,
                        new AssemblyLoadInfo(null, "remlib/Nethereum.Quorum.dll"));

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


        //var assemblyFiles = await Task.WhenAll(response.assemblyReferences.Where(x => x.EndsWith(".dll")).Select(x => client.GetAsync("_framework/_bin/" + x)));
        //var assemblyFiles = await Task.WhenAll(response.assemblyReferences.Where(x => x.EndsWith(".dll")).Select(x => client.GetAsync("_framework/_bin/" + x)));

        //var references = new List<MetadataReference>(assemblyFiles.Length);
        //var assemblies = new List<Assembly>(assemblyFiles.Length);

        //foreach (var assemblyFile in assemblyFiles)
        //{

        //    using (var stream = await assemblyFile.Content.ReadAsStreamAsync())
        //    {
        //       byte[] data = new byte[stream.Length];
        //       await stream.ReadAsync(data, 0, data.Length);
        //       var assemblyInstance = Assembly.Load(data);
        //       assemblies.Add(assemblyInstance);
        //       var metadataReference = MetadataReference.CreateFromImage(data);
        //       references.Add(metadataReference);
        //    }

        //}

        //Assemblies = assemblies;
        //References = references;

        //class BlazorBoot
        //{
        //    public string main { get; set; }

        //    public string entryPoint { get; set; }

        //    public string[] assemblyReferences { get; set; }

        //    public string[] cssReferences { get; set; }

        //    public string[] jsReferences { get; set; }

        //    public bool linkerEnabled { get; set; }

        //}
        //private static List<MetadataReference> References;

        //public static List<Assembly> Assemblies;

        //public string[] GetLocalAssembliesNames()
        //{
        //    var assemblyNames = new string[]
        //    {
        //        "Microsoft.CodeAnalysis.Workspaces",
        //        "Microsoft.CodeAnalysis.Workspaces.Common",
        //        "Microsoft.CodeAnalysis.CSharp.Workspaces",
        //        "Microsoft.CodeAnalysis.CSharp",
        //        "Microsoft.CodeAnalysis.Common",
        //        "Microsoft.CodeAnalysis.VisualBasic.Workspaces",
        //        "Microsoft.CodeAnalysis.Features",
        //        "Microsoft.CodeAnalysis.CSharp.Features",
        //        "Microsoft.CodeAnalysis.VisualBasic.Features"
        //    };
        //    return assemblyNames;
        //}

        //public List<AssemblyLoadInfo> GetLocalAssembliesLoadInfo()
        //{
        //    var list = new List<AssemblyLoadInfo>();
        //    foreach (var assemblyName in GetLocalAssembliesNames())
        //    {
        //        list.Add(new AssemblyLoadInfo(null, "_framework/_bin/" + assemblyName + ".dll"));
        //    }

        //    return list;
        //}

        //private static ImmutableArray<Assembly> LoadDefaultAssemblies()
        //{
        //    // build a MEF composition using the main workspaces assemblies and the known VisualBasic/CSharp workspace assemblies.
        //    // updated: includes feature assemblies since they now have public API's.
        //    var assemblyNames = new string[]
        //    {
        //        "Microsoft.CodeAnalysis.Workspaces",
        //        "Microsoft.CodeAnalysis.CSharp.Workspaces",
        //        "Microsoft.CodeAnalysis.VisualBasic.Workspaces",
        //        "Microsoft.CodeAnalysis.Features",
        //        "Microsoft.CodeAnalysis.CSharp.Features",
        //        "Microsoft.CodeAnalysis.VisualBasic.Features"
        //    };

        //    return LoadNearbyAssemblies(assemblyNames);
        //}

        //internal static ImmutableArray<Assembly> LoadNearbyAssemblies(string[] assemblyNames)
        //{
        //    var assemblies = new List<Assembly>();

        //    foreach (var assemblyName in assemblyNames)
        //    {
        //        var assembly = TryLoadNearbyAssembly(assemblyName);
        //        if (assembly != null)
        //        {
        //            assemblies.Add(assembly);
        //        }
        //        else
        //        {
        //            Console.WriteLine("Could not load:" + assemblyName);
        //        }
        //    }

        //    return assemblies.ToImmutableArray();
        //}

        //private static Assembly TryLoadNearbyAssembly(string assemblySimpleName)
        //{
        //    var thisAssemblyName = typeof(MefHostServices).GetTypeInfo().Assembly.GetName();
        //    var assemblyShortName = thisAssemblyName.Name;
        //    var assemblyVersion = thisAssemblyName.Version;
        //    var publicKeyToken = thisAssemblyName.GetPublicKeyToken().Aggregate(string.Empty, (s, b) => s + b.ToString("x2"));

        //    if (string.IsNullOrEmpty(publicKeyToken))
        //    {
        //        publicKeyToken = "null";
        //    }

        //    var assemblyName = new AssemblyName(string.Format("{0}, Version={1}, Culture=neutral, PublicKeyToken={2}", assemblySimpleName, assemblyVersion, publicKeyToken));

        //    try
        //    {
        //        return Assembly.Load(assemblyName);
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

    }


   
}
