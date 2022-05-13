using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    /*---------------------------------------------------------------------------------------------
    *  Attached to AudioController
    *  Manage Music & Sound Effects
    *--------------------------------------------------------------------------------------------*/

  

    [Header("This Instance")]
    public static AudioController inst = null;

    [Header("References")]
    [SerializeField] AudioSource SoundSource = null;
    [SerializeField] AudioClip Step = null;  //0 = Menu

    private void Awake()
    {
        if (inst == null)
            inst = this;
        else
            Destroy(this.gameObject);
    }

    private void OnEnable()
    {
        MovePlayer.stepDone += Step_Sound;
    }

    private void OnDisable()
    {
        MovePlayer.stepDone -= Step_Sound;
    }

    void Step_Sound()
    {
        SoundSource.PlayOneShot(Step, Random.Range(0.01f, 0.02f));
    }
}
