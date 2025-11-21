using DAL.Interfaces;
using DAL.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace DAL.Repositories;
public class FileRepository<T> : IRepository<T> where T : class
{
    private readonly string _path;
    private readonly IDataProvider<T> _provider;
    private readonly List<T> _items;
    private readonly Func<T, int> _getId;
    private readonly Action<T, int> _setId;
    public FileRepository(string path, IDataProvider<T> provider, Func<T, int> getId, Action<T, int> setId)
    {
        _path = path;
        _provider = provider;
        _items = _provider.Load(path);
        _getId = getId;
        _setId = setId;
    }
    public IEnumerable<T> GetAll() => _items.ToList();
    public T? GetById(int id) => _items.FirstOrDefault(x => _getId(x) == id);
    public void Add(T entity)
    {
        var id = _getId(entity);
        if (id == 0)
        {
            var newId = _items.Any() ? _items.Max(_getId) + 1 : 1;
            _setId(entity, newId);
        }
        else if (_items.Any(x => _getId(x) == id))
            throw new InvalidOperationException("Entity already exists");
        _items.Add(entity);
    }
    public void Update(T entity)
    {
        var id = _getId(entity);
        var idx = _items.FindIndex(x => _getId(x) == id);
        if (idx == -1) throw new InvalidOperationException("Entity not found");
        _items[idx] = entity;
    }
    public void Delete(int id)
    {
        var item = GetById(id);
        if (item == null) throw new InvalidOperationException("Entity not found");
        _items.Remove(item);
    }
    public void Save() => _provider.Save(_path, _items);
}
static class ListExtensions
{
    public static int FindIndex<T>(this List<T> list, Predicate<T> match)
    {
        for (int i = 0; i < list.Count; i++) if (match(list[i])) return i;
        return -1;
    }
}
