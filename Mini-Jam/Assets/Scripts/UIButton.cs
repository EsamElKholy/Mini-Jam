using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
   // public Canvas canvas;
    public RectTransform canvas;
    public RectTransform uiCanvas;
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
        controller1.dontStart = false;
        controller2.dontStart = false;
        uiCanvas.gameObject.SetActive(true);
        canvas.gameObject.SetActive(false);
    }
}
