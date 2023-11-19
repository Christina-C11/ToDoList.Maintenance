using Microsoft.EntityFrameworkCore;
using ToDoList.Maintenance.BusinessRules.Interface;
using ToDoList.Maintenance.DataAccess;
using ToDoList.Maintenance.Models;

namespace ToDoList.Maintenance.BusinessRules
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly MaintenanceDbContext _context;

        public MaintenanceService(MaintenanceDbContext context)
        {
            _context = context;
        }

        public async Task<List<ToDoItem>> GetAll()
        {
            var toDoItems = await _context.ToDoItem.ToListAsync();
            return toDoItems.Select(item => item.ConvertToModel(item)).ToList(); 
        }

        public async Task<ToDoItem> GetById(Int64 id)
        {
            var toDoItemDb = (await _context.ToDoItem.ToListAsync()).FirstOrDefault(item => item.Id == id);
            var toDoItem = toDoItemDb.ConvertToModel(toDoItemDb);
            return toDoItem;
        }

        public async Task<string> Add(ToDoItem toDoItem)
        {
            var toDoItemDB = toDoItem.ConvertToDbModel(toDoItem);
            _context.ToDoItem.Add(toDoItemDB);
            var result = await _context.SaveChangesAsync();

            if(result > 0)
            {
                return "Successful";
            }
            else
            {
                return "Failed";
            }
        }
    }
}