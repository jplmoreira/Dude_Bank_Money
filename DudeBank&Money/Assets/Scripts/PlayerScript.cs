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
            transform.GetComponent<Countdown>().timeStop = !transform.GetComponent<Countdown>().timeStop;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i=0; i < enemies.Length; i++) {
                EnemyAI enemy = enemies[i].GetComponent<EnemyAI>();
                if (enemy.speed == 0)
                    enemy.speed = 600;
                else
                    enemy.speed = 0;
            }
        }
    }

    public void DamagePlayer() {
        GameMaster.KillPlayer(this);
    }
}
