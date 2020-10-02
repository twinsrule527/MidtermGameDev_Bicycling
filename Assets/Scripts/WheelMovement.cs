using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//PURPOSE: Used for the wheel's actual movement: declares the player's velocity, as well as moves them
//USAGE: Attached to the player's front wheel
public class WheelMovement : MonoBehaviour
{
    //Neeed to have a force perpendicular to the center of the entire bike considered as a particle
    //Perpendicular is proportional to speed^2 (inversely proportional to radius of curvature)
    //Needed to determine force: angle between wheel and frame; speed of wheel
    
    public float max_speed;
    //Used to determine the max speed the player can go at (speed is approx. 1/2 what it would be in mph)
    Vector3 velocity;
    //Very simple, just added to the player's position every frame to determine where they are going
    public float speed;
    //At any given moment, this is the speed at which the player is moving
    public float acceleration;
    //Is used to determine the rate at which speed changes
    public float deacceleration;
    //Rate at which a player may slow down voluntary; faster than acceleration because of brakes
    public float turn_radius;
    //The constant for the radius around which the player turns when they are turning (may need tweaks)
    public Transform Directional;
    //The player's handlebars, determines direction they are going in
    Transform myTransform;
    //The transform of this object, used for player's movement

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //These two floats determine the direction the player's wheel is pointing
        float y_direction = Mathf.Sin(( Directional.eulerAngles.z * Mathf.PI) / 180 );
        float x_direction = Mathf.Cos(( Directional.eulerAngles.z * Mathf.PI) / 180 );
        if(Input.GetKey(KeyCode.W)) {
            //The player presses W to accelerate
           speed += acceleration*Time.deltaTime;
           if( speed > max_speed ) {
               speed = max_speed;
           }
        }
        //The player can press S to slow down/brake
        if(Input.GetKey( KeyCode.S )) {
            speed +=deacceleration*Time.deltaTime;
            if(speed < 0 ) {
                speed = 0;
            }
        } 
        //The player will always move forward, as long as speed > 0
        velocity = new Vector3(x_direction,y_direction,0f) * Time.deltaTime * speed;
        myTransform.position += velocity;
        //If the player is turning, their bike will turn relative to how much they turn by
            //The amount which the bike turns every frame is a function equal to:
            //A Normal Vector * the player's current speed * the framerate time * a constant (turn_radius) *the angle of the wheel
        myTransform.localEulerAngles += new Vector3(0f, 0f, 1f)*speed*Time.deltaTime*turn_radius*(Directional.localEulerAngles.z - 90);
    }
}
