using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WriteScore : MonoBehaviour {

    public Text textC;
    private string score;
    private string level;

	private void Start () {
        PlayerPrefs.GetString("newScore",level);
        if (level != "no")
        {
            score = PlayerPrefs.GetFloat("Prototype").ToString("0.00");
            textC.text = "New Best Score: "+score+"s !";
        }
	}	
}