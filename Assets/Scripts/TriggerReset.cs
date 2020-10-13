using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//PURPOSE: The center of this is used as the reset point for the player whenever they run into a wall, etc.
//USAGE: attached to an invisible trigger object
public class TriggerReset : MonoBehaviour
{
    public Position_Reset3 myReset;
    void OnTriggerEnter2D(Collider2D activator) {
        myReset.TransformReset = this.transform;
    }
}
