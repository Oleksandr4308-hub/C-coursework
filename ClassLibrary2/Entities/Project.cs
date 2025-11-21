namespace DAL.Entities;
public class Project
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Cost { get; set; }
    public Project() { }
}
