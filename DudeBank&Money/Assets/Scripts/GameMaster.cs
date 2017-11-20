using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    public static GameMaster gm;

    private void Start() {
        if (gm == null) {
            gm = GameObject.FindWithTag("GM").GetComponent<GameMaster>();
        }
    }

    public Transform player;
    public Transform spawnPoint;

    public IEnumerator RespawnPlayer() {
        yield return new WaitForSeconds(2);
        Instantiate(player, spawnPoint.position, spawnPoint.rotation);
    }

    public static void KillPlayer(PlayerScript player) {
        Destroy(player.gameObject);
        gm.StartCoroutine(gm.RespawnPlayer());
    }

    public static void KillEnemy(EnemyScript enemy) {
        Destroy(enemy.gameObject);
    }
}
