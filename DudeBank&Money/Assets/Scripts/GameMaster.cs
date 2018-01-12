using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets._2D;

public class GameMaster : MonoBehaviour
{

    private bool gameOver = false;

    public static GameMaster gm;

    public PlatformerCharacter2D pc2dscript;

    public IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Menu 3D");
        if (SceneManager.GetSceneByName("GameOver").isLoaded)
        {
            SceneManager.UnloadSceneAsync("GameOver");
        }
        if (SceneManager.GetSceneByName("GameWon").isLoaded)
        {
            SceneManager.UnloadSceneAsync("GameWon");
        }
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public static void EndLevel()
    {
        SceneManager.LoadScene("GameWon", LoadSceneMode.Additive);
        gm.StartCoroutine(gm.RestartGame());
    }

    public static void ReloadLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void KillCharacter(CharacterScript character)
    {
        Destroy(character.gameObject);
    }

    private void Start()
    {
        if (gm == null)
        {
            gm = GameObject.FindWithTag("GM").GetComponent<GameMaster>();
        }
    }

    private void Update() {
        GameObject player = GameObject.Find("Player");
        if (player == null && !gameOver) {
            if (!SceneManager.GetSceneByName("GameWon").isLoaded)
            {
                SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
            }
            gm.StartCoroutine(gm.RestartGame());
            gameOver = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.sceneCount == 1)
        {
            SceneManager.LoadScene("Menu 3D", LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Menu 3D"));
            pc2dscript.timeStop = true;
        }
    }
}
