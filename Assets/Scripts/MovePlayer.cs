using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovePlayer : MonoBehaviour
{
    /*---------------------------------------------------------------------------------------------
    *  Attached to Player.
    *  Manage Player motion
    *--------------------------------------------------------------------------------------------*/

    [Header("References")]
    [SerializeField] GameObject Pawn;

    [Header("Variables")]
    [SerializeField] Ease easeType;
    [SerializeField] float Single_Move_Duration = 0.6f;
    [Space]
    int currentPos = 0;
    int nextPos = 0;
    int targetPos;
    bool moveINProgress = false;
    int fieldsCount;
    float pos_Z_shift;

    //EVENTS
    public delegate void GameOver();
    public static event GameOver gameOver;
    public delegate void NextPlayer();
    public static event NextPlayer nextPlayer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move_Player();
    }

    void Move_Player()
    {
        if (moveINProgress == false && currentPos != targetPos)
        {
            Next_Step();
        }

        Animate_Pawn();
    }

    void Animate_Pawn()
    {
        if (currentPos != targetPos)
        {
            //Animates Y motion
            float tmpPawnDistance = Vector3.Distance(transform.position, Board.inst.Get_Field_Pos(nextPos));
            float pawnDistance = Vector3.Distance(Board.inst.Get_Field_Pos(currentPos), Board.inst.Get_Field_Pos(nextPos));
            float yPos = Mathf.Sin((tmpPawnDistance / pawnDistance) * (Mathf.PI)) * 0.7f;
            Pawn.transform.localPosition = new Vector3(0.0f, yPos, pos_Z_shift);
        }
        else
        {
            Pawn.transform.localPosition = new Vector3(0.0f, 0, pos_Z_shift);
        }
    }

    /// <summary>Sets move Target</summary>
    /// <remarks></remarks>
    public void Set_Player_Move(int diceVal)
    {
        if (fieldsCount == 0)
            fieldsCount = Board.inst.Get_Fields_Count();

        targetPos = currentPos + diceVal;

        //Check for end of board
        if (targetPos >= fieldsCount)
        {
            targetPos = fieldsCount - 1;
        }

        nextPos = currentPos + 1;
        moveINProgress = false;

        DEBUGger.inst?.Set_Debug_Left($"Set_Pawn_Target, diceVal: {diceVal}  > currentPos {currentPos}  > targetPos {targetPos}  > fieldsCount {fieldsCount}" , Color.yellow);
    }

    void Next_Step()
    {
        DEBUGger.inst?.Set_Debug_Left("Set_Pawn_Target, Next_Move nextPos: " + nextPos, Color.yellow);
        moveINProgress = true;

        //nextPos = currentPos++;
        Vector3 targetPosition = Board.inst.Get_Field_Pos(nextPos);

        transform.DOMove(targetPosition, Single_Move_Duration)
                .SetEase(easeType)
                .OnComplete(() => {
                    //executes whenever Pawn reach target position
                    Process_Step_End();
                });
    }

    void Process_Step_End()
    {
        currentPos = nextPos;
        //if (currentPos < targetPos)
        //    nextPos++;

        //Checks for Game Over
        if (currentPos >= fieldsCount - 1)
        {
            DEBUGger.inst?.Set_Debug_Left("Set_Pawn_Target, Next_Move GameOver", Color.red);
            moveINProgress = true;
            gameOver();
        }
        //Check for next move
        else
        {
            //Next step
            if (currentPos < targetPos)
            {
                nextPos++;
                moveINProgress = false;
            }
            //Next Player
            else
            {
                DEBUGger.inst?.Set_Debug_Left("Set_Pawn_Target, Next_Move nextPlayer", Color.red);
                nextPlayer();
                moveINProgress = false;
            }
        }
    }

    /// <summary>Resets the motve for a Game Start</summary>
    /// <remarks></remarks>
    public void ResetPlayer()
    {
        currentPos = 0;
        nextPos = 0;
        targetPos = 0;
        transform.position = Board.inst.Get_Field_Pos(0);
        moveINProgress = false;
        if (GetComponent<Player>().IsAI)
            pos_Z_shift = -0.2f;
        else
            pos_Z_shift = 0.2f;
    }
}
