namespace Api.Model
{
    public class StockModel
    {
        public bool IsOk { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public object ServerMessages { get; set; }
        public List<StockItem> Data { get; set; }

    }

    public class StockItem
    {
        public string InventoryCode { get; set; }
        public string ColorName { get; set; }
        public string SizeName { get; set; }
        public string WarehouseCode { get; set; }
        public int ActualStock { get; set; }
        public int Reserved { get; set; }
        public int LastStock { get; set; }
    }
}
