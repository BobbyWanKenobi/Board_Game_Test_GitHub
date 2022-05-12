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

    public void Set_Field_Number(int no)
    {
        FieldNo = no;
        Text_No.text = no.ToString();
    }
}
