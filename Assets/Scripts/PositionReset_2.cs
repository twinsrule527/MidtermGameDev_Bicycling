using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PURPOSE: This is a revamped simpler version of Position Reset: "When the player hits a wall or something, if they aren't out of the game, this resets their position"
//USAGE: Attached to the Player's front parent object, which needs to have a front wheel child, at the very least
public class PositionReset_2 : MonoBehaviour
{
    public Transform myWheelTransform;
    Vector3 reset_pos;
    float reset_angle;
    float reset_wheel_angle;
    public Vector3 start_reset_pos;
    public float start_reset_angle;
    public float start_reset_wheel_angle;
    bool recorded;
    public Collider2D myCollider;
    public float reset_time;
    public WheelMovement myPMove;
    public WheelRotation myCRot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //A ray is constantly being shown in front of the player. If the ray hits an object, it records it, unless the player currently has another recorded object
        //Then, when the player collides, they will be sent back to their last free spot
        Ray2D frontRay = new Ray2D(myWheelTransform.position, myWheelTransform.right);
        float myRayDist = 3f;
        Debug.DrawRay(frontRay.origin, frontRay.direction * myRayDist, Color.yellow);
        RaycastHit2D myRayHit = Physics2D.Raycast(frontRay.origin, frontRay.direction, myRayDist);
        if(myRayHit.collider == null ) {
            recorded = false;
        }
        else if(recorded == false) {
            //If no previous spot is recorded, the player will record their current position/angle, so they can return to it after colliding
            recorded = true;
            reset_pos = transform.position;
            reset_angle = transform.eulerAngles.z;
            reset_wheel_angle = myWheelTransform.localEulerAngles.z;
        }
        if(reset_time > 0 ) {
            //As long as the player is resetting
            myPMove.paused = true;
            myCRot.paused = true;
            reset_time -=Time.deltaTime;
            
            transform.position += ( reset_pos - start_reset_pos ) *Time.deltaTime / 3f;
            transform.eulerAngles += new Vector3( 0f, 0f, (reset_angle - start_reset_angle) * Time.deltaTime / 3f);
            myWheelTransform.localEulerAngles += new Vector3( 0f ,0f, (reset_wheel_angle - start_reset_wheel_angle) * Time.deltaTime / 3f);

            //After resetting is done, player can move again
            if(reset_time <= 0 ) {
                myPMove.paused = false;
                myCRot.paused = false;
                reset_time = 0;
            }
        }
    }
}
