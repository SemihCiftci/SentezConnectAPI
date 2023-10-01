using Api.Model;
using Api.Services.SentezService.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Dynamic;
using System.Text;

namespace Api.Services.SentezService.Concrete
{
    public class SentezService : ISentezService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;
        private readonly IConfiguration _configuration;
        private const string SentezApiBaseUrlKey = "SentezApiBaseUrl";


        public SentezService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
            _apiBaseUrl = _configuration.GetValue<string>(SentezApiBaseUrlKey);
        }

        public async Task<LoginResult> Login()
        {
            string userCode, password, companyCode, companyPassword;
            int userType;
            var loginResult = new LoginResult();
            string apiEndpoint = $"{_apiBaseUrl}/api/Authentication/Login";
            userCode = _configuration.GetValue<string>("UserCode");
            password = _configuration.GetValue<string>("Password");
            companyCode = _configuration.GetValue<string>("Company");
            companyPassword = _configuration.GetValue<string>("CompanyPassword");
            userType = _configuration.GetValue<int>("UserType");

            var userModel = new Dictionary<string, string>()
            {
                ["userCode"] = userCode,
                ["password"] = password,
                ["companyCode"] = companyCode,
                ["companyPassword"] = companyPassword,
                ["userType"] = userType.ToString()
            };

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer login");

            try
            {
                var uri = QueryHelpers.AddQueryString(apiEndpoint, userModel);
                HttpContent c = new StringContent(JsonConvert.SerializeObject(userModel), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(uri, c);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    dynamic json = JsonConvert.DeserializeObject(result);
                    loginResult.Token = json.Data;
                    loginResult.Success = true;
                }
                else
                {
                    Debug.WriteLine($"Response Code:{response.StatusCode}");
                }
            }

            catch (Exception ex)
            {
                Debug.WriteLine($"Error:{ex.Message}");
                loginResult.ErrorMessage = ex.Message;
            }

            return loginResult;
        }
        public async Task<ActionResult<InventoryModel>> CreateIventoryAsync(InventoryModel product)
        {
            throw new NotImplementedException();
        }
        public async Task<InventoryModel> DeleteInventoryAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<InventoryModel> GetInventoryByID(int id)
        {
            throw new NotImplementedException();

        }
        public async Task<List<InventoryModel>> GetInventoryList()
        {
            throw new NotImplementedException();
        }
        public async Task<InventoryModel> UpdateInventoryAsync(InventoryModel product)
        {
            throw new NotImplementedException();
        }

        #region SentezMainMethods
        public async Task<string> GetAll(string loginToken, string BOId)
        {
            string apiEndpoint = $"{_apiBaseUrl}/api/BO/Get";
            string json = string.Empty;
            var boModel = new Dictionary<string, string>() { ["BOId"] = BOId, };

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {loginToken}");

            try
            {
                var uri = QueryHelpers.AddQueryString(apiEndpoint, boModel);
                var response = await _httpClient.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    dynamic dynJson = JsonConvert.DeserializeObject(result);
                    json = JsonConvert.SerializeObject(dynJson.Data);
                }
                else
                {
                    Debug.WriteLine($"Response Code:{response.StatusCode}");
                }
            }

            catch (Exception ex)
            { Debug.WriteLine($"Error:{ex.Message}"); }

            return json;
        }
        public async Task<string> GetBoId(string loginToken, string BOName, int logicalModuleId, int type, int type2)
        {
            string apiCreateBoUrl = $"{_apiBaseUrl}/api/BO/CreateBO";
            string BoId = string.Empty;
            var boModel = new Dictionary<string, string>() { ["BOName"] = BOName, ["initparams.logicalModuleId"] = logicalModuleId.ToString(), ["initparams.type"] = type.ToString(), ["initparams.type2"] = type2.ToString() };

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {loginToken}");

            try
            {
                var uri = QueryHelpers.AddQueryString(apiCreateBoUrl, boModel);
                var response = await _httpClient.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    dynamic json = JsonConvert.DeserializeObject(result);
                    BoId = json.Data;
                }
                else
                {
                    Debug.WriteLine($"Response Code:{response.StatusCode}");
                }
            }

            catch (Exception ex)
            {
                Debug.WriteLine($"Error  :{ex.Message}");
            }

            return BoId;
        }
        public async Task<string> GetById(string loginToken, string BOId, long RecId)
        {
            string apiCreateBoUrl = $"{_apiBaseUrl}/api/BO/GetById";
            string boResponse = string.Empty;
            var boModel = new Dictionary<string, string>() { ["BOId"] = BOId, ["RecId"] = RecId.ToString() };

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {loginToken}");

            try
            {
                var uri = QueryHelpers.AddQueryString(apiCreateBoUrl, boModel);
                var response = await _httpClient.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    dynamic json = JsonConvert.DeserializeObject(result);
                    boResponse = JsonConvert.SerializeObject(json.Data);
                }
                else
                {
                    Debug.WriteLine($"Response Code:{response.StatusCode}");
                }
            }

            catch (Exception ex)
            {
                Debug.WriteLine($"Error:{ex.Message}");
            }

            return boResponse;
        }

        public async Task<string> PostBO(string loginToken, string boName, Dictionary<string, List<ExpandoObject>> data)
        {
            string apiEndpoint = $"{_apiBaseUrl}/api/BO/PostBO";
            string token = string.Empty;
            var boModel = new Dictionary<string, string>() { ["BoName"] = boName };

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {loginToken}");

            try
            {
                var uri = QueryHelpers.AddQueryString(apiEndpoint, boModel);
                HttpContent c = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(uri, c);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    dynamic json = JsonConvert.DeserializeObject(result);
                    token = JsonConvert.SerializeObject(json);
                }
                else
                {
                    Debug.WriteLine($"Response Code:{response.StatusCode}");
                }
            }

            catch (Exception ex)
            {
                Debug.WriteLine($"Error:{ex.Message}");
            }

            return token;
        }
        public async Task<string> UpdateBO(string loginToken, string boName, long RecId, Dictionary<string, List<ExpandoObject>> data)
        {
            string apiUrl = $"{_apiBaseUrl}/api/BO/UpdateBO";
            string token = string.Empty;
            var boModel = new Dictionary<string, string>() { ["BoName"] = boName, ["RecId"] = RecId.ToString() };

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {loginToken}");

            try
            {
                var uri = QueryHelpers.AddQueryString(apiUrl, boModel);
                HttpContent c = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync(uri, c);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    dynamic json = JsonConvert.DeserializeObject(result);
                    token = JsonConvert.SerializeObject(json);
                }
                else
                {
                    Debug.WriteLine($"Response Code:{response.StatusCode}");
                }
            }

            catch (Exception ex)
            {
                Debug.WriteLine($"Error:{ex.Message}");
            }

            return token;
        }
        public async Task<string> ExecuteQuery(string loginToken, string Query)
        {
            string apiUrl = $"{_apiBaseUrl}/api/Utility/UtilityQuery";
            string token = string.Empty;
            var boModel = new Dictionary<string, string>() { ["Query"] = Query };
            dynamic QueryModel = new JObject();
            QueryModel.Query = Query;

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {loginToken}");

            try
            {
                //var uri = QueryHelpers.AddQueryString(apiEndpoint, boModel);
                HttpContent c = new StringContent(JsonConvert.SerializeObject(QueryModel), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(apiUrl, c);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    dynamic json = JsonConvert.DeserializeObject(result);
                    token = JsonConvert.SerializeObject(json);
                }
                else
                {
                    Debug.WriteLine($"Response Code:{response.StatusCode}");
                }
            }

            catch (Exception ex)
            {
                Debug.WriteLine($"Error:{ex.Message}");
            }

            return token;
        }
        #endregion

        #region APIDataMethods
        public async Task<VariantModel> GetInventoryVariant(string token, int InventoryId)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Select ");
            sb.AppendLine("iv.RecId");
            sb.AppendLine(",iv.InventoryId");
            sb.AppendLine(",vi.ItemCode [ColorName]");
            sb.AppendLine(",vi.ItemName [ColorCode]");
            sb.AppendLine(",vi.UD_Renk_EN");
            sb.AppendLine(",vi.UD_Renk_DE");
            sb.AppendLine(",vi.UD_Renk_FR");
            sb.AppendLine(",vi2.ItemCode [SizeName]");
            sb.AppendLine(",vi2.ItemName [SizeCode]");
            sb.AppendLine("from Erp_InventoryVariant iv with (nolock)");
            sb.AppendLine("left join Erp_VariantItem vi with (nolock) on vi.RecId=iv.Variant1Id");
            sb.AppendLine("left join Erp_VariantItem vi2 with (nolock) on vi2.RecId=iv.Variant2Id");
            sb.AppendLine($"where iv.InventoryId={InventoryId}");

            var responsevariant = await ExecuteQuery(token, sb.ToString());
            var data = JsonConvert.DeserializeObject<VariantModel>(responsevariant);
            if (data == null) { return null; }
            return data;
        }
        public async Task<InventoryModel> GetInventory(string token, string? whereStr)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Select i.RecId ,i.InventoryCode ,i.InventoryName ,i.UD_UrunAdi_EN,i.UD_UrunAdi_DE,i.UD_UrunAdi_FR,c.CategoryName,ig.GroupCode");
            sb.AppendLine(",(Select Explanation from Meta_DataFieldValue with (nolock) where CodeValue=i.UD_ALTÜSTGRUP and FieldId=63) [UDGroup]");
            sb.AppendLine("from Erp_Inventory i with (nolock)");
            sb.AppendLine("left join Erp_InventoryGroup ig with (nolock) on i.GroupId=ig.RecId");
            sb.AppendLine("left join Erp_Category c with (nolock) on i.CategoryId=c.RecId");
            sb.AppendLine("where i.InventoryCode not like 'MLZ%'");
            if (!string.IsNullOrEmpty(whereStr))
                sb.AppendLine(" and " + whereStr);

            var response = await ExecuteQuery(token, sb.ToString());
            if (string.IsNullOrEmpty(response)) return null;
            var data = JsonConvert.DeserializeObject<InventoryModel>(response);
            if (data == null) { return null; }
            return data;
        }
        public async Task<StockModel> GetInventoryStock(string token, int InventoryId, int? InventoryVariantId, string? WarehouseCode)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Select ");
            sb.AppendLine("i.InventoryCode");
            sb.AppendLine(",vi.ItemCode ColorName");
            sb.AppendLine(",vi2.ItemCode SizeName");
            if (!String.IsNullOrEmpty(WarehouseCode))
                sb.AppendLine(",w.WarehouseCode");
            sb.AppendLine(",sum(Convert(int,Erp_InventoryTotal.ActualStock)) ActualStock");
            sb.AppendLine(",sum(Convert(int,Erp_InventoryTotal.Reserved)) Reserved");
            sb.AppendLine(",sum(Convert(int,Erp_InventoryTotal.ActualStock-Erp_InventoryTotal.Reserved)) LastStock");
            sb.AppendLine("from Erp_InventoryTotal with (nolock) ");
            sb.AppendLine("left join Erp_Inventory i with (nolock) on i.RecId=Erp_InventoryTotal.InventoryId");
            sb.AppendLine("left join Erp_InventoryVariant iv with (nolock) on iv.RecId=InventoryVariantId");
            sb.AppendLine("left join Erp_VariantItem vi with (nolock) on vi.RecId=iv.Variant1Id");
            sb.AppendLine("left join Erp_VariantItem vi2 with (nolock) on vi2.RecId=iv.Variant2Id");
            sb.AppendLine("left join Erp_Warehouse w with (nolock) on w.RecId=Erp_InventoryTotal.WarehouseId");
            sb.AppendLine($"where Erp_InventoryTotal.InventoryId={InventoryId} ");
            if (InventoryVariantId != null && InventoryVariantId > 0)
                sb.AppendLine($"and Erp_InventoryTotal.InventoryVariantId={InventoryVariantId} ");
            if (!string.IsNullOrEmpty(WarehouseCode))
                sb.AppendLine($"and w.WarehouseCode ='{WarehouseCode}'");
            sb.AppendLine("and Erp_InventoryTotal.TotalDate is null");
            sb.AppendLine("group by i.InventoryCode,vi.ItemCode,vi2.ItemCode");
            if (!string.IsNullOrEmpty(WarehouseCode))
                sb.AppendLine(",w.WarehouseCode");

            var response = await ExecuteQuery(token, sb.ToString());
            if (string.IsNullOrEmpty(response)) return null;
            var data = JsonConvert.DeserializeObject<StockModel>(response);
            if (data == null) { return null; }
            return data;
        }
        public async Task<PriceModel> GetInventoryPrice(string token, int InventoryId)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Select ");
            sb.AppendLine("(Case PriceType when 1 then 'Alış' when 2 then 'Satış' else '' end) PriceType");
            sb.AppendLine(",PriceCode");
            sb.AppendLine(",(Case VatIncluded when 0 then 'Hariç' when 1 then 'Dahil' else '' end) VatIncluded");
            sb.AppendLine(",isnull(VatRate,0) VatRate");
            sb.AppendLine(",isnull(Price,0) Price");
            sb.AppendLine("from Erp_InventoryPriceList with (nolock) ");
            sb.AppendLine($"where InventoryId={InventoryId} and InUse=1");

            var response = await ExecuteQuery(token, sb.ToString());
            if (string.IsNullOrEmpty(response)) return null;
            var data = JsonConvert.DeserializeObject<PriceModel>(response);
            if (data == null) { return null; }
            return data;
        }
        public async Task<List<string>> GetInventoryPicture(string token, int InventoryId)
        {
            List<string> base64List = new List<string>();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Select FileName from Erp_InventoryAttachment with(nolock) where InventoryId={InventoryId} and Type=1 ");
            var response = await ExecuteQuery(token, sb.ToString());
            if (string.IsNullOrEmpty(response)) return null;
            var data = JsonConvert.DeserializeObject<PictureModel>(response);
            string path = _configuration.GetValue<string>("SentezFilePath");
            string base64String = string.Empty;
            foreach (var item in data.Data)
            {
                string imagePath = path + item.FileName;
                byte[] imageBytes = File.ReadAllBytes(imagePath);
                base64String = Convert.ToBase64String(imageBytes);
                base64List.Add(base64String);
            }

            if (base64List == null && base64List.Count > 0) { return null; }
            return base64List;
        }
        #endregion

    }
}
