using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PURPOSE: Movement code for a NPC bike, so that it maintains a general speed, and will move to other lanes when passing other bikes, etc.
//USAGE: Attached to a bike NPC object, which moves along the path

public class NPC_Bike_Movement : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        Ray2D frontVision = new Ray2D(transform.position,transform.up);
        Debug.DrawRay(frontVision.origin, frontVision.direction, Color.yellow);
        //What needs to be done:
        // - Side ray, which has length depending on the NPC's position in the lane, and determines whether the bike needs to turn
        // - Ray in front of the vehicle. When it collides with an object, the bike will move over to the other lane, until it passes that object
    }
}
