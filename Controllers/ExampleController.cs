using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetstarter.Common;
using dotnetstarter.Resources;
using dotnetstarter.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace dotnetstarter.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IExampleService _service;

        public ExampleController(IConfiguration config, IExampleService service) {
            _config = config;
            _service = service;
        }

        [HttpGet("secure")]
        public ActionResult<string> GetSecureData() {
            return "Some secure data";
        }

        [AllowAnonymous]
        [HttpGet("insecure")]
        public ActionResult<string> GetInsecureData() {
            return "Some data";
        }

        [AllowAnonymous]
        [HttpGet("authenticate")]
        public ActionResult<string> GetJwtToken() {
            return JwtSecurityHelper.GenerateJwtToken(_config[Constants.AuthSecretPreference]);
        }

        [HttpGet("models")]
        public async Task<IEnumerable<ExampleResource>> GetAllAsync() {
            return await _service.GetExamplesAsync();
        }

        [HttpPost("models")]
        public async Task<ExampleResource> CreateAsync([FromBody]ExampleResource resource) {
            return await _service.CreateExample(resource);
        }
    }
}