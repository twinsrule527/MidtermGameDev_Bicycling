using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PURPOSE: Rotates the player's front wheel, allowing for the player to turn.
//USAGE: Attached to the front wheel sprite, which is "attached" to the player's frame sprite

public class WheelRotation : MonoBehaviour
{
    Transform myTransform;
    float wheel_rotate;
    //The current rotation of the front wheel relative to the whole bike
    public float rotation_speed;
    //The speed at which the whole bike will typically rotate
    public float rotation_inc;
    //The increase in speed when recentering the bike
    public float max_rotate;
    //The maximum rotation of the bike in either direction

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        wheel_rotate = myTransform.localEulerAngles.z;
        //Debug.Log(wheel_rotate.ToString());
        //The first press the player makes is less responsive, allowing for easier more slight movements
        if(Input.GetKeyDown(KeyCode.A)) {
            //If the wheel is not at its maximum rotation, it will rotate
            if(wheel_rotate < 90 + max_rotate) {
                Vector3 rotation_amt = new Vector3(0f, 0f, rotation_speed*Time.deltaTime/2);
                myTransform.localEulerAngles += rotation_amt;      
            }
        }
        else if(Input.GetKey(KeyCode.A) ) {
            //Rotation occurs more quickly when the handlebars are on the other side of the center (at a rate of the current rotation squared (decreasing geometric progression?))
            if(wheel_rotate < 90 ) {
                Vector3 rotation_amt = new Vector3(0f, 0f, rotation_speed*Time.deltaTime + Mathf.Pow(90 - wheel_rotate, 2) * rotation_inc );
                myTransform.localEulerAngles += rotation_amt;
            }
            //If the player is not pointed all the way to the left
            else if(wheel_rotate < 90 + max_rotate) {
                Vector3 rotation_amt = new Vector3(0f, 0f, rotation_speed*Time.deltaTime);
                myTransform.localEulerAngles += rotation_amt;
            }
        }
        if(Input.GetKeyDown(KeyCode.D)) {
            if(wheel_rotate > 90 - max_rotate) {
                Vector3 rotation_amt = new Vector3(0f, 0f, -rotation_speed*Time.deltaTime/2);
                myTransform.localEulerAngles += rotation_amt;      
            }
        }
        else if(Input.GetKey( KeyCode.D ) ) {
            if(wheel_rotate > 90 ) {
                Vector3 rotation_amt = new Vector3(0f, 0f, -( rotation_speed*Time.deltaTime + Mathf.Pow(90 - wheel_rotate, 2) * rotation_inc ) );
                myTransform.localEulerAngles += rotation_amt;
            }
            else if(wheel_rotate > 90 - max_rotate) {
                Vector3 rotation_amt = new Vector3(0f, 0f, -rotation_speed*Time.deltaTime);
                myTransform.localEulerAngles += rotation_amt;
            }
        }
    }
}
