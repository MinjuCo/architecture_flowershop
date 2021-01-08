using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flowershop.API.Models;
using Flowershop.API.Models.Web;
using Flowershop.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using BasisRegisters.Vlaanderen;

namespace Flowershop.API.Controllers
{
    [ApiController]
    [Route("shops")]
    public class ShopController : ControllerBase
    {
        private readonly IShopRepository _shopRepository;
        private readonly ILogger<ShopController> _logger;
        private readonly IBasisRegisterService _basisRegisterService;

        public ShopController(IShopRepository shopRepository, ILogger<ShopController> logger, IBasisRegisterService basisRegisterService)
        {
            _shopRepository = shopRepository;
            _logger = logger;
            _basisRegisterService = basisRegisterService;
        }
        

        /// <summary>
        ///   Gets you a list of all the shops.
        /// </summary>
        /// <returns>A list of shops</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ShopWebOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> getAllShops()
        {
            _logger.LogInformation("Getting all shops");
            var shops = (await _shopRepository.getAllShops()).Select(x => x.Convert()).ToList();
            return Ok(shops);
        }


        /// <summary>
        ///   Gets you a specific shop with id.
        /// </summary>
        /// <param name="shopId">The unique identifier of the shop</param>
        /// <returns>A specific shop</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<ShopWebOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> shopById(int shopId)
        {
            _logger.LogInformation("Getting shop by id", shopId);
            var shop = await _shopRepository.getOneShopById(shopId);
            return shop == null ? (IActionResult) NotFound() : Ok(shop.Convert());
        }


        /// <summary>
        ///   Creates a shop.
        /// </summary>
        /// <remarks>
        ///     Request format:
        /// 
        ///     Post shops
        ///     {    
        ///       "Id": 1,    
        ///       "Name": "Anastasia",
        ///       "StreetName": "Anastasiastraat",
        ///       "StreetNumber": "28",
        ///       "Region": "Anastasia"        
        ///     }
        /// </remarks>
        /// <param name="shop">Body of the shop</param>
        /// <returns></returns>
        /// <response code="404">Address not found</response> 
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<ShopWebOutput>), StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> createShop(ShopUpsertInput shop)
        {
            var addresses = await _basisRegisterService
            .AddressMatchAsync(shop.Region, null, null, null, null, shop.StreetName, shop.StreetNumber, null, null);
            addresses.Warnings.ToList().ForEach(x => _logger.LogWarning($"{x.Code} {x.Message}"));
            if(!addresses.Warnings.Any()){
                //Create Shop
                _logger.LogInformation("Creating a shop", shop);
                var persistedShop = await _shopRepository.insert(shop.Name, shop.StreetName, shop.StreetNumber, shop.Region);
                return Created($"/shops/{persistedShop.Id}", persistedShop);
            }
            else{
                    return NotFound("The given address does not exist!");
            }
        }

        /// <summary>
        ///   Update a shop
        /// </summary>
        /// <remarks>
        ///     Request format:
        /// 
        ///     PUT shops
        ///     {    
        ///       "Id": 1,    
        ///       "Name": "Anastasia",
        ///       "StreetName": "Anastasiastraat",
        ///       "StreetNumber": "28",
        ///       "Region": "Anastasia"        
        ///     }
        /// </remarks>
        /// <param name="shopId">Id of the shop</param>
        /// <param name="shop">Body of the shop</param>
        /// <returns></returns>
        /// <response code="404">Address not found</response> 
        [HttpPut("{shopId}")]
        [ProducesResponseType(typeof(IEnumerable<ShopWebOutput>), StatusCodes.Status202Accepted)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> updateShop(int shopId, ShopUpsertInput shop)
        {
            var addresses = await _basisRegisterService
            .AddressMatchAsync(shop.Region, null, null, null, null, shop.StreetName, shop.StreetNumber, null, null);
            addresses.Warnings.ToList().ForEach(x => _logger.LogWarning($"{x.Code} {x.Message}"));
            if(!addresses.Warnings.Any()){
                //Create Shop
                _logger.LogInformation("Updating a shop", shop);
                var persistedShop = await _shopRepository.update(shopId, shop.Name, shop.StreetName, shop.StreetNumber, shop.Region);
                return Accepted();
            }
            else{
                return NotFound("The given address does not exist!");
            }
            
        }

        /// <summary>
        ///   Delete a shop
        /// </summary>
        /// <param name="shopId">Id of the shop</param>
        /// <returns></returns>
        /// <response code="404">Shop is not found</response> 
        /// <response code="204">Store is successfully deleted</response> 
        [HttpDelete("{shopId}")]
        [ProducesResponseType(typeof(IEnumerable<ShopWebOutput>), StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> deleteShop(int shopId)
        {
            _logger.LogInformation("Deleting a shop", shopId);
            var shop = await _shopRepository.getOneShopById(shopId);
            if(shop == null){
                return NotFound();
            }else{
                await _shopRepository.delete(shopId);
                return NoContent();
            }
            
        }
    }
}