using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class UImanager : MonoBehaviour
{
    /*---------------------------------------------------------------------------------------------
    *  Attached to Canvas_UI
    *  Manages UI
    *--------------------------------------------------------------------------------------------*/

    public static UImanager inst = null;

    [Header("References")]
    [SerializeField] GameObject Panel_StartGame;
    [SerializeField] GameObject Panel_HUD;
    [SerializeField] GameObject Button_RollDice;
    [SerializeField] GameObject Button_Another_Game;
    [SerializeField] TextMeshProUGUI Text_GameMessage;

    //EVENTS
    public delegate void ResetHandler();
    public static event ResetHandler newGame;
    

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
        ResetUI();
    }

    /// <summary>Button Player VS AI pressed</summary>
    /// <remarks></remarks>
    public void Button_Player_vs_AI_Pressed()
    {
        Show_Panel_StartGame(false);
        Show_Panel_HUD(true);
        Show_Button_RollDice(true);
        if (newGame != null)
            newGame();
    }

    /// <summary>Shows Hice Start Game panel</summary>
    /// <remarks></remarks>
    public void Show_Panel_StartGame(bool active)
    {
        Panel_StartGame.SetActive(active);
        Show_Panel_HUD(false);
        ResetUI();
    }

    /// <summary>Shows Game Hud</summary>
    /// <remarks></remarks>
    public void Show_Panel_HUD(bool active)
    {
        Panel_HUD.SetActive(active);
    }

    /// <summary>Shows button for Dice roll</summary>
    /// <remarks></remarks>
    public void Show_Button_RollDice(bool active)
    {
        Button_RollDice.SetActive(active);
    }

    /// <summary>Shows button for Dice roll</summary>
    /// <remarks></remarks>
    public void Show_Button_Another_Game(bool active)
    {
        Button_Another_Game.SetActive(active);
    }

    /// <summary>Sets up Game message</summary>
    /// <remarks></remarks>
    public void Set_Game_Message(string msg)
    {
        Text_GameMessage.text = msg;
    }

    public void Toggle_FullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    /// <summary>Resets UI</summary>
    /// <remarks></remarks>
    public void ResetUI()
    {
        Set_Game_Message("");
        Show_Button_Another_Game(false);
        Show_Button_RollDice(false);
    }
}
