namespace DAL.Entities;
public class Worker
{
    public int Id { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string SalaryAccountNumber { get; set; } = "";
    public string DepartmentName { get; set; } = "";
    public string PositionName { get; set; } = "";
    public int WorkExperienceYears { get; set; }
    public List<int> ProjectIds { get; set; } = new();
    public Worker() { }
}