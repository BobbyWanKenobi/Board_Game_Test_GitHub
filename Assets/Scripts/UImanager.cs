using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UImanager : MonoBehaviour
{
    /*---------------------------------------------------------------------------------------------
    *  Attached to Board Fields
    *  Manages Debugging display, Set "Editor only" for final
    *--------------------------------------------------------------------------------------------*/

    public static UImanager inst = null;

    [Header("References")]
    [SerializeField] GameObject Panel_StartGame;
    [SerializeField] GameObject Panel_HUD;
    [SerializeField] GameObject Button_RollDice;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Button_Player_vs_AI_Pressed()
    {
        Show_Panel_StartGame(false);
        Show_Button_RollDice(true);
        if (newGame != null)
            newGame();
    }

    public void Show_Panel_StartGame(bool active)
    {
        Panel_StartGame.SetActive(active);
    }

    public void Show_Panel_HUD(bool active)
    {
        Panel_HUD.SetActive(active);
    }

    public void Show_Button_RollDice(bool active)
    {
        Button_RollDice.SetActive(active);
    }

    public void Button_RollDice_Pressed()
    {
        //if (newGame != null)
        //    newGame();
    }
}
