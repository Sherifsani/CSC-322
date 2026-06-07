using Simulation.Generics;

namespace Csc322.Tests;

public class SortingTests
{
    [Fact]
    public void Test1()
    {

    }

    [Fact]
    public void Sort_UnSortedInts_SortsAscending()
    {
        var sorting = new Sorting();
        var list = new List<int> { 1, 3, 4, 2, 5 };
        
        sorting.Sort(list, false);
        
        Assert.Equal(new List<int> {1, 2, 3, 4, 5}, list);
    }

    [Fact]
    public void Sort_AlreadySorted_RemainsUnchanged()
    {
        Sorting sorting = new();
        List<int> list = new List<int> { 1,2,3,4,5};
        
        sorting.Sort(list, false);
        
        Assert.Equal(new List<int> {1, 2, 3, 4, 5}, list);
    }
}
