using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DEBUGger : MonoBehaviour
{
    /*---------------------------------------------------------------------------------------------
    *  Attached to Canvas_DEBUG.
    *  Manages Debugging display, Set "Editor only" for final
    *  
    *  Method usage
    *  if (DEBUGger.inst != null)
    *      DEBUGger.inst.Set_Debug_Left("Some Comment", Color.red);
    *--------------------------------------------------------------------------------------------*/

    [Header("Instance of Classes")]
    public static DEBUGger inst = null;

    [Header("References")]
    [SerializeField] GameObject DEBUG_Panel = null;
    [SerializeField] TextMeshProUGUI Text_DEBUG_Left = null;
    [SerializeField] TextMeshProUGUI Text_DEBUG_Right = null;
    [SerializeField] TextMeshProUGUI Text_Realtime_Left = null;
    [SerializeField] TextMeshProUGUI Text_Realtime_Right = null;
    [SerializeField] TextMeshProUGUI Text_Realtime_UP = null;

    [Header("How many text lines")]
    [SerializeField] int debug_List_Count = 20;

    [Header("Variables")]
    [SerializeField] bool AlsoPrintDebugLog = true;
    bool debug_Visible = true;
    string Debug_String;

    int debug_No = 0;

    private List<string> left_List = new List<string>();
    private List<string> right_List = new List<string>();

    private void Awake()
    {
        if (inst != null)
            Destroy(this.gameObject);
        else
            inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Button_Toggle_Debug_Display_Pressed();
        DEBUG_Panel.SetActive(false);
        Button_Clear_ALL_Pressed();
    }

    /// <summary>Shows persistant DEBUG messages</summary>
    /// <remarks>debug_List_Count decides text lines count</remarks>
    public void Set_Debug_Left(string msg, Color col)
    {
        Text_DEBUG_Left.text = Process_List(ref left_List, msg, col, debug_List_Count);
        if (AlsoPrintDebugLog)
            Debug.Log(Color_String(msg, new Color(1.0f, 0.7f, 0.0f)));
    }

    public void Set_Debug_Right(string msg, Color col)
    {
        Text_DEBUG_Right.text = Process_List(ref right_List, msg, col, debug_List_Count);
        if (AlsoPrintDebugLog)
            Debug.Log(Color_String(msg, new Color(1.0f, 0.7f, 0.0f)));
    }

    /// <summary>Shows realtime data.</summary>
    /// <remarks>Text should be updated on every frame</remarks>
    public void Set_Realtime_Left(string msg)
    {
        Text_Realtime_Left.text = msg;
    }

    public void Set_Realtime_Right(string msg)
    {
        Text_Realtime_Right.text = msg;
    }

    public void Set_Realtime_UP(string msg)
    {
        Text_Realtime_UP.text = msg;
    }

    /// <summary>Enable/Disable Text display.</summary>
    /// <remarks>Realtime text is always shown</remarks>
    public void Button_Toggle_Debug_Display_Pressed()
    {
        debug_Visible = !debug_Visible;

        DEBUG_Panel.SetActive(debug_Visible);   
    }

    /// <summary>Delete all PlayerPrefs saved data.</summary>
    /// <remarks>USE WITH CAUTION.</remarks>
    public void Button_Delete_All_PlayerPrefs_Pressed()
    {
        //DEBUG
        PlayerPrefs.DeleteAll();
    }

    /// <summary>Clears all displayed text.</summary>
    /// <remarks></remarks>
    public void Button_Clear_ALL_Pressed()
    {
        Text_DEBUG_Left.text = "";
        left_List.Clear();

        Text_DEBUG_Right.text = "";
        right_List.Clear();

        Text_Realtime_Left.text = "";
        Text_Realtime_Right.text = "";
        Text_Realtime_UP.text = "";
    }

    /// <summary>Limits the number of displayed text lines, set text color and number</summary>
    /// <remarks>Use debug_List_Count for text lines count</remarks>
    string Process_List(ref List<string> lst, string msg, Color col, int listCnt)
    {
        msg = Color_String(msg, col);
        msg = System.DateTime.Now.ToString() + "\n" + msg;
        
        lst.Add(debug_No.ToString() + ". " + msg);
        while (lst.Count > listCnt)
            lst.RemoveAt(0);

        string str = "";
        for (int i = 0; i < lst.Count; i++)
        {
            if (i == 0)
                str += lst[i];
            else
                str += "\n" + lst[i];
        }

        debug_No++;

        return str;
    }

    /// <summary>String Color</summary>
    /// <remarks></remarks>
    public string Color_String(string text, Color color)
    {
        return "<color=#" + ColorUtility.ToHtmlStringRGBA(color) + ">" + text + "</color>";
    }
}
