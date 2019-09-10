using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Host.Mef;
using Microsoft.CodeAnalysis.Text;
using NetDapps.Assemblies;

namespace Nethereum.Playground.Components.Monaco.Services
{
    public class CSharpEditorProject
    {
        public static CSharpEditorProject Current { get; set; } = new CSharpEditorProject();
        public AdhocWorkspace Workspace { get; private set; }
        public Project Project { get; private set; }

        private Document _currentDocument;

        public bool IsInitialised()
        {
            return Workspace != null;
        }

        public void InitialiseProject()
        {
            var host = MefHostServices.Create(MefHostServices.DefaultAssemblies);

            Workspace = new AdhocWorkspace(host);
            var projectInfo = ProjectInfo.Create(ProjectId.CreateNewId(), VersionStamp.Create(), "MyProject",
                    "MyProject",
                    LanguageNames.CSharp)
                //isSubmission: true)
                .WithCompilationOptions(new CSharpCompilationOptions(OutputKind.ConsoleApplication))
                .WithMetadataReferences(AssemblyCache.Current.GetAllMetadataReferences());

            Project = Workspace.AddProject(projectInfo);
        }

        public Document GetCurrentDocument(string source)
        {
            if (_currentDocument == null)
            {
                _currentDocument = Workspace.AddDocument(Project.Id,
                    "MyFile.cs", SourceText.From(source));
            }
            else
            {
                _currentDocument = _currentDocument.WithText(SourceText.From(source));
            }

            return _currentDocument;
        }
    }
}