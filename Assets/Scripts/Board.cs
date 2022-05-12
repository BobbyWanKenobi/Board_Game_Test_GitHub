using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    /*---------------------------------------------------------------------------------------------
    *  Attached to Board_Fields_Base.
    *  Holds references for all Board game fields
    *--------------------------------------------------------------------------------------------*/

    public static Board inst = null;

    [Header("References")]
    [SerializeField] List<BoardField> boardFields = null;

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
        int x = 0;
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if (child.GetComponent<BoardField>() != null)
            {
                boardFields.Add(child.GetComponent<BoardField>());
                child.GetComponent<BoardField>().Set_Field_Number(x);
                x++;
            }
        }

        //for (int i = 0; i < boardFields.Count; i++)
        //{
        //    boardFields[i].Set_Field_Number(i);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Reset()
    {
        
    }

    /// <summary>Get BoardFild by naumber</summary>
    /// <remarks></remarks>
    public BoardField Get_Field(int fieldNo)
    {
        if (fieldNo < boardFields.Count)
            return boardFields[fieldNo];
        else
        {
            Debug.LogError($"fieldNO {fieldNo} out of range!");
            return null;
        }
    }

    /// <summary>Get BoardFild by naumber</summary>
    /// <remarks></remarks>
    public Vector3 Get_Field_Pos(int fieldNo)
    {
        if (fieldNo < boardFields.Count)
            return boardFields[fieldNo].gameObject.transform.position;
        else
        {
            Debug.LogError($"fieldNO {fieldNo} out of range!");
            return Vector3.zero;
        }
    }

    /// <summary>Get BoardFild by naumber</summary>
    /// <remarks></remarks>
    public int Get_Fields_Count()
    {
        return boardFields.Count;
    }
}
