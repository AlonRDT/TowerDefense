using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    public static int WaveNumber { get; private set; }
    public static int MonsterHealthMultiplier { get; private set; }
    public static readonly int CannonPrice = 50;
    public static readonly int BeamPrice = 100;
    public static readonly int AOEPrice = 200;
    public static readonly float BaseSpeed = 3.5f;
    public static readonly float FreezeSpeed = 1.5f;
    public static readonly float FreezeDelay = 1f;

    public static void Reset()
    {
        MonsterHealthMultiplier = 1;
        WaveNumber = 0;
    }

    public static void IncreaseWave()
    {
        WaveNumber++;
        if(WaveNumber - 1 % 5 == 2)
        {
            increaseMinionHealth();
        }
    }

    private static void increaseMinionHealth()
    {
        MonsterHealthMultiplier++;
    }
}
