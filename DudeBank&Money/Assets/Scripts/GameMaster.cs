﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{

    private bool gameOver = false;

    public static GameMaster gm;

    public IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Menu 3D");
        SceneManager.UnloadSceneAsync("GameOver");
        SceneManager.UnloadSceneAsync("GameWon");
        SceneManager.UnloadSceneAsync("Prototype");
    }

    public static void EndLevel()
    {
        SceneManager.LoadScene("GameWon", LoadSceneMode.Additive);
        gm.StartCoroutine(gm.RestartGame());
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
        if (player == null || gameOver) {
            SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
            gm.StartCoroutine(gm.RestartGame());
            gameOver = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu 3D", LoadSceneMode.Additive);
        }
    }
}
