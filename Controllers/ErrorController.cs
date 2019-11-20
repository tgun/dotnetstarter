using System;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace dotnetstarter.Controllers {
    [ApiController]
    public class ErrorController : ControllerBase {
        [Route("/error")]
        public IActionResult Error([FromServices] IHostingEnvironment webHostingEnvironment) {
            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            Exception ex = feature?.Error;
            bool isDev = webHostingEnvironment.IsDevelopment();

            if (ex == null)
                return StatusCode(500);

            var problemDetails = new ProblemDetails {
                Status = (int)HttpStatusCode.InternalServerError,
                Instance = feature?.Path,
                Title = isDev ? $"{ex.GetType().Name}: {ex.Message}" : "An error occurred.",
                Detail = isDev ? ex.StackTrace : null,
            };

            return StatusCode(problemDetails.Status.Value, problemDetails);

        }
    }
}
