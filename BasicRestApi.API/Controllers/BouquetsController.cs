using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicRestApi.API.Models;
using BasicRestApi.API.Models.Web;
using BasicRestApi.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BasicRestApi.API.Controllers
{
    [ApiController]
    [Route("shops")]
    public class BouquetController : ControllerBase
    {
        private readonly IBouquetRepository _bouquetRepository;
        private readonly ILogger<ShopController> _logger;

        public BouquetController(IBouquetRepository bouquetRepository, ILogger<ShopController> logger)
        {
            _bouquetRepository = bouquetRepository;
            _logger = logger;
        }


        /// <summary>
        ///   Gets you a list of all the bouquets inside a shop. If the shop does not exist, you get a 404. An empty list means "no bouquets available".
        /// </summary>
        /// <param name="shopId">The unique identifier of the shop</param>
        /// <returns>A list of bouquets</returns>
        [HttpGet("{shopId}/bouquets")]
        [ProducesResponseType(typeof(IEnumerable<BouquetWebOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // This one is needed because we handle NotFoundExceptions explicitly. Other errors throw a 400/500.
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAllBouquetsForShop(int shopId)
        {
            _logger.LogInformation($"Getting all bouquets for shop {shopId}");
            try
            {
              return Ok((await _bouquetRepository.GetAllBouquets(shopId)).Select(x => x.Convert()).ToList());
            }
            catch (NotFoundException)
            {
              return NotFound();
            }
        }


        /// <summary>
        /// Gets you a specific bouquet in a shop. A 404 means that the shop with said id does not exists.
        /// </summary>
        /// <param name="shopId">The unique identifier of the shop</param>
        /// <param name="bouquetId">The unique identifier of the bouquet</param>
        /// <returns>A specific bouquet</returns>
        [HttpGet("{shopId}/bouquets/{id}")]
        [ProducesResponseType(typeof(BouquetWebOutput),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetBouquetFromShop(int shopId, int id)
        {
            _logger.LogInformation($"Getting bouquet by id {id} from shop {shopId}");
            var bouquet = await _bouquetRepository.GetOneBouquetById(shopId, id);
            return bouquet == null ? (IActionResult) NotFound() : Ok(bouquet.Convert());
        }


        /// <summary>
        /// Creates a new bouquet inside a shop. A 404 means that the shop with said id does not exist.
        /// </summary>
        /// <param name="shopId">The unique identifier of the shop</param>
        /// <param name="input">The body of the bouquet</param>
        /// <returns></returns>
        [HttpPost("{shopId}/bouquets")]
        [ProducesResponseType(typeof(BouquetWebOutput),StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddBouquetToShop(int shopId, BouquetUpsertInput input)
        {
            _logger.LogInformation($"Adding a bouquet for shop {shopId}");
            try
            {
              var persistedBouquet = await _bouquetRepository.Insert(shopId, input.Name, input.Price, input.Description);
              return Created($"/shops/{shopId}/bouquets/{persistedBouquet.Id}", persistedBouquet.Convert());
            }
            catch (NotFoundException)
            {
              return NotFound();
            }
        }
    }
}