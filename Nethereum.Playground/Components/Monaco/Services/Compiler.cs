using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.Scripting.Hosting;
using System;
using System.Collections.Immutable;
using System.CommandLine.Invocation;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.CodeAnalysis.Completion;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Host.Mef;
using Microsoft.CodeAnalysis.QuickInfo;
using Microsoft.CodeAnalysis.Recommendations;
using Microsoft.CodeAnalysis.Text;
using NetDapps.Assemblies;
using Nethereum.Playground.Components.Monaco.MonacoDTOs;
using Newtonsoft.Json;

namespace Nethereum.Playground
{

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
