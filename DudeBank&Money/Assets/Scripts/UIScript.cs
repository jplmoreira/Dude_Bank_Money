using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets._2D;

public class UIScript : MonoBehaviour {

    [SerializeField]
    Weapon weapon;

    [SerializeField]
    Countdown countdown;

    [SerializeField]
    PlayerScript player;

    [SerializeField]
    PlatformerCharacter2D pc2dscript;

    [SerializeField]
    Text bulletCounter;

    [SerializeField]
    Text timeCounter;

    [SerializeField]
    Text resourceCounter;

    [SerializeField]
    Text timeStopActions;

    private int numBullets;
    private float timeVal;
    private float resourceVal;
    private float actions;

	// Use this for initialization
	void Start () {
		if (weapon == null || countdown == null || bulletCounter == null || timeCounter == null) {
            Debug.LogError("Missing fields");
            this.enabled = false;
        }
        numBullets = weapon.Bullets;
        timeVal = countdown.Counter;
        resourceVal = player.Resource;
        actions = pc2dscript.timeStopActions;
        bulletCounter.text = "Bullets: " + numBullets;
        timeCounter.text = timeVal.ToString("0.00") + "s";
        resourceCounter.text = "Resource: " + resourceVal.ToString("0");
        //timeStopActions.text = "Actions: " + actions.ToString();
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
        actions = pc2dscript.timeStopActions;
        actions = System.Math.Max(actions, 0f);
        timeCounter.text = timeVal.ToString("0.00") + "s";
        resourceCounter.text = "Resource: " + resourceVal.ToString("0");
        if (pc2dscript.timeStop){
            timeStopActions.text = "Actions: " + actions.ToString();
        }
        else { timeStopActions.text = ""; }
    }
}
