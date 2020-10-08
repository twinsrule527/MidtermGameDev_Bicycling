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
    
    public bool paused;
    //Used to determine if the player needs to be paused for any reason
    public float max_speed;
    //Used to determine the max speed the player can go at (speed is approx. 1/2 what it would be in mph)
    public float ave_speed;
    //Used to determine the max speed the player can maintain at a steady rate. The player can go faster than this, but if they stop pressing 'W', they will quickly drop off to this
    public float base_speed;
    //Similar to the ave_speed, this is a lower limit on speed. If the player is going slower than this, they will come to a stop very quickly. When accelerating, the player accelerates very quickly until they reach this point, then accelerates more slowly.
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
    public float max_stamina;
    //The amount of stamina the player can have at most - Stamina is used when one is going faster than their average speed
    public float stamina;
    //The actual amount of stamina the player has at any moment
    public float stamina_recovery;
    //The rate which the player's stamina recovers at
    public float stamina_decay;
    //The rate at which the player's stamina decays at

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
    if(!paused) {
        //These two floats determine the direction the player's wheel is pointing
        float y_direction = Mathf.Sin(( Directional.eulerAngles.z * Mathf.PI) / 180 );
        float x_direction = Mathf.Cos(( Directional.eulerAngles.z * Mathf.PI) / 180 );
        if(Input.GetKey(KeyCode.W)) {
            //The player presses W to accelerate
            //If the player's speed is less than their average speed, they can accelerate free of charge
            if(speed < ave_speed) {
                speed += acceleration*Time.deltaTime;
            }
            //If the player is currently not as fast as the base speed, they will accelerate twice as fast
            if(speed < base_speed) {
                speed += acceleration*Time.deltaTime;
            }
            //If the player is already going as fast as their average speed, it requires stamina to go faster
            if(speed >= ave_speed && stamina > 0 ) {
                stamina -= stamina_decay * Time.deltaTime;
                if(stamina < 0 ) {
                    stamina = 0;
                }
                speed += acceleration*Time.deltaTime;
            }
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
        //If the player's speed is slower than their average speed, they recover some stamina, relative to their current speed
        //They must also not be actively speeding up
        if(speed < ave_speed && stamina < max_stamina && !Input.GetKey(KeyCode.W)) {
            stamina += (stamina_recovery) * Time.deltaTime;
            if(speed < base_speed) {
                stamina += (base_speed - speed) * Time.deltaTime;
            }
        }

        //The player will always move forward, as long as speed > 0
        velocity = new Vector3(x_direction,y_direction,0f) * Time.deltaTime * speed;
        myTransform.position += velocity;
        //If the player is turning, their bike will turn relative to how much they turn by
            //The amount which the bike turns every frame is a function equal to:
            //A Normal Vector * the player's current speed * the framerate time * a constant (turn_radius) *the angle of the wheel
        myTransform.localEulerAngles += new Vector3(0f, 0f, 1f)*speed*Time.deltaTime*turn_radius*(Directional.localEulerAngles.z - 90);

        //As might be expected on a bike, if the player is not actively trying to go fast, they slow down
        //They slow down quickly when already going fast, but then reach a steady point, after which they slow down more slowly
        if(speed > ave_speed ) {
            //When going SUPER fast, the player slows down quickly
            speed -= ( speed / 10 * Time.deltaTime );
        }
        else if(speed > base_speed ) {
            //Then between the player's average speed and their slowest possible speed, they slow down little by little
            speed -= ( speed / 100 * Time.deltaTime );
        }
        else if(speed > 0 && !Input.GetKey(KeyCode.W)) {
            //Then, the player slows down quickly again after reaching their peak
            speed -= ( (speed + 3 ) / 5 * Time.deltaTime );
        }
        else if(speed < 0) {
           speed = 0;
        }
    }
    }
}
