using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    [System.Serializable]
    public class EnemyStats {
        public int health = 1;
    }

    public EnemyStats enemyStats = new EnemyStats();

    private bool facingRight = true;

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

    public void Flip() {
        facingRight = !facingRight;
        Transform fov = transform.Find("Eyes");
        fov.Rotate(new Vector3(0, 0, 180));

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
