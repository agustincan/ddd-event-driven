using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Common.Transversal
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BaseController<T>: ControllerBase
    {
        protected ILogger<T>? _logger;

        private ILogger<T> Logger
        {
            get
            {
                if (_logger == null)
                {
                    var loggerFactory = HttpContext.RequestServices.GetService<ILoggerFactory>();
                    _logger = loggerFactory.CreateLogger<T>();
                }
                return _logger;
            }
        }

        protected ActionResult ErrorResponse(Exception ex, string safeErrorMessage)
        {
            _logger.LogError(ex, safeErrorMessage);

            return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
            {
                Message = safeErrorMessage
            });
        }

        [HttpGet("status")]
        public IActionResult Status()
        {
            return Ok($"{typeof(T).Name} running ...");
        }
    }
}
