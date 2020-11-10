using System;
using System.Linq;
using BasicRestApi.API.Database;
using BasicRestApi.API.Models.Domain;
using BasicRestApi.API.Models.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BasicRestApi.API.Controllers
{
    [ApiController]
    [Route("shops")]
    public class ShopController : ControllerBase
    {
        private readonly AppDbContext _database;
        private readonly ILogger<ShopController> _logger;

        public ShopController(AppDbContext database, ILogger<ShopController> logger)
        {
            _database = database;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllShops()
        {
            _logger.LogInformation("Getting all shops");
            var shops = _database.Shops.Select(x => new ShopWebOutput(x.Id, x.Name)).ToArray();
            return Ok(shops);
        }
    }
}