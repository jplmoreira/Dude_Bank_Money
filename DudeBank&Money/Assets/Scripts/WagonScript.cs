using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonScript : MonoBehaviour {

    private PlayerScript player;
    private Countdown cd;
    private Rigidbody2D rigidBody;

    void Start () {
        GameObject obj = GameObject.Find("Player");
        player = obj.GetComponent<PlayerScript>();
        cd = obj.GetComponent<Countdown>();
        rigidBody = obj.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player" && player.robbed) {
            GameMaster.EndLevel();
            player.robbed = false;
            cd.alarm = false;
            rigidBody.velocity = new Vector2(0, 0);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "Player" && player.robbed) {
            GameMaster.EndLevel();
            player.robbed = false;
            cd.alarm = false;
            rigidBody.velocity = new Vector2(0, 0);
        }
    }
}
