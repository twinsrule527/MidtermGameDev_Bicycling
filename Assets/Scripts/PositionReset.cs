using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PURPOSE: When the player hits a wall or something, if they aren't out of the game, this resets their position
//USAGE: Attached to the player's parent object
public class PositionReset : MonoBehaviour
{
    float reset_time;
    Vector3 re_start_pos;
    float re_start_angle;
    float re_wheel_angle;
    float re_finish_angle;
    Vector3 re_finish_pos;
    public Collider2D myCollider;
    public WheelMovement myPMove;
    public WheelRotation myCRot;
    public Transform myCTransform;
    public bool call_reset;

    // Update is called once per frame
    void Update()
    {
        //When a reset is first called, various things have to be set up
        if(call_reset) {
            //Depending on the type of collision, different things will happen
            if(myCollider.CompareTag("Wall") ) {
                //When colliding with this type of wall, there is a set amount of time for the player to reset
                reset_time = 3f;
                //During that reset, the player has a starting position + rotation...
                re_start_pos = transform.position;
                re_start_angle = transform.eulerAngles.z;
                re_wheel_angle = myCTransform.eulerAngles.z;
                //..., and a final rotation +...
                re_finish_angle = myCollider.transform.localEulerAngles.z;
                //... a final position, which is dependent on the position of the player/the wall
                //How I calculate it:
                    //If the wall is on the player's right: the player goes down and left 1 unit
                    //If the wall is on the player's left: the player goes down and right 1 unit
                    //In this instance: down depends on the player's direction (or not)
                if(myCollider.transform.eulerAngles.z == 0) {
                    if(transform.position.x > myCollider.transform.position.x) {
                        re_finish_pos = transform.position + new Vector3(1f, -1f, 0f);
                    }
                    else {
                        re_finish_pos = transform.position + new Vector3(-1f, -1f, 0f);
                    }
                }
                else if(myCollider.transform.eulerAngles.z == 180) {
                    if(transform.position.x > myCollider.transform.position.x) {
                        re_finish_pos = transform.position + new Vector3(1f, 1f, 0f);
                    }
                    else {
                        re_finish_pos = transform.position + new Vector3(-1f, 1f, 0f);
                    }
                }
                else if(myCollider.transform.eulerAngles.z == 90f ) {
                    if(transform.position.y> myCollider.transform.position.y) {
                        re_finish_pos = transform.position + new Vector3(1f, 1f, 0f);
                    }
                    else {
                        re_finish_pos = transform.position + new Vector3(1f, -1f, 0f);
                    }
                }
                else if(myCollider.transform.eulerAngles.z == 270f) {
                    if(transform.position.y> myCollider.transform.position.y) {
                        re_finish_pos = transform.position + new Vector3(-1f, 1f, 0f);
                    }
                    else {
                        re_finish_pos = transform.position + new Vector3(-1f, -1f, 0f);
                    }
                }

            }
            call_reset = false;
        }
        if( reset_time > 0 ) {
            //As long as the player is resetting
            myPMove.paused = true;
            myCRot.paused = true;
            reset_time -=Time.deltaTime;

            //still has several issues, mainly: does 270 degree rotation, front wheel doesn't straighten out
            Vector3 pos_change = (re_finish_pos - re_start_pos) * Time.deltaTime / 3f;
            Vector3 angle_change = new Vector3(0f, 0f, (re_finish_angle - re_start_angle)*Time.deltaTime / 3f );
            transform.position += pos_change;
            transform.eulerAngles +=angle_change;
            Vector3 wheel_angle_change = new Vector3(0f, 0f, (90f - re_wheel_angle)*Time.deltaTime / 3f );
            myCTransform.localEulerAngles += wheel_angle_change;
            //When the resetting is done, the player is able to start again
            if(reset_time <= 0 ) {
                myPMove.paused = false;
                myCRot.paused = false;
                reset_time = 0;
            }
        }
    }
}
