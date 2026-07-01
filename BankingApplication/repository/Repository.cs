using System.Text.Json;
using BankingApplication.entity;

namespace BankingApplication.repository;

/// <summary>
/// Generic abstract repository providing JSON-file-based CRUD operations for entities implementing <see cref="IEntity"/>.
/// Each line in the data file is a JSON representation of one entity (NDJSON format).
/// </summary>
/// <typeparam name="T">The entity type, must implement <see cref="IEntity"/>.</typeparam>
public abstract class Repository<T> where T : IEntity
{
    private static readonly string DbDirectory = Path.GetFullPath(
        Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "repository", "db"));

    /// <summary>Gets the file name (e.g. "users.ndjson") used for persistence.</summary>
    protected abstract string FileName { get; }
    /// <summary>Gets the full file path combining the database directory and <see cref="FileName"/>.</summary>
    protected string FilePath => Path.Combine(DbDirectory, FileName);

    /// <summary>Initializes the repository and ensures the database directory exists.</summary>
    protected Repository()
    {
        Directory.CreateDirectory(DbDirectory);
    }
    
    /// <summary>Reads all entities from the file. Returns an empty list if the file does not exist.</summary>
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
    
    /// <summary>Overwrites the file with the JSON representation of every item in the list.</summary>
    /// <param name="items">The complete list of entities to persist.</param>
    protected void SaveAllRaw(List<T> items)
    {
        using var writer = new StreamWriter(FilePath, append: false);
        foreach (var item in items)
        {
            var jsonLine = JsonSerializer.Serialize(item);
            writer.WriteLine(jsonLine);
        }
    }
    
    /// <summary>Returns all entities.</summary>
    public virtual List<T> FindAll() => FindAllRaw();
    
    /// <summary>Finds an entity by its unique identifier, or default if not found.</summary>
    public virtual T FindById(string id) => FindAllRaw().FirstOrDefault(x => x.Id == id);

    /// <summary>Appends a new entity to the file.</summary>
    public virtual void Add(T item)
    {
        using var writer = new StreamWriter(FilePath, append: true);
        writer.WriteLine(JsonSerializer.Serialize(item));
    }

    /// <summary>Replaces an existing entity with the same <see cref="IEntity.Id"/>.</summary>
    public virtual void Update(T item)
    {
        List<T> items = FindAllRaw();
        var index = items.FindIndex(x => x.Id == item.Id);
        if (index >= 0)
        {
            items[index] = item;
            SaveAllRaw(items);
        }
    }

    /// <summary>Deletes the entity with the specified identifier.</summary>
    public virtual void delete(string id)
    {
        List<T> items = FindAllRaw();
        var filteredItems = items.Where(x => x.Id != id).ToList();
        SaveAllRaw(filteredItems);
    }
    
}