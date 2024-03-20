using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int[] array = new int[3];


    private void Start()
    {
        RandomNum();
    }
    public void RandomNum()
    {

        array[0] = Random.Range(0, 10);

        for (int i = 1; i < 3; i++)
        {
            int Num = Random.Range(1, 10);

            if (Num == array[i - 1])
            {

            }

            for (int j = 0; j < i; j++)
            {
                if (Num != array[j])
                {
                    ;
                }

            }


        }




    }





}