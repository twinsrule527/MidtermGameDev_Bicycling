using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//PURPOSE:This is a UI script that provides the player with various elements of their current statistics
//USAGE: Attached to a text object that is the child of the Canvas for the UI
public class Text_DistanceTime : MonoBehaviour
{
    public Text Text_Distance;
    public Text Text_Time;
    public WheelMovement myMovement;
    public float max_speed_ref;
    bool started;
    float distance_travelled;
    float time_passed_sec;
    float time_passed_min;
    public bool paused = false;

    // Update is called once per frame
    void Update()
    {
        if( myMovement.speed > 0 ) {
            started = true;
        }
        if(!paused && started) {
            float distance_frame = myMovement.speed / myMovement.max_speed * Time.deltaTime *max_speed_ref;
            distance_travelled += distance_frame;
            Text_Distance.text = (Mathf.Round(distance_travelled * 100f) / 100f).ToString() + " mi";
            time_passed_sec += Time.deltaTime;
            if(time_passed_sec >= 60f) {
                time_passed_min++;
                time_passed_sec -= 60f;
            }
            if(time_passed_min < 10f) {
                if(time_passed_sec < 10f) {
                    Text_Time.text = "00:0" + time_passed_min.ToString() + ":0" + (Mathf.Round(time_passed_sec * 10f) / 10f ).ToString();
                }
                else {
                    Text_Time.text = "00:0" + time_passed_min.ToString() + ":" + (Mathf.Round(time_passed_sec * 10f) / 10f ).ToString();
                }
            }
            else {
                if(time_passed_sec < 10f) {
                    Text_Time.text = "00:" + time_passed_min.ToString() + ":0" + (Mathf.Round(time_passed_sec * 10f) / 10f ).ToString();
                }
                else {
                    Text_Time.text = "00:" + time_passed_min.ToString() + ":" + (Mathf.Round(time_passed_sec * 10f) / 10f ).ToString();
                }
            }
        }
    }
}

