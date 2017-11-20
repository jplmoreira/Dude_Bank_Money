using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    [System.Serializable]
    public class PlayerStats {
        public int health = 1;
    }

    public PlayerStats playerStats = new PlayerStats();
    public int fallBoundary = -10;

    private void Update() {
        if (transform.position.y <= fallBoundary || transform.position.x >= 39)
            DamagePlayer();
        if (Input.GetKeyDown(KeyCode.R))
            DamagePlayer();
        if (Input.GetKeyDown(KeyCode.E)) {
            transform.GetComponent<Countdown>().timeRate = 1f;
            transform.GetComponent<Countdown>().timeStop = !transform.GetComponent<Countdown>().timeStop;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i=0; i < enemies.Length; i++) {
                EnemyAI enemy = enemies[i].GetComponent<EnemyAI>();
                if (enemy.speed != 0)
                    enemy.speed = 0;
                else
                    enemy.speed = 600;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            transform.GetComponent<Countdown>().timeStop = false;
            float timeRate = transform.GetComponent<Countdown>().timeRate;
            if (timeRate == 1)
                transform.GetComponent<Countdown>().timeRate = 10f;
            else
                transform.GetComponent<Countdown>().timeRate = 1f;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemies.Length; i++) {
                EnemyAI enemy = enemies[i].GetComponent<EnemyAI>();
                if (enemy.speed != 300)
                    enemy.speed = 300;
                else
                    enemy.speed = 600;
            }
        }
    }

    public void DamagePlayer() {
        GameMaster.KillPlayer(this);
    }
}
