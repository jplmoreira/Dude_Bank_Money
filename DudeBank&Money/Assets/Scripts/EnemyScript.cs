using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Vector3 pos1;
    public Vector3 currentEndPos;
    public Vector3 pos2;

    [System.Serializable]
    public class EnemyStats
    {
        public int health = 1;
    }

    public EnemyStats enemyStats = new EnemyStats();

    private void Start()
    {
        pos1 = transform.position;
        currentEndPos = pos2;
    }

    private void Update()
    {
        if (currentEndPos == pos2 && Vector3.Distance(transform.position, pos2) < 0.1f)
        {
            currentEndPos = pos1;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (currentEndPos == pos1 && Vector3.Distance(transform.position, pos1) < 0.1f)
        {
            currentEndPos = pos2;
            transform.localScale = new Vector3(1, 1, 1);
        }
        transform.position = Vector3.Lerp(transform.position, currentEndPos, Time.deltaTime * 1.0f);
    }

    public void DamageEnemy(int amount)
    {
        enemyStats.health -= amount;
        if (enemyStats.health <= 0)
        {
            GameMaster.KillEnemy(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerScript player = collision.collider.GetComponent<PlayerScript>();
        if (player != null) {
            player.DamagePlayer();
        }
    }
}
