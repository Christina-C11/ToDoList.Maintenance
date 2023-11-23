namespace ToDoList.Maintenance.Models
{
    public class ItemEnum
    {
        public enum Priority
        {
            Low = 1,
            Medium = 2,
            High = 3
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
