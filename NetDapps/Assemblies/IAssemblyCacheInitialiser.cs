namespace NetDapps.Assemblies
{
    public delegate void AssemblyCacheInitialisionCompleteHandler(bool success);
    public delegate void AssemblyLoadCompleteHandler(AssemblyLoadInfo assemblyLoadInfo);
    public interface IAssemblyCacheInitialiser
    {
        Task InitialiseCache();
        bool IsInitialised();
        void WhenReady(Func<Task> action);

        event AssemblyCacheInitialisionCompleteHandler? AssemblyCacheInitialisionComplete;
        event AssemblyLoadCompleteHandler? AssemblyLoadComplete;
        
        void NotifyAssemblyLoadComplete(AssemblyLoadInfo assemblyLoadInfo);

    }
}