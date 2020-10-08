using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PURPOSE: Triggers on the bike's collision with other objects. Depending on type of collision, different results
//USAGE: Attached to the front wheel sprite of the player (separate one exists for back wheel)

public class PlayerWheel_Collision : MonoBehaviour
{
    public Transform myPTransform;

    public WheelMovement myWheelMovement;
    public WheelRotation myWheelRotation;

    float out_of_control;

    //Will want to eventually include an array of time delay
    //public int[] myResetTime;
    
    void Update() {
        if(out_of_control > 0 ) {
            out_of_control -= Time.deltaTime;
            myWheelRotation.paused = true;
            if(out_of_control <= 0 ) {
                out_of_control = 0;
                myWheelRotation.paused = false;
            }
        }
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
        else if(activator.CompareTag("Hole")) {
            //Do something semi-random, dependent on 4 main attributes:
            // 1) The angle of the front wheel with the rest of the bike
            // 2) the speed of the bike
            // 3) the size of the pothole
            // 4) a random variable
            float effect_number = Random.Range(-5f, 5f);
            effect_number += myPMove.speed;
            effect_number += activator.transform.localScale.z;
            if( transform.localEulerAngles.z < 90 ) {
                effect_number += transform.localEulerAngles.z / 10f;
            }
            else if( transform.localEulerAngles.z > 90 ) {
                effect_number += (360 - transform.localEulerAngles.z ) / 10f;
            }
            //The different effects are:
            //At best: speed reduction, player loses some stamina
            if( effect_number < 0 ) {
                if(myPMove.speed < myPMove.ave_speed / 3 ) {
                    myPMove.speed = 0;
                    myPMove.stamina -= myPMove.max_stamina / 10;
                }
                else {
                    myPMove.speed -= myPMove.ave_speed / 3;
                    myPMove.stamina -= 1;
                }
            }
            //Second best: slighter speed reduction, player loses some stamina, and wheel turns a bit randomly
            else if(effect_number < 5f) {
                if(myPMove.speed < myPMove.ave_speed / 5) {
                    myPMove.speed = 0;
                    myPMove.stamina -= myPMove.max_stamina / 8;
                }
                else {
                    myPMove.speed -= myPMove.ave_speed / 5;
                    myPMove.stamina -= myPMove.max_stamina / 8;
                }
                transform.localEulerAngles += new Vector3(0f, 0f, Random.Range(-5f, 5f));
                if(transform.localEulerAngles.z < 30f) {
                    transform.localEulerAngles = new Vector3(0f, 0f, 30f);
                }
                else if(transform.localEulerAngles.z > 300f && transform.localEulerAngles.z < 330f) {
                    transform.localEulerAngles = new Vector3(0f, 0f, 330f);
                }
            }
            //Third best: even slighter speed reduction, player front wheel turns, and player unable to control bike for a very short bit
            else if(effect_number < 15f) {
                if(myPMove.speed < myPMove.ave_speed / 8 ) {
                    myPMove.speed = 0;
                    myPMove.stamina -= myPMove.max_stamina / 6;
                }
                else {
                    myPMove.speed -= myPMove.ave_speed / 8;
                    myPMove.stamina -= myPMove.max_stamina / 6;
                }
                transform.localEulerAngles += new Vector3(0f, 0f, Random.Range(-10f, 10f));
                if(transform.localEulerAngles.z < 30f) {
                    transform.localEulerAngles = new Vector3(0f, 0f, 30f);
                }
                else if(transform.localEulerAngles.z > 300f && transform.localEulerAngles.z < 330f) {
                    transform.localEulerAngles = new Vector3(0f, 0f, 330f);
                }
                out_of_control = Random.Range(0.5f, 2f);
            }

            //Second worse case scenario: player loses a lot of control, changes direction, and loses lots of stamina (doesn't slow down, though)
            else if( effect_number < 25f ) {
                transform.localEulerAngles += new Vector3(0f, 0f, Random.Range(-15f, 15f));
                if(transform.localEulerAngles.z < 30f) {
                    transform.localEulerAngles = new Vector3(0f, 0f, 30f);
                }
                else if(transform.localEulerAngles.z > 300f && transform.localEulerAngles.z < 330f) {
                    transform.localEulerAngles = new Vector3(0f, 0f, 330f);
                }
                out_of_control = Random.Range(1f, 5f);
                myPMove.stamina -= myPMove.max_stamina / 4;
            }
            //Worst case scenario: accident, player is out of the game
            else {
                //GAME OVER
            }
        }
    }
}
