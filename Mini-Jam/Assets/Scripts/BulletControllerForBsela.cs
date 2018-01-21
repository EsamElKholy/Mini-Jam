using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControllerForBsela : MonoBehaviour {

	// Use this for initialization
    public float destroyTime;
	void Start () {
        Destroy(this.gameObject, destroyTime);
    }
	
	// Update is called once per frame
	void Update () {
        
    }
   
    private void OnCollisionEnter2D(Collision2D otherObj)
    {

        if (otherObj.gameObject.tag == "Dragon")
        {
            Destroy(this.gameObject);
 
        }
    }

}
