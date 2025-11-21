using System.Collections.Generic;
namespace DAL.Json;
public interface IDataProvider<T>
{
    void Save(string path, List<T> items);
    List<T> Load(string path);
}
