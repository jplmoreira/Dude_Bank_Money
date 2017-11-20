﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour {

    private float timeLeft = 10.00f;

    public float Counter {
        get { return timeLeft;  }
    }

    public bool timeStop = false;
    public float timeRate = 1f;

    // Update is called once per frame
    void Update () {
        if (!timeStop) {
            timeLeft -= Time.deltaTime/timeRate;

            if (timeLeft < 0) {
                transform.GetComponent<PlayerScript>().DamagePlayer();
            }
        }
	}
}
