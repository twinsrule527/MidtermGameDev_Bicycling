using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PURPOSE: Movement code for a NPC bike, so that it maintains a general speed, and will move to other lanes when passing other bikes, etc.
//USAGE: Attached to a bike NPC object, which moves along the path

public class NPC_Bike_Movement : MonoBehaviour
{
    public float speed;
    public float Vision0_dist;
    public float Vision1_dist;
    public float RVision0_dist;
    public float RVision1_dist;
    public float RVision2_dist;
    public float LVision0_dist;
    public float LVision1_dist;
    public float LVision2_dist;
    Vector3[] call_points;

    // Update is called once per frame
    void Update()
    {   
        //The player needs several vision rays:
        // 0) In front of the NPC, reaches a rather far distance
        Ray2D RayVision0 = new Ray2D(transform.position,transform.up);
        Debug.DrawRay(RayVision0.origin, RayVision0.direction * Vision0_dist, Color.magenta);
        RaycastHit2D RayHit0 = Physics2D.Raycast(RayVision0.origin, RayVision0.direction, Vision0_dist);
        // 1) Behind the NPC, in case anything is close behind them
        Ray2D RayVision1 = new Ray2D(transform.position,-transform.up);
        Debug.DrawRay(RayVision1.origin, RayVision1.direction * Vision1_dist, Color.magenta);
        RaycastHit2D RayHit1 = Physics2D.Raycast(RayVision1.origin, RayVision1.direction, Vision1_dist);
        // R0) To the right of the NPC, reaches about the width of the road
        Ray2D RayRVision0 = new Ray2D(transform.position, transform.right);
        Debug.DrawRay(RayRVision0.origin, RayRVision0.direction * RVision0_dist, Color.magenta);
        RaycastHit2D RayHitR0 = Physics2D.Raycast(RayRVision0.origin, RayRVision0.direction, RVision0_dist);
        // R1) A ray 30 degrees in front of to the right of the player, rather long distance
        Ray2D RayRVision1 = new Ray2D(transform.position, (transform.right * Mathf.Sqrt(3) / 2 ) + transform.up / 2);
        Debug.DrawRay(RayRVision1.origin, RayRVision1.direction * RVision1_dist, Color.magenta);
        RaycastHit2D RayHitR1 = Physics2D.Raycast(RayRVision1.origin, RayRVision1.direction, RVision1_dist);
        // R2) A ray 60 degrees in front of to the right of the player, rather long distance
        Ray2D RayRVision2 = new Ray2D(transform.position, transform.right / 2 + (transform.up * Mathf.Sqrt(3) / 2));
        Debug.DrawRay(RayRVision2.origin, RayRVision2.direction * RVision2_dist, Color.magenta);
        RaycastHit2D RayHitR2 = Physics2D.Raycast(RayRVision2.origin, RayRVision2.direction, RVision2_dist);
        // L0) To the right of the NPC, reaches about the width of the road
        Ray2D RayLVision0 = new Ray2D(transform.position, -transform.right);
        Debug.DrawRay(RayLVision0.origin, RayLVision0.direction * LVision0_dist, Color.magenta);
        RaycastHit2D RayHitL0 = Physics2D.Raycast(RayLVision0.origin, RayLVision0.direction, LVision0_dist);
        // L1) A ray 30 degrees in front of to the left of the player, rather long distance
        Ray2D RayLVision1 = new Ray2D(transform.position, (-transform.right * Mathf.Sqrt(3) / 2 ) + transform.up / 2);
        Debug.DrawRay(RayLVision1.origin, RayLVision1.direction * LVision1_dist, Color.magenta);
        RaycastHit2D RayHitL1 = Physics2D.Raycast(RayLVision1.origin, RayLVision1.direction, LVision1_dist);
        // L2) A ray 60 degrees in front of to the left of the player, rather long distance
        Ray2D RayLVision2 = new Ray2D(transform.position, -transform.right / 2 + (transform.up * Mathf.Sqrt(3) / 2));
        Debug.DrawRay(RayLVision2.origin, RayLVision2.direction * LVision2_dist, Color.magenta);
        RaycastHit2D RayHitL2 = Physics2D.Raycast(RayLVision2.origin, RayLVision2.direction, LVision2_dist);
        //What needs to be done:
        //Movement code
        //Movement code is currently in very preliminary steps
        //Most of the code below this just exists to make sure that the Rays work, and the NPC can turn
        //I still need to make it much much smoother
        //And need to make use of all rays
        //Plus make it interact with the player/obstacles/other NPCs
        if(RayHitR0.collider != null) {
            if(RayHitR0.collider.CompareTag("Wall")) {
              if( RayHitR0.fraction < 0.25f) {
                    transform.Rotate(0f, 0f, 1f);
                }
                else if( RayHitR0.fraction > 0.3f ) {
                    transform.Rotate(0f, 0f, -1f);
                }
            }
        }
        if(RayHit0.collider != null) {
            if(RayHit0.collider.CompareTag("Wall")) {
                if( RayHit0.fraction < 0.05f ) {
                    if( RayHitL0.collider == null ) {
                        transform.Rotate(0f,0f, 1f);
                    }
                }
            }
        }
        // - Ray in front of the vehicle. When it collides with an object, the bike will move over to the other lane, until it passes that object
        transform.position += transform.up *Time.deltaTime * speed;
    }
}
