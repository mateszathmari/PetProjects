using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFDataAccess.DataAccess;
using Microsoft.EntityFrameworkCore;
using RecipesAPI.Models;

namespace RecipesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;
        private readonly UserContext context;
        private SQLUserRepository _db;

        public ApiController(ILogger<ApiController> logger, UserContext context)
        {
            _logger = logger;
            _db = new SQLUserRepository(context);
        }



        [HttpGet()]
        public string Get()
        {
            return "data";
        }
    }
}
