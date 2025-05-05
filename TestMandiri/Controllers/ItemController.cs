using Azure.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using TestMandiri.Data.Models;
using TestMandiri.Services;
using YourNamespace.Services.Interfaces;

namespace TestMandiri.Controllers
{
    [Route("api/Item")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpPost("addTransaction")]
        public IActionResult Login([FromBody] TransactionModel request)
        {
            var item = request.items;
            var idUser = request.idUser;
            var result = _itemService.addTransaction(idUser, item);
            if (result == "ok")
                return Ok(new { message = result });

            return BadRequest(new { message = result });
        }

        [HttpPost("ViewGrid")]
        public async Task<IActionResult> Register([FromQuery] int takes,string? orderby)
        {
            try
            {
             
                var result = await _itemService.getTransactionsAsync(takes,orderby);
                 return Ok(result);
                
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError(nameof(Register), ex);
                return BadRequest(new { message = "mohon hubungi tim support" });
            }
        }
    }
}
