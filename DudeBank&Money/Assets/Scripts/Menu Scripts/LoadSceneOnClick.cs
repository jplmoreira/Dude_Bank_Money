using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Prototype");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

}