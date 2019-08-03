using System;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Nethereum.TryOnBrowser.Components;
using Nethereum.TryOnBrowser.Components.Modal;
using Nethereum.TryOnBrowser.Components.Monaco;
using Nethereum.TryOnBrowser.Repositories;

namespace Nethereum.TryOnBrowser.Pages
{
    //3
    // Based on https://github.com/Suchiman/Runny all credit to him
    public class VBIndexModel : EditorModelBase
    {
        public override string GetEditorLanguage()
        {
            return "vb";
        }

        public override string GetAllowedExtension()
        {
            return ".vb";
        }

        public override CodeLanguage GetCodeLanguage()
        {
            return CodeLanguage.VbNet;
        }

        protected override async Task CompileAndRun()
        {
            var (success, asm, rawBytes) = Compiler.LoadSource(editorModel.Script, editorModel.Language);
            if (success)

            {
                // well this is interesting - We can't do async task main in VB
                // attempting to run an async function through Sub Main causes Invoke to hang
                // until a more concrete solution is found, running async should be done through another function (RunAsync() as Task)
                // non-async ones can run through Sub Main or RunAsync (as a function name though not as apt)
                var assembly = AppDomain.CurrentDomain.Load(rawBytes);

                // check if RunAsync exists, favor this over Main
                var RunAsyncExists = (from type in assembly.GetTypes()
                                      where type.GetMethod("RunAsync") != null
                                      select type.GetMethod("RunAsync")).Any();

                var entry = assembly.EntryPoint;
                if (RunAsyncExists) // if RunAsync does not exist, fallback to Main
                {
                    entry = entry.DeclaringType.GetMethod("RunAsync",
                        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static); // reflect for RunAsync
                }
                else
                {
                    entry = entry.DeclaringType.GetMethod("Main",
                        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static); // reflect for Main
                }

                var hasArgs = entry.GetParameters().Length > 0;
                var result = entry.Invoke(null, hasArgs ? new object[] { new string[0] } : null);
                if (result is Task t)
                {
                    await t;
                }
            }
        }
    }
}

