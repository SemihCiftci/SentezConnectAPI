namespace Api.Model
{
    public class InventoryModel
    {
        public bool IsOk { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public object ServerMessages { get; set; }
        public List<DataItem> Data { get; set; }

    }

    public class DataItem
    {
        public int RecId { get; set; }
        public string InventoryCode { get; set; }
        public string InventoryName { get; set; }
        public string UD_UrunAdi_EN { get; set; }
        public string UD_UrunAdi_DE { get; set; }
        public string UD_UrunAdi_FR { get; set; }
        public string CategoryName { get; set; }
        public string GroupCode { get; set; }
        public string UDGroup { get; set; }
        public List<VariantItem> Variant { get; set; }
    }
    public class VariantItem
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
