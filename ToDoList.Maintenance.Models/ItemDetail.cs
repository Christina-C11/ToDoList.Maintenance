namespace ToDoList.Maintenance.Models
{
    public class ItemDetail
    {
        public string ItemId { get; set; } = string.Empty; //will be using uuid
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string LastUpdatedBy { get; set; } = string.Empty;
        public DateTime LastUpdatedDate { get; set; }
    }
}
