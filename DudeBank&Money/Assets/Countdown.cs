using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour {

    private float timeLeft = 10.00f;

    public float Counter {
        get { return timeLeft;  }
    }

    // Update is called once per frame
    void Update () {
            timeLeft -= Time.deltaTime;

            if (timeLeft < 0) {
                transform.GetComponent<PlayerScript>().DamagePlayer();
            }
	}
}
