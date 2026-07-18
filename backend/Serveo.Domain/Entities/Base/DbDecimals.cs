namespace Serveo.Domain.Entities.Base
{
    public static class DbDecimals
    {
        public const string Money = "decimal(18,2)";
        public const string Quantity = "decimal(18,3)";
        public const string Rate = "decimal(5,2)"; // Rate / Percentage
        public const string Fx = "decimal(19,6)"; // FX / multi-currency
    }
}
