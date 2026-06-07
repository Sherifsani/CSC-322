using System;
using System.Collections.Generic;

namespace Simulation.Generics;

public class Sorting
{
    public void Sort<T>(List<T> listT, bool reverse) where T : IComparable<T>
    {
        for (int i = 1; i < listT.Count; i++)
        {
            T key = listT[i];
            int j = i - 1;

            while (j >= 0 && listT[j].CompareTo(key) > 0)
            {
                listT[j + 1] = listT[j];
                j -= 1;
            }
            listT[j + 1] = key;
        }
    }
}