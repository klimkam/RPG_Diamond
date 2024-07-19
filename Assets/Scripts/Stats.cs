using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class Stats : MonoBehaviour
{
    const int STAT_MAX_VALUE = 16;
    const int STAT_MIN_VALUE = 4;

    static public int statPoints = 12;
    public enum Stat { PATT, PDEF, MATT, MDEF, HEALTH, MANA}
    static public float[] statsValue = { 10,10,10,10,10,10};
    static public float[] statsValueNormalized
    { get
        {
            float[] returnValues = { 0,0,0,0,0,0};
            for (int i = 0; i < returnValues.Length; i++) 
            {
                returnValues[i] = (statsValue[i] - 4)/12;
            }
            return returnValues; 
        }
    }

    static public string GetFullName(Stat stat)
    {
        string fullName = "";

        switch (stat)
        {
            case Stat.PATT :
                fullName = "P.Attack";
                break;
            case Stat.PDEF:
                fullName = "P.Defence";
                break;
            case Stat.MATT:
                fullName = "M.Attack";
                break;
            case Stat.MDEF:
                fullName = "M.Defence";
                break;
            case Stat.HEALTH:
                fullName = "Health";
                break;
            case Stat.MANA:
                fullName = "Mana";
                break;
        }

        return fullName;
    }

    public static bool TryIncreaseStat(Stat stat)
    {
        if (statsValue[((int)stat)] >= STAT_MAX_VALUE || (statPoints <= 0))
            return false; // failed

        statsValue[(int)stat] += 1;
        statPoints--;
        return true;
    }

    public static bool TryDecreaseStat(Stat stat)
    {
        if (statsValue[((int)stat)] <= STAT_MIN_VALUE)
            return false; // failed

        statsValue[(int)stat] -= 1;
        statPoints++;
        return true;
    }
}
