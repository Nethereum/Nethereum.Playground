using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Nethereum.Playground.Components.Monaco.MonacoDTOs;

namespace NetDapps.Compiler.Services
{
    public class SignatureService
    {

        public static Task<SignatureCollection> GetSignatureCollection(string source, string language, int position)
        {
            if (language == "csharp")
            {
                if (!CSharpEditorProject.Current.IsInitialised()) return null;
                var document = CSharpEditorProject.Current.GetCurrentDocument(source);

                return GetSignatureCollection(document, position);
            }

            return null;
        }

        public static async Task<SignatureCollection> GetSignatureCollection(Document document, int position)
        {
            var invocation = await GetInvocation(document, position);
            var response = new SignatureCollection();

            if (invocation == null)
            {
                return response;
            }

            // define active parameter by position
            foreach (var comma in invocation.Separators)
            {
                if (comma.Span.Start > invocation.Position)
                {
                    break;
                }

                response.ActiveParameter += 1;
            }

            // process all signatures, define active signature by types
            var signaturesSet = new HashSet<Signature>();
            var bestScore = int.MinValue;
            Signature bestScoredItem = null;

            var types = invocation.ArgumentTypes;

            ISymbol symbol = null;
            var symbolInfo = invocation.SemanticModel.GetSymbolInfo(invocation.Receiver);
            if (symbolInfo.Symbol != null)
            {
                symbol = symbolInfo.Symbol;
            }
            else if (!symbolInfo.CandidateSymbols.IsEmpty)
            {
                symbol = symbolInfo.CandidateSymbols.First();
            }

            var overloads = symbol?.ContainingType == null
                ? Array.Empty<IMethodSymbol>()
                : symbol.ContainingType.GetMembers(symbol.Name).OfType<IMethodSymbol>();

            foreach (var methodOverload in overloads)
            {
                var signature = BuildSignature(methodOverload);
                signaturesSet.Add(signature);

                var score = InvocationScore(methodOverload, types);
                if (score > bestScore)
                {
                    bestScore = score;
                    bestScoredItem = signature;
                }
            }

            var signaturesList = signaturesSet.ToList();
            response.Signatures = signaturesList;
            response.ActiveSignature = signaturesList.IndexOf(bestScoredItem);

            return response;
        }

        internal static async Task<InvocationContext> GetInvocation(Document document, int position)
        {
            var tree = await document.GetSyntaxTreeAsync();
            var root = await tree.GetRootAsync();
            var node = root.FindToken(position).Parent;

            // Walk up until we find a node that we're interested in.
            while (node != null)
            {
                switch (node)
                {
                    case InvocationExpressionSyntax invocation when invocation.ArgumentList.Span.Contains(position):
                        {
                            var semanticModel = await document.GetSemanticModelAsync();
                            return new InvocationContext(semanticModel, position, invocation.Expression,
                                invocation.ArgumentList);
                        }

                    case ObjectCreationExpressionSyntax objectCreation
                        when objectCreation.ArgumentList?.Span.Contains(position) ?? false:
                        {
                            var semanticModel = await document.GetSemanticModelAsync();
                            return new InvocationContext(semanticModel, position, objectCreation,
                            objectCreation.ArgumentList);
                        }

                    case AttributeSyntax attributeSyntax when attributeSyntax.ArgumentList.Span.Contains(position):
                        {
                            var semanticModel = await document.GetSemanticModelAsync();
                            return new InvocationContext(semanticModel, position, attributeSyntax,
                            attributeSyntax.ArgumentList);
                        }
                }

                node = node.Parent;
            }

            return null;
        }

        private static int InvocationScore(IMethodSymbol symbol, IEnumerable<Microsoft.CodeAnalysis.TypeInfo> types)
        {
            var parameters = symbol.Parameters;
            if (parameters.Count() < types.Count())
            {
                return int.MinValue;
            }

            var score = 0;
            var invocationEnum = types.GetEnumerator();
            var definitionEnum = parameters.GetEnumerator();
            while (invocationEnum.MoveNext() && definitionEnum.MoveNext())
            {
                if (invocationEnum.Current.ConvertedType == null)
                {
                    // 1 point for having a parameter
                    score += 1;
                }
                else if (invocationEnum.Current.ConvertedType.Equals(definitionEnum.Current.Type))
                {
                    // 2 points for having a parameter and being
                    // the same type
                    score += 2;
                }
            }

            return score;
        }

        private static Signature BuildSignature(IMethodSymbol symbol)
        {
            var signature = new Signature
            {
                Documentation = symbol.GetDocumentationCommentXml(),
                Name = symbol.MethodKind == MethodKind.Constructor ? symbol.ContainingType.Name : symbol.Name,
                Label = symbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat),
            };

            signature.Parameters = symbol.Parameters.Select(parameter => new SignatureParameter
            {
                Name = parameter.Name,
                Label = parameter.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat),
                Documentation = parameter.GetDocumentationCommentXml(),
            }).ToList();

            return signature;
        }

    }
}