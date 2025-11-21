namespace BLL.Models;
public class PositionModel {
    public int Id { get; set; }
    public string Title { get; set; } = ""; 
    public decimal Salary { get; set; }
    public int WorkHours { get; set; }
}
