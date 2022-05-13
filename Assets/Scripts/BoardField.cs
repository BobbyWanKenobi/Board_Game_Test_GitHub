using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoardField : MonoBehaviour
{
    /*---------------------------------------------------------------------------------------------
    *  Attached to Board Fields
    *  Manages Board fields number
    *--------------------------------------------------------------------------------------------*/

    [Header("References")]
    [SerializeField] TextMeshPro Text_No;

    [Header("Variables")]
    public int FieldNo = 0;

    /// <summary>Display fields number</summary>
    /// <remarks></remarks>
    public void Set_Field(int no)
    {
        FieldNo = no;
        Text_No.text = no.ToString();
    }

    /// <summary>Set_Test on field</summary>
    /// <remarks></remarks>
    public void Set_Field(string msg)
    {
        Text_No.text = msg;
    }
}
