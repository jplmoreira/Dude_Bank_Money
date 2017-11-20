using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    [System.Serializable]
    public class EnemyStats {
        public int health = 1;
    }

    public EnemyStats enemyStats = new EnemyStats();

    public void DamageEnemy(int amount) {
        enemyStats.health -= amount;
        if (enemyStats.health <= 0) 
            GameMaster.KillEnemy(this);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        PlayerScript player = collision.collider.GetComponent<PlayerScript>();
        if (player != null) {
            player.DamagePlayer();
        }
    }
}
