namespace NetDapps.Assemblies
{
    public interface IAssemblyCacheInitialiser
    {
        Task InitialiseCache();
        bool IsInitialised();
        void WhenReady(Func<Task> action);
    }
}