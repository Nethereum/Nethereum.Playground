using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using NetDapps.Assemblies;
using Nethereum.Playground.Components.Monaco.Services;
using System.Net.Http.Json;

namespace Nethereum.Playground.Services
{
    public class PlaygroundAssemblyCacheInitialiser : AssemblyCacheInitialiser
    {
        private readonly HttpClient client;

        public PlaygroundAssemblyCacheInitialiser(HttpClient client)
        {
            this.client = client;
            client.Timeout = TimeSpan.FromMilliseconds(2000);
            InitializationTask = InitialiseCache();
        }

        public override async Task InitialiseCache()
        {
            try
            {
                var response = await client.GetFromJsonAsync<AssemblyLoadInfo[]>("assemblies.json");
                await Task.WhenAll(response.Select(x =>  
                    AssemblyCache.Current.LoadAssembly(client, x, this))
                 );

                var response2 = await client.GetFromJsonAsync<AssemblyLoadInfo[]>("core-assemblies.json");
                await Task.WhenAll(response2.Select(x => AssemblyCache.Current.LoadAssembly(client, x)));
              

                await AssemblyCache.Current.LoadAssembly(client,
                   new AssemblyLoadInfo(null, "res2/System.Private.CoreLib.dll"));

                ProjectEditorInitialiser.InitialiseProjectsFirstInit();

                NotifyInitialisationAssemblyCacheComplete(true);
            }
            catch (Exception ex)
            {
                NotifyInitialisationAssemblyCacheComplete(false);
                Console.WriteLine("Error:" + ex.Message);
            }
        }

        //AssemblyCache.Current.InitialiseCurrentAssembliesFromMemory();
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



}
