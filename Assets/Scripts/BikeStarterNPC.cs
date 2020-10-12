using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeStarterNPC : MonoBehaviour
{
    public float base_speed;
    public NPC_BikeMovement_2 myBike;

    public Camera cam;
    // Update is called once per frame
    void Update()
    {
        Vector3 myPos = cam.WorldToViewportPoint(transform.position);
        if(myBike.speed == 0 && (myPos.x >= -.1 && myPos.x <= 1.1) && (myPos.y >=-.1 && myPos.y <= 1.1 ) ) {
            myBike.speed = base_speed;
            myBike.base_speed = base_speed;
        }
    }
}
