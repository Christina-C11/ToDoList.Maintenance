using System.Text.Json;
using log4net;
using Microsoft.EntityFrameworkCore;
using ToDoList.Maintenance.BusinessRules.Interface;
using ToDoList.Maintenance.DataAccess;
using ToDoList.Maintenance.Models;
using static ToDoList.Maintenance.Models.ItemEnum;

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

        public async Task<List<ToDoItem>> GetAll(GetToDoItem toDoItem)
        {
            try
            {
                //To get list form ToDoItem table
                var toDoItemsDB = await _context.ToDoItem.ToListAsync();
                var recordsPerPage = toDoItem.RecordPerPage;
                var toDoItems = toDoItemsDB.Select(item => item.ConvertToModel(item));
                //To select every ToDoItemDB item from toDoItems and convert it to ToDoItem
                //Return List of ToDoItem

                if(toDoItems.Count() > 0) {
                    if (!string.IsNullOrEmpty(toDoItem.SearchText))
                    {
                        var searchText = toDoItem.SearchText.ToLower();

                        toDoItems = toDoItems.Where(item => item.Title.ToLower().Contains(searchText)
                        || item.DueDate.ToString("dd-MM-yyyy").Contains(searchText)
                        || ((Priority)item.Priority).ToString().ToLower().Contains(searchText)
                        || ((Status)item.Status).ToString().ToLower().Contains(searchText)
                        );
                    }
                }
                return toDoItems.OrderBy(x => x.DueDate).Take(toDoItem.RecordPerPage).ToList();
            }
            catch(Exception ex)
            {
                log.Error(ex);
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
                log.Error(ex);
                throw;
            }  
        }

        public async Task<List<ToDoItem>> Add(ToDoItem toDoItem)
        {
            try
            {
                //Convert toDoItem from ToDoItem to ToDoItemDB object
                var toDoItemDB = toDoItem.ConvertToDbModel(toDoItem);
                
                //Add item
                _context.ToDoItem.Add(toDoItemDB);
                //Save and commit changes into table
                var result = await _context.SaveChangesAsync();

                //If there is result return list of ToDoItem; else return empty list;
                return (result > 0) ? await GetAll(toDoItem.GetToDoItem) : new List<ToDoItem>();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw;
            }
        }

        public async Task<List<ToDoItem>> Update(ToDoItem toDoItem)
        {
            try
            {
                var existingItem = await _context.ToDoItem.FirstOrDefaultAsync(i => i.Id == toDoItem.Id);

                //If does not have existing item
                if (existingItem == null) return new List<ToDoItem>();

                //Update properties
                existingItem.Title = toDoItem.Title;
                existingItem.Priority = toDoItem.Priority;
                existingItem.Items = JsonSerializer.Serialize<List<ItemDetail>>(toDoItem.ItemList);
                existingItem.DueDate = toDoItem.DueDate;
                existingItem.Status = toDoItem.Status;
                existingItem.LastUpdatedBy = toDoItem.LastUpdatedBy;
                existingItem.LastUpdatedDate = toDoItem.LastUpdatedDate;

                //Update item
                _context.ToDoItem.Update(existingItem);
                //Save and commit changes into table
                var result = await _context.SaveChangesAsync();

                //If there is result return list of ToDoItem; else return empty list;
                return (result > 0) ? await GetAll(toDoItem.GetToDoItem) : new List<ToDoItem>();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw;
            }
        }

        public async Task<List<ToDoItem>> Delete(ToDoItem toDoItem)
        {
            try
            {
                var itemToDelete = await _context.ToDoItem.FindAsync(toDoItem.Id);

                //If does not have item to delete
                if (itemToDelete == null) return new List<ToDoItem>();

                //Delete item
                _context.ToDoItem.Remove(itemToDelete);
                //Save and commit changes into table
                var result = await _context.SaveChangesAsync();

                //If there is result return list of ToDoItem; else return empty list;
                return (result > 0) ? await GetAll(toDoItem.GetToDoItem) : new List<ToDoItem>();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw;
            }
        }
    }
}