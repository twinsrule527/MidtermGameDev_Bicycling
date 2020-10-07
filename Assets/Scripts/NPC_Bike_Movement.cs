using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PURPOSE: Movement code for a NPC bike, so that it maintains a general speed, and will move to other lanes when passing other bikes, etc.
//USAGE: Attached to a bike NPC object, which moves along the path

public class NPC_Bike_Movement : MonoBehaviour
{
    public float speed;
    public float Vision0_dist;
    public float RVision0_dist;
    public float LVision0_dist;
    // Update is called once per frame
    void Update()
    {   
        //The player needs several vision rays:
        // 0) In front of the NPC, reaches a rather far distance
        Ray2D RayVision0 = new Ray2D(transform.position,transform.up);
        Debug.DrawRay(RayVision0.origin, RayVision0.direction * Vision0_dist, Color.magenta);
        // R0) To the right of the NPC, reaches about the width of the road
        Ray2D RayRVision0 = new Ray2D(transform.position, transform.right);
        Debug.DrawRay(RayRVision0.origin, RayRVision0.direction * RVision0_dist, Color.magenta);
        // R1) A ray 30 degrees in front of to the right of the player, rather long distance

        // R2) A ray 60 degrees in front of to the right of the player, rather long distance
        
        // L0) To the right of the NPC, reaches about the width of the road
        Ray2D RayLVision0 = new Ray2D(transform.position, -transform.right);
        Debug.DrawRay(RayLVision0.origin, RayLVision0.direction * LVision0_dist, Color.magenta);
        // L1) A ray 30 degrees in front of to the left of the player, rather long distance

        // L2) A ray 60 degrees in front of to the left of the player, rather long distance

        //What needs to be done:
        // - Side ray, which has length depending on the NPC's position in the lane, and determines whether the bike needs to turn
        // - Ray in front of the vehicle. When it collides with an object, the bike will move over to the other lane, until it passes that object
    }
}
