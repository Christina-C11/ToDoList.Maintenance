using ToDoList.Maintenance.BusinessRules.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ToDoList.Maintenance.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MaintenanceController : ControllerBase
    {

        private readonly ILogger<MaintenanceController> _logger;
        public readonly IMaintenanceService _maintenanceService;
        public MaintenanceController(ILogger<MaintenanceController> logger,
               IMaintenanceService maintenanceService
            )
        {
            _logger = logger;
            _maintenanceService = maintenanceService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_maintenanceService.Get());
                    
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}