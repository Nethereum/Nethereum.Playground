using System.Collections.Generic;

using Newtonsoft.Json;
using Nethereum.Generators.Model;

namespace Nethereum.Generators.Net
{
    public class ABIDeserialiser
    {
        public ConstructorABI BuildConstructor(IDictionary<string, object> constructor)
        {
            var constructorABI = new ConstructorABI();
            constructorABI.InputParameters = BuildFunctionParameters((List<object>) constructor["inputs"]);
            return constructorABI;
        }

        public EventABI BuildEvent(IDictionary<string, object> eventobject)
        {
            var eventABI = new EventABI((string) eventobject["name"]);
            eventABI.InputParameters = BuildEventParameters((List<object>) eventobject["inputs"]);

            return eventABI;
        }

        public ParameterABI[] BuildEventParameters(List<object> inputs)
        {
            var parameters = new List<ParameterABI>();
            var parameterOrder = 0;
            foreach (IDictionary<string, object> input in inputs)
            {
                parameterOrder = parameterOrder + 1;
                var parameter = new ParameterABI((string)input["type"], (string)input["name"], parameterOrder)
                {
                    Indexed = (bool)input["indexed"]
                };
              

                parameters.Add(parameter);
            }

            return parameters.ToArray();
        }

        public FunctionABI BuildFunction(IDictionary<string, object> function)
        {
            var functionABI = new FunctionABI((string) function["name"], (bool) function["constant"],
                false);
            functionABI.InputParameters = BuildFunctionParameters((List<object>) function["inputs"]);
            functionABI.OutputParameters = BuildFunctionParameters((List<object>) function["outputs"]);
            return functionABI;
        }

        public ParameterABI[] BuildFunctionParameters(List<object> inputs)
        {
            var parameters = new List<ParameterABI>();
            var parameterOrder = 0;
            foreach (IDictionary<string, object> input in inputs)
            {
                parameterOrder = parameterOrder + 1;
                var parameter = new ParameterABI((string) input["type"], (string) input["name"], parameterOrder);
                parameters.Add(parameter);
            }

            return parameters.ToArray();
        }

        public ContractABI DeserialiseContract(string abi)
        {
            var convertor = new ExpandoObjectConverter();
            var contract = JsonConvert.DeserializeObject<List<Dictionary<string,object>>>(abi, convertor);
            var functions = new List<FunctionABI>();
            var events = new List<EventABI>();
            ConstructorABI constructor = null;

            foreach (IDictionary<string, object> element in contract)
            {
                if ((string) element["type"] == "function")
                    functions.Add(BuildFunction(element));
                if ((string) element["type"] == "event")
                    events.Add(BuildEvent(element));
                if ((string) element["type"] == "constructor")
                    constructor = BuildConstructor(element);
            }

            var contractABI = new ContractABI();
            contractABI.Functions = functions.ToArray();
            contractABI.Constructor = constructor;
            contractABI.Events = events.ToArray();

            return contractABI;
        }
    }
}