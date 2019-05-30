//using Microsoft.AspNetCore.Components;
//using System;
//using System.Diagnostics;
//using System.IO;
//using System.Net.Http;
//using System.Threading.Tasks;

//namespace Nethereum.TryOnBrowser.Pages
//{
//    //1
//    public class IndexModel1 : ComponentBase

//    {

//        public string Output = "";

//        public string Code = @"using System;



//class Program

//{

//    public static void Main()

//    {

//        Console.WriteLine(""Hello World"");

//    }

//}";



//        [Inject] private HttpClient Client { get; set; }



//        protected override Task OnInitAsync()
//        {

//            Compiler.InitializeMetadataReferences(Client);
//            return base.OnInitAsync();

//        }


//        public async Task Run()
//        {
//            await Compiler.WhenReady(RunInternal());
//        }


//        public async Task RunInternal()

//        {

//            Output = "";



//            Console.WriteLine("Compiling and Running code");

//            var sw = Stopwatch.StartNew();

//            var currentOut = Console.Out;

//            var writer = new StringWriter();

//            Console.SetOut(writer);

//            Exception exception = null;

//            try
//            {
//                var state = await Compiler.RuWithScriptingAsync(Code);
//            }

//            catch (Exception ex)
//            {
//                exception = ex;
//            }

//            Output = writer.ToString();

//            if (exception != null)
//            {
//                Output += "\r\n" + exception.ToString();
//            }

//            Console.SetOut(currentOut);



//            sw.Stop();

//            Console.WriteLine("Done in " + sw.ElapsedMilliseconds + "ms");

//        }
//    }

//}

