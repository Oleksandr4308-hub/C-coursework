using BLL.Models;
namespace BLL.Interfaces;
public interface IPositionService
{
    void AddPosition(PositionModel p);
    void UpdatePosition(PositionModel p);
    PositionModel? GetPosition(int id);
    IEnumerable<PositionModel> Top5AttractivePositions();
    WorkerModel? MostProfitableWorkerOnPosition(string positionName);
}
