using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BackgroundSoundScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (SceneManager.sceneCount > 1 && GetComponent<AudioSource>().isPlaying)
        {

            GetComponent<AudioSource>().Pause();
        }
        else if (SceneManager.sceneCount == 1 && !GetComponent<AudioSource>().isPlaying)
        {

            GetComponent<AudioSource>().Play();
        }

    }
}
