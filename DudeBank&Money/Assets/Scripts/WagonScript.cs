using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonScript : MonoBehaviour {

    private PlayerScript player;

    void Start () {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player" && player.robbed) {
            GameMaster.EndLevel();
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "Player" && player.robbed) {
            GameMaster.EndLevel();
        }
    }
}
