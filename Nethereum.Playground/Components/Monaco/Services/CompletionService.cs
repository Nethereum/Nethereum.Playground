using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Completion;
using Microsoft.CodeAnalysis.Recommendations;
using Microsoft.JSInterop;
using Nethereum.Playground.Components.Monaco.MonacoDTOs;

namespace Nethereum.Playground.Components.Monaco.Services
{
    public class CompletionService
    {
        
        public static int TryGetMonacoItemType(CompletionItem completionItem)
        {
            try
            {
                var type = completionItem.Properties["SymbolKind"];
                switch (type)
                {
                    case "15":
                        return 9; ///Property
                    case "9":
                        return 1; //Method / Function
                    case "12":
                        return 8; //monaco.languages.CompletionItemKind.Module "Namespace"
                    case "11":
                        return 5; //monaco.languages.CompletionItemKind.Class
                    default:
                        return 1;
                }
            }
            catch
            {
                return 1;
            }

            /*
             Method = 0,
            Function = 1,
            Constructor = 2,
            Field = 3,
            Variable = 4,
            Class = 5,
            Struct = 6,
            Interface = 7,
            Module = 8,
            Property = 9,
            Event = 10,
            Operator = 11,
            Unit = 12,
            Value = 13,
            Constant = 14,
            Enum = 15,
            EnumMember = 16,
            Keyword = 17,
            Text = 18,
            Color = 19,
            File = 20,
            Reference = 21,
            Customcolor = 22,
            Folder = 23,
            TypeParameter = 24,
            Snippet = 25
             */
        }

        public static string TryGetPropertyValue(Microsoft.CodeAnalysis.Completion.CompletionItem completionItem, string key)
        {
            try
            {
                return completionItem.Properties[key];

            }
            catch
            {
                return "";
            }
        }

        [JSInvokable()]
        public static async Task<Completion[]> GetCompletionItems(EditorModel editorModel, int position)
        {
            var results = await GetCompletion(editorModel.Script, editorModel.Language, position);

            if (results.Item1 != null)
            {
                return results.Item1.Items.GroupBy(x => x.DisplayText).Select(x => x.FirstOrDefault()).Select(x => new Completion
                {
                    Label = x.DisplayText,
                    Documentation = results.Item2, //only we get one info 
                    Kind = TryGetMonacoItemType(x),
                    InsertText = TryGetPropertyValue(x, "InsertionText"),
                    FilterText = x.FilterText,
                    SortText = x.SortText
                }).ToArray();
            }

            return null;
        }

        public static async Task<(CompletionList, string)> GetCompletion(string source, string language, int position)
        {
            var description = string.Empty;
            if (language == "csharp")
            {
                if (!CSharpEditorProject.Current.IsInitialised()) return (null, null);

                var document = CSharpEditorProject.Current.GetCurrentDocument(source);
                var completionService = Microsoft.CodeAnalysis.Completion.CompletionService.GetService(document);
                var items = await completionService.GetCompletionsAsync(document, position);
                var semanticModel = await document.GetSemanticModelAsync();
                var recommendedSymbols =
                    await Recommender.GetRecommendedSymbolsAtPositionAsync(semanticModel, position,
                        CSharpEditorProject.Current.Workspace);

                if (items != null && items.Items.Length == 1)
                {
                    var documentDescription = await completionService.GetDescriptionAsync(document, items.Items[0]);
                    description = documentDescription.Text;
                }

                return (items, description);

            }
            else if (language == "vb")
            {

            }

            return (null, null);
        }
    }
}
