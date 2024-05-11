using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PokemonTable
{

    public static float[,] typeChart;

    public static float[,] NatureChart =new float[3,3];

    public static float[][] typeChart1 = new float[3][];

    public static float GetNatureEffect(int index, NatrueTypeEnum natrueTypeEnum)
    {
        if (NatureChart == null)
        {
            Debug.LogError("��ȡ�Ը�����ʧ��");
            return 0;
        }
        return NatureChart[index, (int)natrueTypeEnum];
    }
}
