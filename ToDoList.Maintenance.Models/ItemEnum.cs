namespace ToDoList.Maintenance.Models
{
    public class ItemEnum
    {
        public enum Priority
        {
            Low = 0,
            Medium = 1,
            High = 2
        }

        public enum Status
        {
            Active = 0,
            Inactive = 1,
            Completed = 2,
            Expired = 3
        }
    }
}
