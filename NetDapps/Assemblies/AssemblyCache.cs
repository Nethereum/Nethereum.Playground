using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using System.Runtime.Loader;

namespace NetDapps.Assemblies
{

    public class AssemblyCache
    { 
        private static AssemblyCache _current;
        public static AssemblyCache Current
        {
            get
            {
                if (_current == null) _current = new AssemblyCache();

                return _current;
            }
        }

        public List<AssemblyLoadInfo> LoadedAssemblies { get; } = new List<AssemblyLoadInfo>();

        public void InitialiseCurrentAssembliesFromMemory()
        {
            foreach (var ass in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!ContainsAssembly(ass.FullName))
                    LoadedAssemblies.Add(new AssemblyLoadInfo(ass.FullName));
            }

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        public async Task LoadInternalAssembly(HttpClient client, AssemblyLoadInfo assemblyInfo)
        {
            try
            {

                if (!ContainsAssembly(assemblyInfo.FullName) && !ContainsAssemblyRemotePath(assemblyInfo.Url))
                {

                    var assemblyStreamByte = await client.GetByteArrayAsync(GetAssemblyRemotePath(assemblyInfo.Url));

                    // var assemblyStream = await client.GetStreamAsync(GetAssemblyRemotePath(assemblyInfo.PublishedRemotePath));
                    //var assembly = AssemblyLoadContext.Default.LoadFromStream(new MemoryStream(assemblyStreamByte));
                    //assembly = AppDomain.CurrentDomain.Load(assemblyStreamByte);
                    //making sure we have the right name as we may not have passed it as a parameter.
                    //assemblyInfo.FullName = assembly.FullName;
                    assemblyInfo.MetadataReference = MetadataReference.CreateFromImage(assemblyStreamByte);
                    //assemblyInfo.Assembly = assembly;
                    LoadedAssemblies.Add(assemblyInfo);
                    Console.WriteLine("Loaded: " + assemblyInfo.FullName);

                }
                else
                {
                    Console.WriteLine("Already loaded:" + assemblyInfo.FullName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error occurred loading assembly {assemblyInfo.FullName} from url:{assemblyInfo.Url}, {ex.Message}");
            }
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (args.Name.StartsWith("System.Runtime"))
            {
                return AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.FullName.StartsWith("System.Runtime,"));
            }

            Console.WriteLine("Requesting" + args.Name);
            return AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.FullName == args.Name);
        }

        public bool ContainsAssembly(string assemblyFullName)
        {
            if (string.IsNullOrEmpty(assemblyFullName)) return false;
            return LoadedAssemblies.Exists(x => string.Equals(x.FullName, assemblyFullName, StringComparison.InvariantCultureIgnoreCase));
        }

       

