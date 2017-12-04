using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultScript : MonoBehaviour {

    private Countdown cd;
    private PlayerScript player;

	// Use this for initialization
	void Start () {
        GameObject obj = GameObject.Find("Player");
        cd = obj.GetComponent<Countdown>();
        player = obj.GetComponent<PlayerScript>();
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            cd.SoundAlarm();
            player.robbed = true;

        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "Player") {
            cd.SoundAlarm();
            player.robbed = true;
        }
    }
}
