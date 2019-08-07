using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Nethereum.Playground.Components.Monaco
{
	public static class Interop
	{
		public static Task<bool> EditorInitializeAsync(IJSRuntime jsruntime, EditorModel editorModel)
			=> jsruntime.InvokeAsync<bool>("BlazorBitsMonacoInterop.EditorInitialize", new[] { editorModel });

        public static Task<EditorModel> EditorGetAsync(IJSRuntime jsruntime, EditorModel editorModel)
            => jsruntime.InvokeAsync<EditorModel>("BlazorBitsMonacoInterop.EditorGet", new[] { editorModel });

		public static Task<EditorModel> EditorSetAsync(IJSRuntime jsruntime, EditorModel editorModel)
			=> jsruntime.InvokeAsync<EditorModel>("BlazorBitsMonacoInterop.EditorSet", new[] { editorModel });
    }
}
