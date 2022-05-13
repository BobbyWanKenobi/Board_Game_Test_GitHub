using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    /*---------------------------------------------------------------------------------------------
    *  Attached to GameManager.
    *  generates random number
    *--------------------------------------------------------------------------------------------*/

    /// <summary>generates random int in range.</summary>
    /// <remarks>Seed is decided bu monet of call</remarks>
    public int GetRandomInt(int min, int max)
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        return Random.Range(min, max + 1);

    }
}
