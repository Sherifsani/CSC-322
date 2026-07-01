using Simulation.Generics;

namespace Csc322.Tests;

public class SortingTests
{
    private readonly Sorting _sorting = new();

    [Fact]
    public void Sort_UnSortedIntsAscending_SortsCorrectly()
    {
        var list = new List<int> { 1, 3, 4, 2, 5 };
        _sorting.Sort(list, false);
        Assert.Equal(new List<int> { 1, 2, 3, 4, 5 }, list);
    }

    [Fact]
    public void Sort_UnSortedIntsDescending_SortsCorrectly()
    {
        var list = new List<int> { 1, 3, 4, 2, 5 };
        _sorting.Sort(list, true);
        Assert.Equal(new List<int> { 5, 4, 3, 2, 1 }, list);
    }

    [Fact]
    public void Sort_AlreadySortedAscending_RemainsUnchanged()
    {
        var list = new List<int> { 1, 2, 3, 4, 5 };
        _sorting.Sort(list, false);
        Assert.Equal(new List<int> { 1, 2, 3, 4, 5 }, list);
    }

    [Fact]
    public void Sort_AlreadySortedDescending_RemainsUnchanged()
    {
        var list = new List<int> { 5, 4, 3, 2, 1 };
        _sorting.Sort(list, true);
        Assert.Equal(new List<int> { 5, 4, 3, 2, 1 }, list);
    }

    [Fact]
    public void Sort_EmptyList_DoesNothing()
    {
        var list = new List<int>();
        _sorting.Sort(list, false);
        Assert.Empty(list);
    }

    [Fact]
    public void Sort_SingleElement_RemainsUnchanged()
    {
        var list = new List<int> { 42 };
        _sorting.Sort(list, false);
        Assert.Equal(new List<int> { 42 }, list);
    }

    [Fact]
    public void Sort_ListWithDuplicatesAscending_SortsCorrectly()
    {
        var list = new List<int> { 3, 1, 2, 1, 3 };
        _sorting.Sort(list, false);
        Assert.Equal(new List<int> { 1, 1, 2, 3, 3 }, list);
    }

    [Fact]
    public void Sort_ListWithDuplicatesDescending_SortsCorrectly()
    {
        var list = new List<int> { 3, 1, 2, 1, 3 };
        _sorting.Sort(list, true);
        Assert.Equal(new List<int> { 3, 3, 2, 1, 1 }, list);
    }

    [Fact]
    public void Sort_ReverseSortedAscending_SortsCorrectly()
    {
        var list = new List<int> { 5, 4, 3, 2, 1 };
        _sorting.Sort(list, false);
        Assert.Equal(new List<int> { 1, 2, 3, 4, 5 }, list);
    }

    [Fact]
    public void Sort_AscendingSortedDescending_SortsCorrectly()
    {
        var list = new List<int> { 1, 2, 3, 4, 5 };
        _sorting.Sort(list, true);
        Assert.Equal(new List<int> { 5, 4, 3, 2, 1 }, list);
    }

    [Fact]
    public void Sort_NegativeNumbersAscending_SortsCorrectly()
    {
        var list = new List<int> { -5, 0, 3, -2, 1 };
        _sorting.Sort(list, false);
        Assert.Equal(new List<int> { -5, -2, 0, 1, 3 }, list);
    }

    [Fact]
    public void Sort_NegativeNumbersDescending_SortsCorrectly()
    {
        var list = new List<int> { -5, 0, 3, -2, 1 };
        _sorting.Sort(list, true);
        Assert.Equal(new List<int> { 3, 1, 0, -2, -5 }, list);
    }

    [Fact]
    public void Sort_StringsAscending_SortsAlphabetically()
    {
        var list = new List<string> { "banana", "apple", "cherry", "date" };
        _sorting.Sort(list, false);
        Assert.Equal(new List<string> { "apple", "banana", "cherry", "date" }, list);
    }

    [Fact]
    public void Sort_StringsDescending_SortsReverseAlphabetically()
    {
        var list = new List<string> { "banana", "apple", "cherry", "date" };
        _sorting.Sort(list, true);
        Assert.Equal(new List<string> { "date", "cherry", "banana", "apple" }, list);
    }

    [Fact]
    public void Sort_DoublesAscending_SortsCorrectly()
    {
        var list = new List<double> { 3.14, 1.41, 2.72, 1.61 };
        _sorting.Sort(list, false);
        Assert.Equal(new List<double> { 1.41, 1.61, 2.72, 3.14 }, list);
    }

    [Fact]
    public void Sort_NullList_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _sorting.Sort<int>(null!, false));
    }
}
