using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PURPOSE: Triggers on the bike's collision with other objects. Depending on type of collision, different results
//USAGE: Attached to the front wheel sprite of the player (separate one exists for back wheel)

public class PlayerWheel_Collision : MonoBehaviour
{
    public Transform myPTransform;

    //Will want to eventually include an array of time delay
    //public int[] myResetTime;
    
    void Start() {
        //myResetTime = new int[10];
        //myResetTime[0] = 100;//Time to reset when hitting a wall
    }

    void OnTriggerEnter2D(Collider2D activator) {
        PositionReset_2 myPReset = GetComponentInParent<PositionReset_2>();
        WheelMovement myPMove = GetComponentInParent<WheelMovement>();
        if(activator.CompareTag("Wall")) {
            myPMove.speed = 0;
            myPReset.myCollider = activator;
            myPReset.reset_time = 3f;
            myPReset.start_reset_angle = myPTransform.localEulerAngles.z;
            myPReset.start_reset_pos = myPTransform.position;
            myPReset.start_reset_wheel_angle = transform.localEulerAngles.z;
            //Need to include a slight animation of moving backwards and getting into position
            //myPTransform.localEulerAngles = activator.transform.localEulerAngles;
            //myPTransform.localPosition -= new Vector3(1f,0f,0f);
            //myPTransform.position -= myPTransform.up / 1; - Needs to go to the left or right
            //transform.localEulerAngles = new Vector3(0f, 0f, 90f);
        }
    }
}
