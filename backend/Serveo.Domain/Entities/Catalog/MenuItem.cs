namespace Serveo.Domain.Entities.Catalog
{
    public sealed class MenuItem
    {
        public Guid MenuId { get; set; }
        public Guid ProductId { get; set; }

        //public int DisplayOrder { get; set; }
        //public decimal? PriceOverride { get; set; }
        //public bool IsAvailable { get; set; }


        public Menu Menu { get; set; } = default!;
        public Product Product { get; set; } = default!;
    }
}
