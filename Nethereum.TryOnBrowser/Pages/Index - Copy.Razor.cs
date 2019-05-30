//using System;

//using System.Diagnostics;

//using System.IO;

//using System.Net.Http;

//using System.Threading.Tasks;

//using Microsoft.AspNetCore.Components;

//using System.Collections.Generic;


//using System.CommandLine;

//using System.CommandLine.Binding;

//using System.CommandLine.Builder;

//using System.CommandLine.Invocation;

//using System.Linq;

//using System.Reflection;

//using Newtonsoft.Json;
//using System.Threading;


// Using Try.net compilation and console output
//namespace Nethereum.TryOnBrowser.Pages
//{
//    //2
//    public class IndexModel2 : ComponentBase

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

//            // Output = "";

//            var output = new List<string>();

//            string runnerException = null;

//            var stdOut = new StringWriter();

//            var stdError = new StringWriter();



//            Console.WriteLine("Compiling and Running code");

//            var sw = Stopwatch.StartNew();



//            var currentOut = Console.Out;

//            var writer = new StringWriter();

//            Console.SetOut(writer);


//            try

//            {

//                var (success, asm, rawBytes) = Compiler.LoadSource(Code);

//                if (success)

//                {

//                    var hasArgs = asm.EntryPoint.GetParameters().Length > 0;

//                    //asm.EntryPoint.Invoke(null, hasArgs ? new string[][] { null } : null);

//                    var args = new string[] { };



//                    var builder = new CommandLineBuilder()

//                        .ConfigureRootCommandFromMethod(asm.EntryPoint);



//                    var parser = builder.Build();

//                    parser.InvokeAsync(args).GetAwaiter().GetResult();
//                }

//            }

//            catch (Exception e)

//            {

//                if ((e.InnerException ?? e) is TypeLoadException t)

//                {

//                    runnerException = $"Missing type `{t.TypeName}`";

//                }

//                if ((e.InnerException ?? e) is MissingMethodException m)

//                {

//                    runnerException = $"Missing method `{m.Message}`";

//                }

//                if ((e.InnerException ?? e) is FileNotFoundException f)

//                {

//                    runnerException = $"Missing file: `{f.FileName}`";

//                }



//                output.AddRange(SplitOnNewlines(e.ToString()));

//            }



//            var errors = stdError.ToString();

//            if (!string.IsNullOrWhiteSpace(errors))

//            {

//                runnerException = errors;

//                output.AddRange(SplitOnNewlines(errors));

//            }


//            output.AddRange(SplitOnNewlines(stdOut.ToString()));

//            Output = string.Join(Environment.NewLine,output.ToArray());
//            Console.WriteLine(Output);

//            sw.Stop();

//            Console.WriteLine("Done in " + sw.ElapsedMilliseconds + "ms");

//        }

//        private static IEnumerable<string> SplitOnNewlines(string str)

//        {

//            str = str.Replace("\r\n", "\n");

//            return str.Split('\n');

//        }



//    }

//    internal static class CommandLineBuilderExtensions

//    {

//        public static CommandLineBuilder ConfigureRootCommandFromMethod(

//            this CommandLineBuilder builder,

//            MethodInfo method)

//        {

//            if (builder == null)

//            {

//                throw new ArgumentNullException(nameof(builder));

//            }



//            if (method == null)

//            {

//                throw new ArgumentNullException(nameof(method));

//            }



//            builder.Command.ConfigureFromMethod(method);







//            return builder;

//        }



//        private static readonly string[] _argumentParameterNames =

//        {

//            "arguments",

//            "argument",

//            "args"

//        };



//        public static void ConfigureFromMethod(

//            this Command command,

//            MethodInfo method)

//        {

//            if (command == null)

//            {

//                throw new ArgumentNullException(nameof(command));

//            }



//            if (method == null)

//            {

//                throw new ArgumentNullException(nameof(method));

//            }



//            foreach (var option in method.BuildOptions())

//            {

//                command.AddOption(option);

//            }



//            if (method.GetParameters()

//                .FirstOrDefault(p => _argumentParameterNames.Contains(p.Name)) is ParameterInfo argsParam)

//            {

//                command.Argument = new Argument

//                {

//                    ArgumentType = argsParam.ParameterType,

//                    Name = argsParam.Name

//                };

//            }



//            command.Handler = CommandHandler.Create(method);

//        }



//        public static IEnumerable<Option> BuildOptions(this MethodInfo type)

//        {

//            var descriptor = HandlerDescriptor.FromMethodInfo(type);



//            var omittedTypes = new[]

//            {

//                typeof(IConsole),

//                typeof(InvocationContext),

//                typeof(BindingContext),

//                typeof(ParseResult),

//                typeof(CancellationToken),

//            };



//            foreach (var option in descriptor.ParameterDescriptors

//                .Where(d => !omittedTypes.Contains(d.Type))

//                .Where(d => !_argumentParameterNames.Contains(d.Name))

//                .Select(p => p.BuildOption()))

//            {

//                yield return option;

//            }

//        }



//        public static Option BuildOption(this ParameterDescriptor parameter)

//        {

//            var argument = new Argument

//            {

//                ArgumentType = parameter.Type

//            };



//            if (parameter.HasDefaultValue)

//            {

//                argument.SetDefaultValue(parameter.GetDefaultValue);

//            }



//            var option = new Option(

//                parameter.BuildAlias(),

//                parameter.Name,

//                argument);



//            return option;

//        }



//        public static string BuildAlias(this IValueDescriptor descriptor)

//        {

//            if (descriptor == null)

//            {

//                throw new ArgumentNullException(nameof(descriptor));

//            }



//            return BuildAlias(descriptor.Name);

//        }



//        internal static string BuildAlias(string parameterName)

//        {

//            if (String.IsNullOrWhiteSpace(parameterName))

//            {

//                throw new ArgumentException("Value cannot be null or whitespace.", nameof(parameterName));

//            }



//            return parameterName.Length > 1

//                ? $"--{parameterName.ToKebabCase()}"

//                : $"-{parameterName.ToLowerInvariant()}";

//        }

//    }

//}


