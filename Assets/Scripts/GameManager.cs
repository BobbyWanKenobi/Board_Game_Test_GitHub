using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /*---------------------------------------------------------------------------------------------
    *  Attached to Canvas_UI
    *  Manages Game
    *--------------------------------------------------------------------------------------------*/

    public static GameManager inst = null;

    [Header("References")]
    public List<GameObject> Players;

    [Header("Variables")]
    public int ActivePlayer = 0;

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
        RestartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        UImanager.newGame += RestartGame;
        MovePlayer.gameOver += Game_Over;
        MovePlayer.nextPlayer += Next_Player;
        DiceController.diceDone += Move_Player;
    }

    private void OnDisable()
    {
        UImanager.newGame -= RestartGame;
        MovePlayer.gameOver -= Game_Over;
        MovePlayer.nextPlayer -= Next_Player;
        DiceController.diceDone -= Move_Player;
    }


    /// <summary>Resets Game and start a new one</summary>
    /// <remarks></remarks>
    public void RestartGame()
    {
        ActivePlayer = 0;

        foreach (var player in Players)
        {
            player.GetComponent<MovePlayer>().ResetPlayer();
            player.GetComponent<Player>().ResetPlayer();
        }

        DiceController.inst.ResetDice();
    }


    /// <summary>Switch to next Player, Called form nextPlayer event</summary>
    /// <remarks></remarks>
    public void Next_Player()
    {
        DEBUGger.inst?.Set_Debug_Left("GameManager.NextPlayer", Color.green);

        //Advance player
        ActivePlayer++;
        if (ActivePlayer >= Players.Count)
            ActivePlayer = 0;

        //No of Player turns
        Players[ActivePlayer].GetComponent<Player>().TurnsNo++;

        //On every third dice roll player or AI have Bigger Dice
        bool dice_5_10 = false;
        if (Players[ActivePlayer].GetComponent<Player>().TurnsNo == 3)
            dice_5_10 = true;
        DiceController.inst.Set_Dice_Type(dice_5_10);

        //Call Dice button for player or do auto dice roll for AI
        if (Players[ActivePlayer].GetComponent<Player>().IsAI == false)
        {
            UImanager.inst.Set_Game_Message("YOUR MOVE");
            Show_Play_UI();
        }
        else
        {
            UImanager.inst.Set_Game_Message("AI MOVE");
            Roll_Dice(Random.Range(2, 4));
        }
    }

    /// <summary>Rolls the Dice</summary>
    /// <remarks></remarks>
    public void Roll_Dice(float delay)
    {
        DEBUGger.inst?.Set_Debug_Left("GameManager.Roll_Dice, delay: " + delay, Color.green);
        StartCoroutine(DiceController.inst.Set_Dice_Rotation_Delayed(GetComponent<RandomGenerator>().GetRandomInt(1, 6), delay));
    }

    /// <summary>Move Player, Called form diceDone event</summary>
    /// <remarks></remarks>
    void Move_Player(int val)
    {
        DEBUGger.inst?.Set_Debug_Left("GameManager.Move_Pawn, val: " + val, Color.green);
        Players[ActivePlayer].GetComponent<MovePlayer>().Set_Player_Move(val);
    }

    /// <summary>Shows Roll Dice button for player and hides it for AI</summary>
    /// <remarks></remarks>
    void Show_Play_UI()
    {
        if (Players[ActivePlayer].GetComponent<Player>().IsAI == false)
        {
            UImanager.inst.Show_Button_RollDice(true);
        }
        else
        {
            UImanager.inst.Show_Button_RollDice(false);
        }
    }

    /// <summary>Game is finished, Called form gameOver event</summary>
    /// <remarks></remarks>
    public void Game_Over()
    {
        DEBUGger.inst?.Set_Debug_Left("GameManager.Game_Over", Color.green);
        if (Players[ActivePlayer].GetComponent<Player>().IsAI == false)
        {
            UImanager.inst.Set_Game_Message("YOU WON");
        }
        else
        {
            UImanager.inst.Set_Game_Message("AI WON");
        }

        UImanager.inst.Show_Button_Another_Game(true);
    }
}
