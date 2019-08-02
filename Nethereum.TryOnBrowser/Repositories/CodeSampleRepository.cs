using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

namespace Nethereum.TryOnBrowser.Repositories
{
    public class CodeSampleRepository
    {
        private HttpClient _httpClient;

        private List<CodeSample> _codeSamples = new List<CodeSample>();

        public CodeSampleRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            LoadCSharpSamples();
            LoadVbSamples();
        }

        public void AddCodeSample(CodeSample codeSample)
        {
            _codeSamples.Add(codeSample);
        }

        public void SaveCustomCodeSamples()
        {
            //_codeSamples.Add(codeSample);
        }

        public void RemoveCodeSample(CodeSample codeSample)
        {
            if (codeSample.Custom)
            {
                _codeSamples.Remove(codeSample);
            }
        }

        public List<CodeSample> GetCodeSamples(CodeLanguage language)
        {
            return _codeSamples.Where(x => x.Language == language).ToList();
        }

        public void LoadCSharpSamples()
        {
            _codeSamples.AddRange(CSharpSamples.GetSamples());
        }

        public void LoadVbSamples()
        {
            _codeSamples.AddRange(VbNetSamples.GetSamples());
        }

    }


}


//OLD github
//private const string _githubPath = "nethereum/nethereum/nethereum.playground";

//private const string _githubContentUrl =
//    "https://api.github.com/repos/Nethereum/Nethereum.Playground/contents/samples/csharp";

//private const string _githubFileUrl =
//    "https://raw.githubusercontent.com/Nethereum/Nethereum.Playground/master/samples/csharp/";

//var contents = await _httpClient.GetJsonAsync<GithubContent[]>(_githubContentUrl);
//Console.WriteLine(contents.Length);
//Console.WriteLine(contents[0].Name);


//foreach (var content in contents)
//{
//    //var code = await _httpClient.GetStringAsync(_githubFileUrl + content.Name);

//    _codeSamples.Add(new CodeSample()
//    {
//        Name = content.Name,
//       // Code = code   
//    });
//}

//return _codeSamples.ToArray();
