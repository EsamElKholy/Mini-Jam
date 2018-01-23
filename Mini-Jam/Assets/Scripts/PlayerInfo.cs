using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public int Health;
    public int Energy;
    public bool isDead;

    public Image healthBar;
    public Image energyBar;

    private float widthHP;
    private float widthSP;

    // Use this for initialization

    private void Awake()
    {
        Health = 100;
        Energy = 100;
        isDead = false;

        widthSP = energyBar.rectTransform.sizeDelta.x;
        widthHP = healthBar.rectTransform.sizeDelta.x;
    }
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void AddHealth(int val)
    {
        var oldHP = Health;

        Health += val;
        Health = Mathf.Clamp(Health, 0, 100);

        widthHP = Health * widthHP / oldHP;
        
        var HP = healthBar.rectTransform.sizeDelta;
        HP.x = widthHP;
        healthBar.rectTransform.sizeDelta = HP;
    }

    public void AddEnergy(int val)
    {
        var oldSP = Energy;

        Energy += val;
        Energy = Mathf.Clamp(Energy, 0, 100);

        widthSP = Energy * widthSP / oldSP;

        var SP = energyBar.rectTransform.sizeDelta;
        SP.x = widthSP;
        energyBar.rectTransform.sizeDelta = SP;
    }
}
