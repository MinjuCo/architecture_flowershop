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
    public class BouquetController : ControllerBase
    {
        private readonly IBouquetRepository _bouquetRepository;
        private readonly ILogger<ShopController> _logger;

        public BouquetController(IBouquetRepository bouquetRepository, ILogger<ShopController> logger)
        {
            _bouquetRepository = bouquetRepository;
            _logger = logger;
        }

        [HttpGet("{shopId}/bouquets")]
        public IActionResult GetAllBouquetsForShop(int shopId)
        {
            _logger.LogInformation($"Getting all bouquets for shop {shopId}");
            try
            {
              return Ok(_bouquetRepository.GetAllBouquets(shopId).Select(x => x.Convert()).ToList());
            }
            catch (NotFoundException)
            {
              return NotFound();
            }
        }

        [HttpGet("{shopId}/bouquets/{id}")]
        public IActionResult GetBouquetFromShop(int shopId, int id)
        {
            _logger.LogInformation($"Getting bouquet by id {id} from shop {shopId}");
            var bouquet = _bouquetRepository.GetOneBouquetById(shopId, id);
            return bouquet == null ? (IActionResult) NotFound() : Ok(bouquet.Convert());
        }

        [HttpPost("{shopId}/bouquets")]
        public IActionResult AddBouquetToShop(int shopId, BouquetUpsertInput input)
        {
            _logger.LogInformation($"Adding a bouquet for shop {shopId}");
            try
            {
              var persistedBouquet = _bouquetRepository.Insert(shopId, input.Name, input.Price, input.Description);
              return Created($"/shops/{shopId}/bouquets/{persistedBouquet.Id}", persistedBouquet.Convert());
            }
            catch (NotFoundException)
            {
              return NotFound();
            }
        }
    }
}