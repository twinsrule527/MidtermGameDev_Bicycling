using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PURPOSE: Used as a reference for the player to know their speed through a speedometer
//Placed on a speedometer needle object, pareneted by a speedometer frame

public class SpeedometerNeedle_Script : MonoBehaviour
{
    public WheelMovement mySpeed;
    // Update is called once per frame
    void Update()
    {   
        transform.localEulerAngles = new Vector3(0f, 0f, 90f - mySpeed.speed * 180 / mySpeed.max_speed);

    }
}
