using ToDoList.Maintenance.Models;

namespace ToDoList.Maintenance.BusinessRules.Interface
{
    public interface IMaintenanceService
    {
        public Task<List<ToDoItem>> GetAll(GetToDoItem item);
        public Task<ToDoItem> GetById(Int64 id);
        public Task<List<ToDoItem>> Add(ToDoItem item);
        public Task<List<ToDoItem>> Update(ToDoItem item);
        public Task<List<ToDoItem>> Delete(ToDoItem item);
    }
}