namespace NetDapps.Assemblies
{
    public abstract class AssemblyCacheInitialiser : IAssemblyCacheInitialiser
    {
        protected static Task InitializationTask { get; set; }

        public event AssemblyCacheInitialisionCompleteHandler? AssemblyCacheInitialisionComplete;
        public event AssemblyLoadCompleteHandler? AssemblyLoadComplete;

        public bool IsInitialised()
        {
            return InitializationTask != null && InitializationTask.Status == TaskStatus.RanToCompletion;
        }

        protected void NotifyInitialisationAssemblyCacheComplete(bool sucessful)
        {
            AssemblyCacheInitialisionComplete?.Invoke(sucessful);
        }

        public void NotifyAssemblyLoadComplete(AssemblyLoadInfo assemblyLoadInfo)
        {
            AssemblyLoadComplete?.Invoke(assemblyLoadInfo);
        }

        public bool ErrorLoading { get; set; }

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

        /// <summary>
        /// Override this to initialise the cache
        /// </summary>
        /// <returns></returns>
        public abstract Task InitialiseCache();
        
    }
}
