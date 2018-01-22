using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

    // Use this for initialization
    //GameObject dragon;
    //GameObject bsela;
    //void Start () {
    //    dragon = GameObject.Find("Dragon");
    //    bsela = GameObject.Find("Bsela");
    //}



    float margin = 1.5f; // space between screen border and nearest fighter
 
 private float z0 = 0; // coord z of the fighters plane
 private float zCam; // camera distance to the fighters plane
 private float wScene; // scene width
 private Transform f1; // fighter1 transform
 private Transform f2; // fighter2 transform
 private float xL; // left screen X coordinate
 private float xR; // right screen X coordinate
 
 void calcScreen(Transform p1, Transform p2)
    {
        // Calculates the xL and xR screen coordinates 
        if (p1.position.x < p2.position.x)
        {
            xL = p1.position.x - margin;
            xR = p2.position.x + margin;
        }
        else
        {
            xL = p2.position.x - margin;
            xR = p1.position.x + margin;
        }
    }

    void Start()
    {
        // find references to the fighters
        f1 = GameObject.Find("Dragon").transform;
        f2 = GameObject.Find("Bsela").transform;
        // initializes scene size and camera distance
        calcScreen(f1, f2);
        wScene = xR - xL;
        zCam = (transform.position.z - z0);// / 2.5f;
    }

    void Update()
    {

        calcScreen(f1, f2);
        float width = xR - xL;
        var pos = transform.position;
        if (width > wScene)
        { // if fighters too far adjust camera distance
            
            pos.z = zCam * width / wScene + z0;

            pos.z = Mathf.Clamp(pos.z, -19, -10);           
        }
        // centers the camera

        pos.x = (xR + xL) / 2;
        transform.position = pos;
    }

}
