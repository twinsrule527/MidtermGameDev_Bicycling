using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass_Rotation : MonoBehaviour
{

    public Transform headTransform;

    public float up_constant;

    public float right_constant;
    // Update is called once per frame
    void Update()
    {
        transform.position = headTransform.position + headTransform.up * up_constant + headTransform.right * right_constant + new Vector3(0f, 0f, 1f);
    }
}
