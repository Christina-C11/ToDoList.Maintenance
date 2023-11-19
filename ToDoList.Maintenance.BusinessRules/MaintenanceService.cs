using log4net;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using ToDoList.Maintenance.BusinessRules.Interface;
using ToDoList.Maintenance.DataAccess;
using ToDoList.Maintenance.Models;

namespace ToDoList.Maintenance.BusinessRules
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly MaintenanceDbContext _context;
        private static readonly ILog log = LogManager.GetLogger(typeof(MaintenanceService));

        public MaintenanceService(MaintenanceDbContext context)
        {
            _context = context;
        }

        public async Task<List<ToDoItem>> GetAll()
        {
            try
            {
                //To get list form ToDoItem table
                var toDoItems = await _context.ToDoItem.ToListAsync();

                //To select every ToDoItemDB item from toDoItems and convert it to ToDoItem
                //Return List of ToDoItem
                return toDoItems.Select(item => item.ConvertToModel(item)).ToList();
            }
            catch(Exception ex)
            {
                log.Error(JsonSerializer.Serialize(ex));
                throw;
            }
        }

        public async Task<ToDoItem> GetById(Int64 id)
        {
            try
            {
                //To get list from ToDoItem table
                //To get the first matched ID result by searching id
                var toDoItemDb = (await _context.ToDoItem.ToListAsync()).FirstOrDefault(item => item.Id == id);

                //Convert toDoItemDb from ToDoItemDB to ToDoItem object
                var toDoItem = toDoItemDb.ConvertToModel(toDoItemDb);
                return toDoItem;
            }
            catch(Exception ex)
            {
                log.Error(JsonSerializer.Serialize(ex));
                throw;
            }  
        }

        public async Task<string> Add(ToDoItem toDoItem)
        {
            try
            {
                //Convert toDoItem from ToDoItem to ToDoItemDB object
                var toDoItemDB = toDoItem.ConvertToDbModel(toDoItem);
                _context.ToDoItem.Add(toDoItemDB);
                //Save and commit changes into table
                var result = await _context.SaveChangesAsync();

                //If there is result return Successful; else return Failed;
                return (result > 0) ? "Successful" : "Failed";
            }
            catch (Exception ex)
            {
                log.Error(JsonSerializer.Serialize(ex));
                throw;
            }
        }
    }
}