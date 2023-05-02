namespace ESM.Modules.Import.Utilities
{
    public class SelectableViewModel
    {
        public bool IsSelected { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double? Discount { get; set; }
        public decimal SellPrice => Discount == null || Discount == 0 ? Price : Price * (1 - (decimal)Discount / 100);
        public string Unit { get; set; }
        public int Remain { get; set; }
        public override bool Equals(object? obj)
        {
            return Id == (obj as SelectableViewModel)?.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
