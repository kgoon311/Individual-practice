using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sort : MonoBehaviour
{
    protected int[] array;
    [SerializeField] protected int count;

    private void Start()
    {
        array = new int[count];
        for (int i = 0; i < count; i++)
        {
            array[i] = i;
        }
        ShuffleArray(10);
        SortPattern();
    }
    private void ShuffleArray(int shuffleCount)
    {
        for (int i = 0; i < shuffleCount; i++)
        {
            int random = Random.Range(0, count);
            int random2 = Random.Range(0, count);

            SwapArray(random, random2);
        }
    }
    protected void SwapArray(int idx1,int idx2)
    {
        int temp = array[idx1];
        array[idx1] = array[idx2];
        array[idx2] = temp;
    }
    protected abstract void SortPattern();
}
