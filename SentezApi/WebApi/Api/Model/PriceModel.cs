namespace Api.Model
{
    public class PriceModel
    {
        public bool IsOk { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public object ServerMessages { get; set; }
        public List<PriceItem> Data { get; set; }

    }

    public class PriceItem
    {
        public string PriceType { get; set; }
        public string PriceCode { get; set; }
        public string VatIncluded { get; set; }
        public decimal VatRate { get; set; }
        public decimal Price { get; set; }
    }
}
