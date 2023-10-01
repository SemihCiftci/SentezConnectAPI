namespace Api.Model
{
    public class VariantModel
    {
        public bool IsOk { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public object ServerMessages { get; set; }
        public List<VariantItem> Data { get; set; }

    }

    public class DataVariantItem
    {
        public int RecId { get; set; }
        public int InventoryId { get; set; }
        public string ColorName { get; set; }
        public string ColorCode { get; set; }
        public string UD_Renk_EN { get; set; }
        public string UD_Renk_DE { get; set; }
        public string UD_Renk_FR { get; set; }
        public string SizeName { get; set; }
        public string SizeCode { get; set; }
    }
}
