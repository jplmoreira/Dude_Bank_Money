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
    Text bulletCounter;

    [SerializeField]
    Text timeCounter;

    private int numBullets;
    private float timeVal;

	// Use this for initialization
	void Start () {
		if (weapon == null || countdown == null || bulletCounter == null || timeCounter == null) {
            Debug.LogError("Missing fields");
            this.enabled = false;
        }
        numBullets = weapon.Bullets;
        timeVal = countdown.Counter;
        bulletCounter.text = "Bullets: " + numBullets;
        timeCounter.text = timeVal.ToString("0.00") + "s";
	}

    // Update is called once per frame
    void Update() {
        if (numBullets > weapon.Bullets) {
            numBullets = weapon.Bullets;
            bulletCounter.text = "Bullets: " + numBullets;
        }
        timeVal = countdown.Counter;
        timeVal = System.Math.Max(timeVal, 0f);
        timeCounter.text = timeVal.ToString("0.00") + "s";
    }
}
