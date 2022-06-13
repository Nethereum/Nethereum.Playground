namespace Nethereum.Playground.Components.Monaco.MonacoDTOs
{
    public class Completion
    {
        public string Label { get; set; }
        public int Kind { get; set; }
        public string Documentation { get; set; }
        public string InsertText { get; set; }
        public string FilterText { get; set; }
        public string SortText { get; set; }
    }
}