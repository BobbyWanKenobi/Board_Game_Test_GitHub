using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DiceController : MonoBehaviour
{
    /*---------------------------------------------------------------------------------------------
    *  Attached to DiceController
    *  Manages Dices
    *--------------------------------------------------------------------------------------------*/

    public static DiceController inst = null;

    [Header("References")]
    [SerializeField] GameObject Dice_6;
    [SerializeField] GameObject Dice_10;

    [SerializeField] Ease easeType;

    [Header("Variables")]
    bool Dice_5_10 = false;
    bool DiceReady = true;

    //EVENTS
    public delegate void DiceDone(int diceVal);
    public static event DiceDone diceDone;

    /*---------------------------------------------------------------------------------------------
    *  rotations for result:
    *  1 - 5 (-90, -180, 0)
    *  2 - 8 (180, -90, 0)
    *  3 - 7 (0, -180, -90)
    *  4 - 8 (0, -180, 90)
    *  5 - 9 (0, 0, 0)
    *  6 - 10 (90, 90, 0)
    *--------------------------------------------------------------------------------------------*/
    Vector3[] diceRotAngles = {  new Vector3 { x = -90, y = -180, z = 0 },
                                 new Vector3 { x = 180, y = -90, z = 0},
                                 new Vector3 { x = 0, y = -180, z = -90},
                                 new Vector3 { x = 0, y = -180, z = 90},
                                 new Vector3 { x = 0, y = 0, z = 0 },
                                 new Vector3 { x = 90, y = 90, z = 0 } };


    private void Awake()
    {
        if (inst == null)
            inst = this;
        else
            Destroy(this.gameObject);
    }

    /// <summary>Set Dice Type 1-6 or 5-10</summary>
    /// <remarks></remarks>
    public void Set_Dice_Type(bool dice_5_10)
    {
        DEBUGger.inst?.Set_Debug_Right("DiceController. Set_Dice_Type, Dice_5_10: " + Dice_5_10 + "  > dice_5_10: " + dice_5_10 + "  > ActivePlayer: " + GameManager.inst.ActivePlayer, Color.blue);
        if (Dice_5_10 != dice_5_10)
            StartCoroutine(Swap_Dice(1.0f, 0.1f));
        Dice_5_10 = dice_5_10;
    }

    /// <summary>Change dices on every 3 moves</summary>
    /// <remarks></remarks>
    IEnumerator Swap_Dice(float duration, float delay = 0)
    {
        DEBUGger.inst?.Set_Debug_Right("DiceController. Swap_Dice, Dice_5_10: " + Dice_5_10 + "  > ActivePlayer: " + GameManager.inst.ActivePlayer, Color.blue);
        DiceReady = false;

        yield return new WaitForSeconds(delay);

        if (Dice_5_10)
        {
            Dice_10.transform.DOScale(Vector3.one, duration)
                .SetEase(easeType)
                .OnComplete(() => {
                    //executes whenever Pawn reach target position
                    DiceReady = true;
                });
            Dice_6.transform.DOScale(Vector3.one * 0.1f, duration);
        }
        else
        {
            Dice_6.transform.DOScale(Vector3.one, duration)
                .SetEase(easeType)
                .OnComplete(() => {
                    //executes whenever Pawn reach target position
                    DiceReady = true;
                });
            Dice_10.transform.DOScale(Vector3.one * 0.1f, duration);
        }
    }

    /// <summary>Set dice number with Delay</summary>
    /// <remarks></remarks>
    public IEnumerator Set_Dice_Rotation_Delayed(int diceVal, float delay)
    {
        yield return new WaitForSeconds(delay);

        Set_Dice_Rotation(diceVal);
    }

    /// <summary>Set dice number</summary>
    /// <remarks></remarks>
    public void Set_Dice_Rotation(int diceVal)
    {
        if (!DiceReady)
            return;
        else
            UImanager.inst.Show_Button_RollDice(false);

        Debug.Log("Requested result: " + diceVal);
        
        //Adds randomnes to the dice rotation 
        transform.rotation = new Quaternion(Random.Range(-3f, 3f), Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0);

        //Rotates dice to desired result
        transform.DOLocalRotate(diceRotAngles[diceVal - 1] + new Vector3(1080,0,0), 3f, RotateMode.FastBeyond360)
                .SetEase(easeType)
                .OnComplete(() => {
                    //executes whenever Pawn reach target position
                    StartCoroutine(Delay_Result(1.0f, diceVal));
                });
    }

    IEnumerator Delay_Result(float delay, int diceVal)
    {
        yield return new WaitForSeconds(delay);

        //Increment result for bigger dice
        if (Dice_5_10)
            diceVal += 4;

        diceDone(diceVal);
    }

    /// <summary>Reset dices to start condition</summary>
    /// <remarks></remarks>
    public void ResetDice()
    {
        Set_Dice_Type(false);
        Dice_6.transform.localScale = Vector3.one;
        Dice_10.transform.localScale = Vector3.one * 0.1f;
    }
}
