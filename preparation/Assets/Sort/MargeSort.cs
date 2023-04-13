using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class MargeSort : Sort
{
    protected override void SortPattern()
    {
        array = MergeSort(array, 0, array.Length);//1 5 2 5 1 8 8 7 ,1,7
    }

    private int[] MergeSort(int[] array, int left, int right)
    {
        //1자리까지 나누어 졌을 때 재귀 종료
        if (left == right)
            return new int[] { array[left] };

        int middle = left + (right - left) / 2;

        //재귀 형식으로 반복
        int[] array1 = MergeSort(array, left, middle);
        int[] array2 = MergeSort(array, middle + 1, right);

        //정렬을 앞에서부터 하기 위해 사용
        int arrayCount1 = 0;
        int arrayCount2 = 0;

        int[] returnArray = new int[right - left + 1];
        for (int i = 0; i < returnArray.Length; i++)
        {
            //왼쪽 배열에 값이 남아있고, 가장 작은 값이 오른쪽 노드의 가장 작은 값보다 작으면
            if (arrayCount1 < array1.Length && (arrayCount2 >= array2.Length || array1[arrayCount1] < array2[arrayCount2]))
                returnArray[i] = array1[arrayCount1++];
            else
                returnArray[i] = array2[arrayCount2++];
        }
        return returnArray;
    }

}
