//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

static class Consts
{
    public const float leftBorder=-18f;
    public const float rightBorder=18f;
    public const float upBorder=0f;
    public const float downBorder=-12f;
    public const float distFromPlayer=2f;
    public const float overlayHeavyMetal = 100f;
}

static class MyUtils
{
    public static int sign(float a)
    {
        if(a > 0f) return 1;
        else if(a < 0f) return -1;
        else return 0;
    }
    public static bool randomBool()
    {
        return Random.value > 0.5;
    }
}
