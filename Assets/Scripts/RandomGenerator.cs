using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    /*---------------------------------------------------------------------------------------------
    *  Attached to GameManager.
    *  generates random number
    *--------------------------------------------------------------------------------------------*/

    public int GetRandomInt(int min, int max)
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        return Random.Range(min, max + 1);

    }
}
