using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartcareAPI.Repo;

namespace SmartcareAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SmartcareController : ControllerBase
    {
        private readonly IArtRepo _artRepo;

        private readonly ILogger<SmartcareController> _logger;

        public SmartcareController(IArtRepo artRepo, ILogger<SmartcareController> logger)
        {
            _logger = logger;
            _artRepo = artRepo;
        }

        [HttpGet("{mrn}")]
        public async Task<IActionResult>Get(long mrn)
        {
            try {
                var artPatients = await _artRepo.GetArtPatients(mrn);

                return Ok(artPatients);

            }catch(Exception ex) {
                return  StatusCode(500, ex.Message);
            }
        }
    }
}
