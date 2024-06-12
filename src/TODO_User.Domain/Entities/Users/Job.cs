namespace TODO_User.Domain.Entities.Users
{
    public class Job
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime LastUpdated { get; set; }
     
    }
}
