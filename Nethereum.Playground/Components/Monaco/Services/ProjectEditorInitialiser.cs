using System;

namespace Nethereum.Playground.Components.Monaco.Services
{
    public class ProjectEditorInitialiser
    {
        public static void InitialiseProject(string language)
        {
            //Console.WriteLine("Init project");
            if (language == "csharp")
            {
               // Console.WriteLine("Init project is csharp");
                if (CSharpEditorProject.Current.IsInitialised())
                {   
                  //  Console.WriteLine("Init again project csharp");
                    CSharpEditorProject.Current.InitialiseProject();
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