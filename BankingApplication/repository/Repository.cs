using System.Text.Json;
using BankingApplication.entity;

namespace BankingApplication.repository;

public abstract class Repository<T> where T : IEntity
{
    protected abstract string FilePath { get; }
    
    
    /*
     * constructor is kept hidden from other classes following a singleton approach
     * Only one repository<T> should exist in memory to prevent two or more threads from modifying the database
     */
    protected Repository(){}
    
    // helper method to get all from the database and load them into memory
    protected List<T> FindAllRaw()
    {
        if (!File.Exists(FilePath)) return new List<T>();
        var items = new List<T>();
        foreach (string line in File.ReadLines(FilePath))
        {
            if(string.IsNullOrWhiteSpace(line)) continue;
            var item = JsonSerializer.Deserialize<T>(line);
            if(item != null) items.Add((item));
        }

        return items;
    }
    
    //helper method to save records into the database file (it overwrites each time though)
    protected void SaveAllRaw(List<T> items)
    {
        using var writer = new StreamWriter(FilePath, append: false);
        foreach (var item in items)
        {
            var jsonLine = JsonSerializer.Serialize(item);
            writer.WriteLine(jsonLine);
        }
    }
    
    public virtual List<T> FindAll() => FindAllRaw();
    
    public virtual T FindById(string id) => FindAllRaw().FirstOrDefault(x => x.Id == id);

    public virtual void Add(T item)
    {
        using var writer = new StreamWriter(FilePath, append: true);
        writer.WriteLine(JsonSerializer.Serialize(item));
    }

    public virtual void delete(string id)
    {
        List<T> items = FindAllRaw();
        var filteredItems = items.Where(x => x.Id != id).ToList();
        SaveAllRaw(filteredItems);
    }
    
}