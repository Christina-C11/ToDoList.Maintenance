using ToDoList.Maintenance.BusinessRules.Interface;
using ToDoList.Maintenance.Models;

namespace ToDoList.Maintenance.BusinessRules
{
    public class MaintenanceService : IMaintenanceService
    {
        public MaintenanceService()
        {
           
        }

        public List<ToDoItem> Get()
        {
            var toDoItems = new List<ToDoItem>();
            toDoItems.Add(new ToDoItem
            {
                Id = 1,
                Title = string.Empty,
                Items = string.Empty,
                Priority = 0,
                Status = 0,
                CreatedBy = "System",
                CreatedDate = DateTime.Now,
                LastUpdatedBy = "System",
                LastUpdatedate  = DateTime.Now
            }); 
            return toDoItems;
        }
    }
}