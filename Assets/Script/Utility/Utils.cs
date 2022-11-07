using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static void Log(object Message, IDebug debug)
    {
#if UNITY_EDITOR
        if (debug.Debug == false) return;
        Debug.Log(Message);
#endif
    }
    public static void LogWarning(object Message, IDebug debug)
    {
#if UNITY_EDITOR
        if (debug.Debug == false) return;
        Debug.LogWarning(Message);
#endif
    }

    public static void LogError(object Message, IDebug debug)
    {
#if UNITY_EDITOR
        if (debug.Debug == false) return;
        Debug.LogError(Message);
#endif
    }
    public static float Scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {

        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }

    public static Vector3 VectorAbs(in Vector3 vector3)
    {
        return new Vector3(Mathf.Abs(vector3.x), Mathf.Abs(vector3.y), Mathf.Abs(vector3.z));
    }
}