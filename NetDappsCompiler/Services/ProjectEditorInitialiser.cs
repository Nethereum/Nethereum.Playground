using System;
using System.Threading.Tasks;

namespace NetDapps.Compiler.Services
{
    public class ProjectEditorInitialiser
    {
        public static async Task InitialiseProject(string language)
        {
            //Console.WriteLine("Init project");
            if (language == "csharp")
            {
                // Console.WriteLine("Init project is csharp");
                if (CSharpEditorProject.Current.IsInitialised())
                {
                    //  Console.WriteLine("Init again project csharp");
                    await Task.Run(() => CSharpEditorProject.Current.InitialiseProject());
                }
            }

            if (language == "vb")
            {
                //todo;
            }
        }

        public static void InitialiseProjectsFirstInit()
        {
            CSharpEditorProject.Current.InitialiseProject();
        }
    }
}