using System.Text.Json;

namespace ToDoList.Maintenance.Models
{

    public class ToDoItemBase
    {
        public Int64 Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Priority { get; set; }
        public DateTime DueDate { get; set; }
        public byte Status { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string LastUpdatedBy { get; set; } = string.Empty;
        public DateTime LastUpdatedDate { get; set; }
    }

    public class ToDoItem : ToDoItemBase
    {
        public List<ItemDetail>? ItemList { get; set; } = new List<ItemDetail>();
        public GetToDoItem GetToDoItem { get; set; }
        public ToDoItemDB ConvertToDbModel(ToDoItem item)
        {
            return new ToDoItemDB
            {
                Id = this.Id,
                Title = this.Title,
                Priority = this.Priority,
                Items = JsonSerializer.Serialize<List<ItemDetail>>(item.ItemList) ?? string.Empty,
                DueDate = this.DueDate,
                Status = this.Status,
                CreatedBy = this.CreatedBy,
                CreatedDate = this.CreatedDate,
                LastUpdatedBy = this.LastUpdatedBy,
                LastUpdatedDate = this.LastUpdatedDate
            };
        }
    }

    public class ToDoItemDB : ToDoItemBase
    {
        public string Items { get; set; } = string.Empty;
        public ToDoItem ConvertToModel(ToDoItemDB item)
        {
            return new ToDoItem
            {
                Id = this.Id,
                Title = this.Title,
                Priority = this.Priority,
                ItemList = JsonSerializer.Deserialize<List<ItemDetail>>(item.Items) ?? new List<ItemDetail>(),
                DueDate = this.DueDate,
                Status = this.Status,
                CreatedBy = this.CreatedBy,
                CreatedDate = this.CreatedDate,
                LastUpdatedBy = this.LastUpdatedBy,
                LastUpdatedDate = this.LastUpdatedDate
            };
        }
    }

    public class GetToDoItem
    {
        public int PageIndex { get; set; }
        public int? TotalPageCount { get; set; }
        public int RecordPerPage { get; set; }
        public List<ToDoItem>? ToDoItems { get; set; } = new List<ToDoItem>();
        public string? SearchText { get; set; } = string.Empty;
    }
}