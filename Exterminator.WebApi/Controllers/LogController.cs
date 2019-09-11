using Exterminator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Exterminator.WebApi.Controllers
{
    [Route("api/logs")]
    public class LogController : Controller
    {
        private readonly ILogService _logservice;

        public LogController(ILogService logService) 
        {
            _logservice = logService;
        }
        
        [HttpGet]
        [Route("")]
        public IActionResult GetAllLogs() => Ok(_logservice.GetAllLogs());
    }
}