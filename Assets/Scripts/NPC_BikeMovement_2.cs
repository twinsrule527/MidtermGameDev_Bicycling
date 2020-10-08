using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PURPOSE: A retry of the movement code for the NPC bike's movement, including turning and avoiding obstacles
//USAGE: Attached to a BicycleNPC object which has a box collider.
public class NPC_BikeMovement_2 : MonoBehaviour
{
    public float speed;
    //This is the bike's speed for any given moment
    public float lane_min;
    public float lane_max;
    //These determines where the bike should be horizontally relative to the wall
    public float RVision0_dist;
    public float Vision0_dist;
    BoxCollider2D myCollider;
    Ray2D[] FrontRay = new Ray2D[3];

    void Start() {
        myCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //These front facing rays detect if there's something in front of the player they need to avoid
        FrontRay[2]= new Ray2D(transform.position, transform.up);
        Debug.DrawRay(FrontRay[2].origin, FrontRay[2].direction *  Vision0_dist, Color.magenta);
        FrontRay[1]= new Ray2D(transform.position + transform.right * (( myCollider.size.x * transform.localScale.x / 2)), transform.up);
        Debug.DrawRay(FrontRay[1].origin, FrontRay[1].direction *  Vision0_dist, Color.magenta);
        FrontRay[0]= new Ray2D(transform.position - transform.right * (( myCollider.size.x * transform.localScale.x / 2)), transform.up);
        Debug.DrawRay(FrontRay[0].origin, FrontRay[0].direction *  Vision0_dist, Color.magenta);
        //This Right ray is one directly to the right, coming from the point that they NPC will be in the next frame if they do not change their angle
        Ray2D RVision0 = new Ray2D(transform.position + transform.up * (( myCollider.size.y * transform.localScale.y / 2) + speed *Time.deltaTime) ,transform.right);
        Debug.DrawRay(RVision0.origin, RVision0.direction * RVision0_dist, Color.magenta);
        //The next Right ray is the same as the previous, just at a rotated angle, so that a line could be formed between the two, which the NPC will make its path off of
        Ray2D RVision1 = new Ray2D(transform.position + transform.up * (( myCollider.size.y * transform.localScale.y / 2) + speed *Time.deltaTime), Vector3.Normalize(transform.right * 10f + transform.up) );
        Debug.DrawRay(RVision1.origin, RVision1.direction * RVision0_dist, Color.magenta);
        RaycastHit2D RHit0 = Physics2D.Raycast(RVision0.origin, RVision0.direction, RVision0_dist);
        RaycastHit2D RHit1 = Physics2D.Raycast(RVision1.origin, RVision1.direction, RVision0_dist);
        Debug.DrawLine(RHit0.point, RHit1.point, Color.magenta);
        RaycastHit2D ForwardHit = Physics2D.Raycast(FrontRay[2].origin, FrontRay[2].direction, Vision0_dist);
        //If there is an object in front of the player, they may switch lanes
        if(ForwardHit.collider != null) {
            if(ForwardHit.collider.CompareTag("Vehicle") || ForwardHit.collider.CompareTag("Player")) {
                lane_min = 3.5f;
                lane_max = 4f;
            }
            if(ForwardHit.collider.CompareTag("Wall")) {
                if(ForwardHit.collider.name == RHit0.collider.name) {
                    transform.Rotate(0f, 0f, 1f);
                }
            }
        }
        //If the player is too close or too far from the wall, they will reset
        if(RHit0.collider != null) {
        if(RHit0.collider.CompareTag("Wall")) {
            if(RHit0.fraction * RVision0_dist < lane_min) {
                transform.Rotate(0f, 0f, 3f * (lane_min - RHit0.fraction * RVision0_dist));
            }
            else if(RHit0.fraction * RVision0_dist > lane_max) {
                transform.Rotate(0f, 0f, 3f * (lane_max - RHit0.fraction * RVision0_dist));
            }
        }
        }
        transform.position += transform.up * speed *Time.deltaTime;
    }
}
