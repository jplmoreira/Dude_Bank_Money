using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    [SerializeField]
    Weapon weapon;

    [SerializeField]
    Countdown countdown;

    [SerializeField]
    PlayerScript player;

    [SerializeField]
    Text bulletCounter;

    [SerializeField]
    Text timeCounter;

    [SerializeField]
    Text resourceCounter;

    private int numBullets;
    private float timeVal;
    private float resourceVal;

	// Use this for initialization
	void Start () {
		if (weapon == null || countdown == null || bulletCounter == null || timeCounter == null) {
            Debug.LogError("Missing fields");
            this.enabled = false;
        }
        numBullets = weapon.Bullets;
        timeVal = countdown.Counter;
        resourceVal = player.Resource;
        bulletCounter.text = "Bullets: " + numBullets;
        timeCounter.text = timeVal.ToString("0.00") + "s";
        resourceCounter.text = "Resource: " + resourceVal.ToString("0");
	}

    // Update is called once per frame
    void Update() {
        if (numBullets > weapon.Bullets) {
            numBullets = weapon.Bullets;
            bulletCounter.text = "Bullets: " + numBullets;
        }
        timeVal = countdown.Counter;
        timeVal = System.Math.Max(timeVal, 0f);
        resourceVal = player.Resource;
        resourceVal = System.Math.Max(resourceVal, 0f);
        timeCounter.text = timeVal.ToString("0.00") + "s";
        resourceCounter.text = "Resource: " + resourceVal.ToString("0");
    }
}
