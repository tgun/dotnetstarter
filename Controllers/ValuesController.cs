using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Prometheus;

namespace dotnetstarter.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase {
        private static readonly Counter myCounter =
            Metrics.CreateCounter("dotnetstarter_requestcounter", "Counts the number of requests happening");

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get() {
            myCounter.Inc();
            myCounter.Publish();
            return new[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id) {
            myCounter.Inc();
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value) {
            myCounter.Inc();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
            myCounter.Inc();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
            myCounter.Inc();
        }
    }
}
