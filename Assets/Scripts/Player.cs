using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /*---------------------------------------------------------------------------------------------
    *  Attached to Player
    *  Holds Player Data
    *--------------------------------------------------------------------------------------------*/

    [Header("Variables")]
    public bool IsAI = false;

    int turnsNo;
    public int TurnsNo
    {
        get{ return turnsNo; }
        set
        {
            turnsNo = value;

            if (turnsNo > 3)
                turnsNo = 0;
        }
    }


    public void ResetPlayer()
    {
        turnsNo = 0;
    }
}
