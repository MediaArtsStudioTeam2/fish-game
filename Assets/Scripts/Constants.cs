//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

static class Consts
{
    public const float leftBorder=-34f;
    public const float rightBorder=34f;
    public const float upBorder=0f;
    public const float downBorder=-18f;
    public const float overlayHeavyMetal = 100f;

    public static int ObstacleLayer = LayerMask.GetMask("Obstacles", "Border");
    public static int FishLayer = LayerMask.GetMask("Fish", "OtherFish");

    public const float detectWallRange=3f;
    public const float chaseRange=7f;
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
    public static int boolToSign(bool b)
    {
        return b ? 1 : -1;
    }
}
