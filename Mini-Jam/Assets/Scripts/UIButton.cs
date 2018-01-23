using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    public Canvas canvas;
    public CharacterController controller1;
    public CharacterController controller2;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void StartGame()
    {
        canvas.enabled = false;
        controller1.dontStart = false;
        controller2.dontStart = false;
    }
}
