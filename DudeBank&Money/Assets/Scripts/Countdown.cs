using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour {

    private float timeLeft = 10.00f;

    public float Counter {
        get { return timeLeft;  }
    }

    public bool alarm = true;

    private PlayerScript player;

    private void Awake() {
        GameObject obj = GameObject.Find("Player");
        player = obj.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update () {
        if (alarm) {
            timeLeft -= Time.deltaTime * player.slowFactor;

            if (timeLeft < 0) {
                transform.GetComponent<CharacterScript>().DamageCharacter(9999);
            }
        }
	}

    public void SoundAlarm() {
        alarm = true;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
            enemies[i].GetComponent<EnemyScript>().Alarmed();
    }
}
