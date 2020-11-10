using System.Linq;
using BasicRestApi.API.Models;
using BasicRestApi.API.Models.Web;
using BasicRestApi.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BasicRestApi.API.Controllers
{
    [ApiController]
    [Route("shops")]
    public class ShopController : ControllerBase
    {
        private readonly IShopRepository _shopRepository;
        private readonly ILogger<ShopController> _logger;

        public ShopController(IShopRepository shopRepository, ILogger<ShopController> logger)
        {
            _shopRepository = shopRepository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllShops()
        {
            _logger.LogInformation("Getting all shops");
            var shops = _shopRepository.GetAllShops().Select(x => x).ToList();
            return Ok(shops);
        }

        [HttpPost]
        public IActionResult CreateShop(ShopUpsertInput input)
        {
            _logger.LogInformation("Creating a shop", input);
            var persistedShop = _shopRepository.Insert(input.Name);
            return Created($"/shops/{persistedShop.Id}", persistedShop);
        }
    }
}