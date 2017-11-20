using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

    public static GameMaster gm;

    private void Start() {
        if (gm == null) {
            gm = GameObject.FindWithTag("GM").GetComponent<GameMaster>();
        }
    }

    public Transform player;
    public Transform spawnPoint;

    public IEnumerator RestartGame() {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void KillPlayer(PlayerScript player) {
        Destroy(player.gameObject);
        gm.StartCoroutine(gm.RestartGame());
    }

    public static void KillEnemy(EnemyScript enemy) {
        Destroy(enemy.gameObject);
    }
}
