using ToDoList.Maintenance.Models;

namespace ToDoList.Maintenance.BusinessRules.Interface
{
    public interface IMaintenanceService
    {
        public Task<List<ToDoItem>> GetAll();
        public Task<ToDoItem> GetById(Int64 id);
        public Task<string> Add(ToDoItem item);
    }
}