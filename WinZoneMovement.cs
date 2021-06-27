using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZoneMovement : MonoBehaviour
{
    public int Object_X_axis;
    public float Object_Speed;
    
    // Update is called once per frame
    void Update()
    {
       gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Object_X_axis, 0) * Object_Speed;  
    }
}
