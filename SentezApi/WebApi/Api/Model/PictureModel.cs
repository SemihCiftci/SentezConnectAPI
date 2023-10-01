namespace Api.Model
{
    public class PictureModel
    {
        public bool IsOk { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public object ServerMessages { get; set; }
        public List<PictureItem> Data { get; set; }

    }

    public class PictureItem
    {
        public string FileName { get; set; }
    }
}
