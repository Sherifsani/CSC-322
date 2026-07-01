namespace Simulation.Generics;

/// <summary>
/// Provides generic sorting functionality using insertion sort algorithm.
/// </summary>
public class Sorting
{
    /// <summary>
    /// Sorts the elements in a list using the insertion sort algorithm.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list, which must implement <see cref="IComparable{T}"/>.</typeparam>
    /// <param name="listT">The list to sort. The list is modified in place.</param>
    /// <param name="reverse">If <c>true</c>, sorts in descending order; otherwise, sorts in ascending order.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="listT"/> is null.</exception>
    public void Sort<T>(List<T> listT, bool reverse) where T : IComparable<T>
    {
        ArgumentNullException.ThrowIfNull(listT);

        for (int i = 1; i < listT.Count; i++)
        {
            T key = listT[i];
            int j = i - 1;

            while (j >= 0 && Compare(listT[j], key, reverse) > 0)
            {
                listT[j + 1] = listT[j];
                j -= 1;
            }
            listT[j + 1] = key;
        }
    }

    private static int Compare<T>(T x, T y, bool reverse) where T : IComparable<T>
    {
        int result = x.CompareTo(y);
        return reverse ? -result : result;
    }
}