using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DiceController : MonoBehaviour
{
    /*---------------------------------------------------------------------------------------------
    *  Attached to Canvas_UI
    *  Manages Game
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set_Dice_Type(bool dice_5_10)
    {
        DEBUGger.inst?.Set_Debug_Left("DiceController. Set_Dice_Type, Dice_5_10: " + Dice_5_10 + "  > dice_5_10: " + dice_5_10, Color.blue);
        if (Dice_5_10 != dice_5_10)
            StartCoroutine(Swap_Dice(1.0f, 0.1f));
        Dice_5_10 = dice_5_10;
    }

    IEnumerator Swap_Dice(float duration, float delay = 0)
    {
        DEBUGger.inst?.Set_Debug_Left("DiceController. Swap_Dice, Dice_5_10: " + Dice_5_10, Color.blue);
        DiceReady = false;

        yield return new WaitForSeconds(delay);

        // keep track of when the scaling started, when it should finish, and how long it has been running
        var startTime = Time.time;
        var endTime = Time.time + duration;
        var elapsedTime = 0f;

        // loop repeatedly until the previously calculated end time
        while (Time.time <= endTime )
        {
            elapsedTime = Time.time - startTime; // update the elapsed time
            var percentage = 1 / (duration / elapsedTime); // calculate how far along the timeline we are
            if (Dice_5_10) // if we are fading out
            {
                Dice_6.transform.localScale = Vector3.one * percentage;
                Dice_10.transform.localScale = Vector3.one * (1 - percentage);
            }
            else // if we are fading in/up
            {
                Dice_6.transform.localScale = Vector3.one * (1 - percentage);
                Dice_10.transform.localScale = Vector3.one * percentage;
            }

            yield return new WaitForEndOfFrame(); // wait for the next frame before continuing the loop
        }

        DiceReady = true;
    }

    public IEnumerator Set_Dice_Rotation_Delayed(int diceVal, float delay)
    {
        yield return new WaitForSeconds(delay);

        Set_Dice_Rotation(diceVal);
    }

    public void Set_Dice_Rotation(int diceVal)
    {
        if (!DiceReady)
            return;
        else
            UImanager.inst.Show_Button_RollDice(false);

        Debug.Log("Requested result: " + diceVal);

        transform.rotation = new Quaternion(Random.Range(-3f, 3f), Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0);

        transform.DOLocalRotate(diceRotAngles[diceVal - 1] + new Vector3(1440,0,0), 4f, RotateMode.FastBeyond360)
                .SetEase(easeType)
                .OnComplete(() => {
                    //executes whenever Pawn reach target position
                    StartCoroutine(Delay_Result(1.0f, diceVal));
                });
    }

    IEnumerator Delay_Result(float delay, int diceVal)
    {
        yield return new WaitForSeconds(delay);

        if (Dice_5_10)
            diceVal += 4;

        diceDone(diceVal);
    }

    public void ResetDice()
    {
        Set_Dice_Type(false);
    }
}