        public bool ContainsAssemblyRemotePath(string remotePath)
        {
            if (string.IsNullOrEmpty(remotePath)) return false;
            return LoadedAssemblies.Exists(x => string.Equals(x.Url, remotePath, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task LoadAssemblies(HttpClient client, params AssemblyLoadInfo[] assemblyInfos)
        {
            foreach (var assemblyInfo in assemblyInfos)
            {
                await LoadAssembly(client, assemblyInfo);
            }
        }

        public MetadataReference[] GetAllMetadataReferences()
        {
            return LoadedAssemblies.Select(x => x.MetadataReference).ToArray();
        }

        public void LoadAssembly(IEnumerable<MemoryStream> assemblyStreams)
        {
            foreach (var assemblyStream in assemblyStreams)
            {
                var streamBytes = assemblyStream.ToArray();
                var assembly = AppDomain.CurrentDomain.Load(streamBytes);
                if (!ContainsAssembly(assembly.FullName))
                {
                    assembly = AssemblyLoadContext.Default.LoadFromStream(assemblyStream);
                    var assemblyInfo = new AssemblyLoadInfo();
                    //making sure we have the right name as we may not have passed it as a parameter.
                    assemblyInfo.FullName = assembly.FullName;
                    assemblyInfo.MetadataReference = MetadataReference.CreateFromImage(streamBytes);
                    assemblyInfo.Assembly = assembly;
                    LoadedAssemblies.Add(assemblyInfo);
                    
                }
            }

        }

        public async Task LoadAssemblyFallback(HttpClient client, AssemblyLoadInfo assemblyInfo, IAssemblyCacheInitialiser? assemblyCacheInitialiser = null, int retryCount = 0)
        {
            try
            {

                if (!ContainsAssembly(assemblyInfo.FullName) && !ContainsAssemblyRemotePath(assemblyInfo.Url))
                {

                    var assemblyStreamByte = await client.GetByteArrayAsync(GetAssemblyFallbackPath(assemblyInfo)) ;

                    // var assemblyStream = await client.GetStreamAsync(GetAssemblyRemotePath(assemblyInfo.PublishedRemotePath));
                    var assembly = AssemblyLoadContext.Default.LoadFromStream(new MemoryStream(assemblyStreamByte));
                    // assembly = AppDomain.CurrentDomain.Load(assemblyStreamByte);
                    //making sure we have the right name as we may not have passed it as a parameter.
                    assemblyInfo.FullName = assembly.FullName;
                    assemblyInfo.MetadataReference = MetadataReference.CreateFromImage(assemblyStreamByte);
                    assemblyInfo.Assembly = assembly;
                    LoadedAssemblies.Add(assemblyInfo);
                    Console.WriteLine("Loaded: " + assemblyInfo.FullName);
                    if (assemblyCacheInitialiser != null)
                    {
                        assemblyCacheInitialiser.NotifyAssemblyLoadComplete(assemblyInfo);
                    }

                }
                else
                {
                    Console.WriteLine("Already loaded:" + assemblyInfo.FullName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error occurred loading assembly {assemblyInfo.FullName} from remote path:{assemblyInfo.Url}, {ex.Message}");
                if (retryCount < 5)
                {
                    await LoadAssemblyFallback(client, assemblyInfo, assemblyCacheInitialiser, retryCount + 1);
                }
            }
        }

        public async Task LoadAssembly(HttpClient client, AssemblyLoadInfo assemblyInfo, IAssemblyCacheInitialiser? assemblyCacheInitialiser = null)
        {
            try
            {

                if (!ContainsAssembly(assemblyInfo.FullName) && !ContainsAssemblyRemotePath(assemblyInfo.Url))
                {

                   
                    var assemblyStreamByte = await client.GetByteArrayAsync(GetAssemblyRemotePath(assemblyInfo.Url) );

                    // var assemblyStream = await client.GetStreamAsync(GetAssemblyRemotePath(assemblyInfo.PublishedRemotePath));
                    var assembly = AssemblyLoadContext.Default.LoadFromStream(new MemoryStream(assemblyStreamByte));
                   // assembly = AppDomain.CurrentDomain.Load(assemblyStreamByte);
                    //making sure we have the right name as we may not have passed it as a parameter.
                    assemblyInfo.FullName = assembly.FullName;
                    assemblyInfo.MetadataReference = MetadataReference.CreateFromImage(assemblyStreamByte);
                    assemblyInfo.Assembly = assembly;
                    LoadedAssemblies.Add(assemblyInfo);
                    Console.WriteLine("Loaded: " + assemblyInfo.FullName);

                    if(assemblyCacheInitialiser != null)
                    {
                        assemblyCacheInitialiser.NotifyAssemblyLoadComplete(assemblyInfo);
                    }

                }
                else
                {
                    Console.WriteLine("Already loaded:" + assemblyInfo.FullName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error occurred loading assembly {assemblyInfo.FullName} from url:{assemblyInfo.Url}, {ex.Message}, trying fallback path");
                await LoadAssemblyFallback(client, assemblyInfo, assemblyCacheInitialiser);
                
            }
        }

        public string GetAssemblyRemotePath(string remotePath)
        {
            if (remotePath.StartsWith("ipfs://")) return remotePath.Replace("ipfs://", "https://cloudflare-ipfs.com/ipfs/");
            return remotePath;
        }

        public string GetAssemblyFallbackPath(AssemblyLoadInfo info)
        {
            
            return "fallbacklib/" + info.Name + ".dll";
        }

        public string GetAllLoadedAssemblies()
        {
            var stringOuput = new StringBuilder();
            stringOuput.AppendLine("var assembliesLoadInfo = new AssemblyLoadInfo[]{");
            foreach (var assemblyLoadInfo in LoadedAssemblies)
            {
                stringOuput.AppendLine($@"new AssemblyLoadInfo(""{assemblyLoadInfo.FullName}"",""{assemblyLoadInfo.Url}""),");
            }

            stringOuput.AppendLine(("};"));
            return stringOuput.ToString();
        }

    }
}
