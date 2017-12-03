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

    public IEnumerator RestartGame() {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void EndLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void KillCharacter(CharacterScript character) {
        Destroy(character.gameObject);
    }

    private void Update() {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
            gm.StartCoroutine(gm.RestartGame());
    }
}
