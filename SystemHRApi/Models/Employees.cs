namespace SystemHRApi.Models
{
    public class Employees
    {
        public long Id { get; set; }
        public string? Name { get; set; }    
        public string? Surname { get; set; }
        public string? Adress { get; set; }
        public long NumberPhone { get; set; }
        public  string? DateOfBirth { get; set; }
        public string? DateOfEmployment { get; set; }
        public string? Contract { get; set; }
    }
}
