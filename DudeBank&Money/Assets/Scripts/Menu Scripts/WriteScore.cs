using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WriteScore : MonoBehaviour {

    public Text textC;
    private string score;
    private string level;

    private void Start () {
        string level = PlayerPrefs.GetString("newScore");
        if (level == "no")
        {
            textC.text = "No new Best Score";
        }
        else
        { 
            score = PlayerPrefs.GetFloat(level).ToString("0.00");
            textC.text = "New Best Score: "+score+"s !";
        }
        //Debug.Log(level + " - " + score + "  -  " + PlayerPrefs.GetString("newScore"));
    }	
}