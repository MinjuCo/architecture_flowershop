using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicRestApi.API.Models;
using BasicRestApi.API.Models.Web;
using BasicRestApi.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

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
        

        /// <summary>
        ///   Gets you a list of all the shops.
        /// </summary>
        /// <returns>A list of shops</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ShopWebOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllShops()
        {
            _logger.LogInformation("Getting all shops");
            var shops = (await _shopRepository.GetAllShops()).Select(x => x.Convert()).ToList();
            return Ok(shops);
        }


        /// <summary>
        ///   Gets you a specific shop with id.
        /// </summary>
        /// <param name="shopId">The unique identifier of the shop</param>
        /// <returns>A specific shop</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<ShopWebOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ShopById(int id)
        {
            _logger.LogInformation("Getting shop by id", id);
            var shop = await _shopRepository.GetOneShopById(id);
            return shop == null ? (IActionResult) NotFound() : Ok(shop.Convert());
        }


        /// <summary>
        ///   Creates a shop.
        /// </summary>
        /// <param name="input">Body of the shop</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<ShopWebOutput>), StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateShop(ShopUpsertInput input)
        {
            _logger.LogInformation("Creating a shop", input);
            var persistedShop = await _shopRepository.Insert(input.Name, input.Address, input.Region);
            return Created($"/shops/{persistedShop.Id}", persistedShop);
        }
    }
}