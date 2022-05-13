using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    /*---------------------------------------------------------------------------------------------
    *  Attached to CameraTarget
    *  Manages Camera tracking
    *--------------------------------------------------------------------------------------------*/

    public static GameManager inst = null;

    [Header("References")]


    [Header("Variables")]
    GameObject Target;
    [SerializeField] float lerpSpeed = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        Set_Game_Start();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Target.transform.position, lerpSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Target.transform.rotation, lerpSpeed * Time.deltaTime);
    }

    private void OnEnable()
    {
        MovePlayer.nextPlayer += Set_Target_Object;
        UImanager.newGame += Set_Game_Start;
    }

    private void OnDisable()
    {
        MovePlayer.nextPlayer -= Set_Target_Object;
        UImanager.newGame -= Set_Game_Start;
    }

    void Set_Target_Object()
    {
        StartCoroutine(Set_Target(1.0f));
    }

    //
    IEnumerator Set_Target(float delay)
    {
        yield return new WaitForSeconds(delay);
        Target = GameManager.inst.Players[GameManager.inst.ActivePlayer];
    }

    void Set_Game_Start()
    {
        Target = GameManager.inst.Players[0];
    }
}
