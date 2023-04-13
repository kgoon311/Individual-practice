using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionSort : Sort
{
    protected override void SortPattern()
    {
        for (int i = 0; i < count - 1; i++)
        {
            int minValue = i;
            for (int j = 0; j < count; j++)
            {
                if (array[i] > array[j])
                {
                    minValue = j;
                }
            }
            if(minValue != i)
                SwapArray(i, minValue);
        }
    }
}
