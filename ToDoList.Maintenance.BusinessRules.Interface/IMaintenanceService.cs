using ToDoList.Maintenance.Models;

namespace ToDoList.Maintenance.BusinessRules.Interface
{
    public interface IMaintenanceService
    {
        public List<ToDoItem> Get();
    }
}