using System.Runtime.Serialization;

namespace ToDoList.Maintenance.Models
{
    [DataContract]
    public class ToDoItem
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Items { get; set; }
        [DataMember]
        public int Priority { get; set; }
        [DataMember]
        public int Status { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }
        [DataMember]
        public DateTime CreatedDate { get; set; }
        [DataMember]
        public string LastUpdatedBy { get; set; }
        [DataMember]
        public DateTime LastUpdatedate { get; set; }

    }
}