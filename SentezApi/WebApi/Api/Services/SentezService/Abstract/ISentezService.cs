using Api.Model;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Api.Services.SentezService.Abstract
{
    public interface ISentezService
    {
        Task<string> GetBoId(string loginToken, string BOName, int logicalModuleId, int type, int type2);
        Task<string> GetAll(string loginToken, string BOId);
        Task<string> GetById(string loginToken, string BOId, long RecId);
        Task<LoginResult> Login();
        Task<string> ExecuteQuery(string loginToken, string Query);
        Task<string> PostBO(string loginToken, string boName, Dictionary<string, List<ExpandoObject>> data);
        Task<string> UpdateBO(string loginToken, string boName, long RecId, Dictionary<string, List<ExpandoObject>> data);

        Task<List<InventoryModel>> GetInventoryList();
        Task<InventoryModel> GetInventoryByID(int id);
        Task<ActionResult<InventoryModel>> CreateIventoryAsync(InventoryModel product);
        Task<InventoryModel> UpdateInventoryAsync(InventoryModel product);
        Task<InventoryModel> DeleteInventoryAsync(int id);

        Task<VariantModel> GetInventoryVariant(string token, int InventoryId);
        Task<InventoryModel> GetInventory(string token, string? whereStr);
        Task<StockModel> GetInventoryStock(string token, int InventoryId, int? InventoryVariantId, string? WarehouseCode);
        Task<PriceModel> GetInventoryPrice(string token, int InventoryId);
        Task<List<string>> GetInventoryPicture(string token, int InventoryId);
    }
}
