using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDoList.Maintenance.BusinessRules.Interface;
using ToDoList.Maintenance.Models;

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
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _maintenanceService.GetAll());
                    
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Int64 id)
        {
            try
            {
                return Ok(await _maintenanceService.GetById(id));

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]ToDoItem toDoItem)
        {
            try
            {
                return Ok(await _maintenanceService.Add(toDoItem));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}