using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PURPOSE: Allows the player to look behind them by pressing 'Q' (the specific key might change later)
//USAGE: Attached to the player's parent bike
public class Camera_LookBack : MonoBehaviour
{
    public Transform myCamera;
    Vector3 base_pos;
    //The base position the camera should return to
    float pos_change;
    public float max_pos_change;
    //The maximum amount the camera should be able to change by

    void Start() {
        base_pos = myCamera.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Q)) {
            if(pos_change < max_pos_change) {
                pos_change += Time.deltaTime * 2f;
                if(pos_change > max_pos_change) {
                    pos_change = max_pos_change;
                }
            }
        }
        else if(pos_change > 0f) {
            pos_change -=Time.deltaTime * 3f;
        }
        else {
            pos_change =0f;
        }
        myCamera.localPosition = base_pos -myCamera.right * pos_change;
    }
}
