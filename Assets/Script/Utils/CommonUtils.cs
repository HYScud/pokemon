using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonUtils
{
    public static void CorrectPosition(Vector3 pos)
    {
        pos.x = Mathf.Round(pos.x);
        pos.y = Mathf.Round(pos.y);
        pos.z = Mathf.Round(pos.z);
    }
}
