using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities
{
    public static bool ArrayContains<T>(T[] array, T value)
    {
        foreach(T arrayValue in array)
        {
            if (EqualityComparer<T>.Default.Equals(arrayValue, value)) return true;
        }

        return false;
    }

    public static float RadiansToDegrees(float radians)
    {
        return radians * 180 / Mathf.PI;
    }
}
