using Api.CustomAttributes;
using Api.Model;
using Api.Services.SentezService.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [ApiKeyAuth]
    public class HomeController : ControllerBase
    {
        private readonly ISentezService _sentezService;

        public HomeController(ISentezService sentezService)
        {
            _sentezService = sentezService;
        }

        [HttpGet("GetInventoryList")]
        public async Task<IActionResult> GetInventoryList(string? whereStr)
        {
            LoginResult responselogin = await _sentezService.Login();
            if (!string.IsNullOrEmpty(responselogin.ErrorMessage))
                return BadRequest(responselogin.ErrorMessage);

            var response = await _sentezService.GetInventory(responselogin.Token, whereStr);
            if (response.Data == null) { return NotFound(); }
            return Ok(response.Data);
        }

        [HttpGet("GetInventoryVariant")]
        public async Task<IActionResult> GetInventoryVariant(int InventoryId)
        {
            LoginResult responselogin = await _sentezService.Login();
            if (!string.IsNullOrEmpty(responselogin.ErrorMessage))
                return BadRequest(responselogin.ErrorMessage);

            var response = await _sentezService.GetInventoryVariant(responselogin.Token, InventoryId);
            if (response.Data == null) { return NotFound(); }
            return Ok(response.Data);
        }

        [HttpGet("GetInventoryAndVariantList")]
        public async Task<IActionResult> GetInventoryAndVariantList(string? whereStr)
        {
            LoginResult responselogin = await _sentezService.Login();
            if (!string.IsNullOrEmpty(responselogin.ErrorMessage))
                return BadRequest(responselogin.ErrorMessage);

            var inventory = await _sentezService.GetInventory(responselogin.Token, whereStr);

            foreach (var item in inventory.Data)
            {

                var VariantList = await _sentezService.GetInventoryVariant(responselogin.Token, item.RecId);

                if (VariantList is not null)
                {
                    item.Variant = VariantList.Data;
                }
            }

            var result = inventory;
            return Ok(result);
        }

        [HttpGet("GetInventoryStock")]
        public async Task<IActionResult> GetInventoryStock(int InventoryId, int? InventoryVariantId, string? WarehouseCode)
        {
            LoginResult responselogin = await _sentezService.Login();
            if (!string.IsNullOrEmpty(responselogin.ErrorMessage))
                return BadRequest(responselogin.ErrorMessage);

            var response = await _sentezService.GetInventoryStock(responselogin.Token, InventoryId, InventoryVariantId, WarehouseCode);
            if (response.Data == null) { return NotFound(); }
            return Ok(response.Data);
        }

        [HttpGet("GetInventoryPrice")]
        public async Task<IActionResult> GetInventoryPrice(int InventoryId)
        {
            LoginResult responselogin = await _sentezService.Login();
            if (!string.IsNullOrEmpty(responselogin.ErrorMessage))
                return BadRequest(responselogin.ErrorMessage);

            var response = await _sentezService.GetInventoryPrice(responselogin.Token, InventoryId);
            if (response.Data == null) { return NotFound(); }
            return Ok(response.Data);
        }

        [HttpGet("GetInventoryPicture")]
        public async Task<IActionResult> GetInventoryPicture(int InventoryId)
        {
            LoginResult responselogin = await _sentezService.Login();
            if (!string.IsNullOrEmpty(responselogin.ErrorMessage))
                return BadRequest(responselogin.ErrorMessage);

            var response = await _sentezService.GetInventoryPicture(responselogin.Token, InventoryId);
            if ((response == null && response.Count > 0)) { return NotFound(); }
            return Ok(response);
        }

    }
}
