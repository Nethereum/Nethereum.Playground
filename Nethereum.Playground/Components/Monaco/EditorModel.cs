using System;

namespace Nethereum.Playground.Components.Monaco
{
	public class EditorModel
	{
		public string Script { get; set; } = "// Code goes here";
		public string Language { get; set; } = "javascript";
		public string Id { get; set; } = $"BlazorMonaco_{new Random().Next(0, 1000000).ToString()}";
	}
}
