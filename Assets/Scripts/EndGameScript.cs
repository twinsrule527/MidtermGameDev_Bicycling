using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//PURPOSE: Used to end the game when the player activates the triggers.
//USAGE: Attached to a trigger object at the end of the game.
public class EndGameScript : MonoBehaviour
{
    public GameObject myPlayer;
    public WheelMovement myPlayerMovement;
    public WheelRotation myPlayerRotation;
    public Text_DistanceTime myTracker;
    //These several collision floats bellow are used to keep track of the number of collisions the player's had
    public float num_collisions;
    public Text Text_num;
    public float bike_collisions;
    public Text Text_bike;
    public float pothole_collisions;
    public Text Text_pothole;
    public float wall_collisions;
    public Text Text_wall;
    public float num_resets;
    public Text Text_resets;
    void OnTriggerEnter2D(Collider2D activator) {
        if(activator.name == myPlayer.name) {
            Text_num.text = "Number of Collisions: " + num_collisions.ToString();
            Text_bike.text = "Number of Collisions with Bicycles: " + bike_collisions.ToString();
            Text_pothole.text = "Number of Potholes Run Over: " + pothole_collisions.ToString();
            Text_wall.text = "Number of Collisions with Walls: " + wall_collisions.ToString();
            Text_resets.text = "Number of Resets: " + num_resets;
            myPlayerMovement.speed = 0;
            myPlayerMovement.paused = true;
            myPlayerRotation.paused = true;
            myTracker.paused = true;
        }
    }
}
