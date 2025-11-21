using System.Collections.Generic;
using System.IO;
using System.Text.Json;
namespace DAL.Json;
public class JsonProvider<T> : IDataProvider<T>
{
    private JsonSerializerOptions Options => new() { WriteIndented = true, PropertyNameCaseInsensitive = true };
    public List<T> Load(string path)
    {
        if (!File.Exists(path)) return new List<T>();
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<T>>(json, Options) ?? new List<T>();
    }
    public void Save(string path, List<T> items)
    {
        var dir = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir)) Directory.CreateDirectory(dir);
        var json = JsonSerializer.Serialize(items, Options);
        File.WriteAllText(path, json);
    }
}
