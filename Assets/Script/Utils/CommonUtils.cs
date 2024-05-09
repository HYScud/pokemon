using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonUtils
{
    public static Dictionary<long, Vector3> AllUnreachablePos = new Dictionary<long, Vector3>();

    public static void CorrectPosition(Vector3 pos)
    {
        pos.x = Mathf.Round(pos.x-0.5f);
        pos.y = Mathf.Round(pos.y - 0.5f);
        pos.z = Mathf.Round(pos.z);
    }
    public static bool CheckIsReachable(Vector3 targetPos)
    {
        foreach (KeyValuePair<long, Vector3> kv in AllUnreachablePos)
        {

            if (kv.Value.Equals(targetPos))
            {
                return false;
            }
        }
        return true;
    }

    public static void SaveCharacterPos(long id,Vector3 targetPos)
    {
        if (!AllUnreachablePos.ContainsKey(id))
        {
            AllUnreachablePos.Add(id, targetPos);
        }
        else
        {
            AllUnreachablePos[id] = targetPos;
        }
    }
}
