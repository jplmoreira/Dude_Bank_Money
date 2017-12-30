using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WagonScript : MonoBehaviour {

    private PlayerScript player;
    private Countdown cd;
    private Rigidbody2D rigidBody;
    private string level;
    private float score;
    private bool updated;

    void Start () {
        updated = false;
        GameObject obj = GameObject.Find("Player");
        player = obj.GetComponent<PlayerScript>();
        cd = obj.GetComponent<Countdown>();
        rigidBody = obj.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player" && player.robbed) {
            if(!updated) UpdateScore();
            GameMaster.EndLevel();
            player.robbed = false;
            cd.alarm = false;
            rigidBody.velocity = new Vector2(0, 0);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "Player" && player.robbed) {
            if (!updated) UpdateScore();
            GameMaster.EndLevel();
            player.robbed = false;
            cd.alarm = false;
            rigidBody.velocity = new Vector2(0, 0);
        }
    }

    private void UpdateScore()
    {
        PlayerPrefs.SetString("newScore", "no");
        level = SceneManager.GetActiveScene().name;
        PlayerPrefs.GetFloat(level,score);
        if (cd.Counter > score || score == 0)
        {
            PlayerPrefs.SetFloat(level,cd.Counter);
            PlayerPrefs.SetString("newScore", level);
        }
        PlayerPrefs.Save();
    }
}
