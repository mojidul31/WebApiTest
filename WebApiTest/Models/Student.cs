namespace WebApiTest.Models
{
    public class Student
    {
        public Guid StudentId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }
    }
}
