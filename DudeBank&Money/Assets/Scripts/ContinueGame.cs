using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueGame : MonoBehaviour {

    public void ResumeGame()
    {
        if (SceneManager.sceneCount > 1)
        {
            SceneManager.UnloadSceneAsync("Menu 3D");
        }     
    }
}
