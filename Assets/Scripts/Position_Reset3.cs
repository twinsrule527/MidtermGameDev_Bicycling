using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//PURPOSE: This is my third attempt to make a working/nice reset code
//USAGE: Attached to the player's Parent Bike
public class Position_Reset3 : MonoBehaviour
{
    //All of these variables are mostly coming from PositionReset2, in case I decide to go back to that reset
    public Transform TransformReset;
    public Collider2D myCollider;
    public float reset_time;
    //Used as a step by step counter of resetting
    public float reset_timer;
    //Used to see the total time needed to reset
    public WheelMovement myPMove;
    public WheelRotation myCRot;
    public Text Text_Reset;
    //This is the text that appears when you need to reset
    float text_delay;
    //This float is used so that the text will act with a slight delay

    // Update is called once per frame
    void Update()
    {
        if(reset_time <=0 && reset_timer > 0 ) {
            Text_Reset.text = "Resetting...";
            reset_time = reset_timer;
            myCRot.paused = true;
            myPMove.paused = true;
        }
        //While the reset timer is greater than 0, the player resets back to their original spot
        if(reset_time > 0) {
            reset_time -= Time.deltaTime;
            //When resetting is finished, the player's reset timer is also set to 0
            if(reset_time <= 0 ) {
                reset_time = 0;
                reset_timer = 0;
                myCRot.paused = false;
                myPMove.paused = false;
                Text_Reset.text = "Go!";
            }
            //Once the resetting is halfway done, the player move to their reset position
            if(reset_time < reset_timer * 3/4 ) {
                transform.position = TransformReset.position;
                transform.rotation = TransformReset.rotation;
                myCRot.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
                float x = 0f;
                if(reset_time < reset_timer * 1/4) {
                    x = 1f;
                }
                else if(reset_time < reset_timer * 1/2 ) {
                    x = 2f;
                }
                else {
                    x = 3f;
                    if(myCollider.gameObject != null && myCollider.CompareTag("Vehicle")) {
                        Destroy(myCollider.gameObject);
                    }
                }
                Text_Reset.text = "Reset in " + x.ToString();
                //I need to solve an issue with respawning on top of another bike, preventing you from turning for the first few seconds
            }
        }
        if(myPMove.speed > 0 && text_delay <= 0) {
            text_delay = 1f;
        }
        if(text_delay > 0) {
            text_delay -= Time.deltaTime;
            if(text_delay <= 0 ) {
                text_delay = 0f;
                Text_Reset.text = "";
            }
        }
    }
}
