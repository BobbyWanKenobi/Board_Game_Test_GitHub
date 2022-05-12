using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Light : MonoBehaviour
{
    //**MANAGE Lights Flicker*//
    //**ATTACHED TO Any Light*//
    /*NOTES:
     * 
     */

    [SerializeField] float Light_Intensity = 1;
    [SerializeField] float Light_Flicker_percent = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        Light_Intensity = GetComponent<Light>().intensity;
        Light_Flicker_percent = Light_Intensity * Light_Flicker_percent;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Light>().intensity = GetComponent<Light>().intensity + Random.Range(-Light_Flicker_percent, Light_Flicker_percent);
    }
}
