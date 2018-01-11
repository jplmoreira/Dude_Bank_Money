using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets._2D;

public class ContinueGame : MonoBehaviour {

    public PlatformerCharacter2D pc2dscript;

    public void ResumeGame()
    {
        if (SceneManager.sceneCount > 1)
        {
            SceneManager.UnloadSceneAsync("Menu 3D");
            pc2dscript = GameObject.FindObjectOfType<PlatformerCharacter2D>();
            pc2dscript.timeStop = false;
            pc2dscript.timeReset = true;
        }     
    }
}
