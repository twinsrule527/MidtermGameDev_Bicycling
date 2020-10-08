using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PURPOSE: Used as a reference for the player to know their speed through a speedometer
//Placed on a speedometer needle object, pareneted by a speedometer frame

public class SpeedometerNeedle_Script : MonoBehaviour
{
    public WheelMovement mySpeed;
    float speed_ref;
    void Start() {
        speed_ref = 0f;
    }
    // Update is called once per frame
    void Update()
    {   
        if(speed_ref != mySpeed.speed) {
            transform.Rotate(0f, 0f, (speed_ref - mySpeed.speed) * 180 / mySpeed.max_speed);
        }
        float speed = mySpeed.speed;

    }
}
