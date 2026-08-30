namespace BankingApplication.Tests;

/// <summary>
/// Helpers for working with the NDJSON files the repositories persist to.
/// </summary>
/// <remarks>
/// The repositories resolve their data directory relative to <see cref="AppContext.BaseDirectory"/>
/// (<c>bin/&lt;config&gt;/&lt;tfm&gt;/../../../repository/db</c>), so when the code runs from this test
/// assembly it reads and writes <c>BankingApplication.Tests/repository/db</c> — never the files the
/// console application uses. Tests only need to clear those files between runs.
/// </remarks>
public static class TestDatabase
{
    /// <summary>The directory the repositories use while the tests are running.</summary>
    public static string Directory { get; } = Path.GetFullPath(
        Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "repository", "db"));

    private static readonly string[] Files = ["users.ndjson", "accounts.ndjson", "transactions.ndjson"];

    /// <summary>Deletes every data file so the next test starts from an empty database.</summary>
    public static void Reset()
    {
        System.IO.Directory.CreateDirectory(Directory);
        foreach (var file in Files)
        {
            var path = Path.Combine(Directory, file);
            if (File.Exists(path)) File.Delete(path);
        }
    }

    /// <summary>Returns the raw lines of one data file, or an empty array when it does not exist.</summary>
    public static string[] ReadLines(string fileName)
    {
        var path = Path.Combine(Directory, fileName);
        return File.Exists(path) ? File.ReadAllLines(path) : [];
    }
}

/// <summary>
/// Base class that gives every test a clean database.
/// </summary>
public abstract class BankingTestBase : IDisposable
{
    /// <summary>Clears the data files before the test body runs.</summary>
    protected BankingTestBase() => TestDatabase.Reset();

    /// <summary>Clears the data files again so nothing is left behind for the next test.</summary>
    public void Dispose() => TestDatabase.Reset();
}
