using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WriteScoreMenu : MonoBehaviour {

    public Text textC;
    private string score;
    public string level;

    private void Start()
    {
        score = PlayerPrefs.GetFloat(level).ToString("0.00");
        textC.text = score + "s";
    }
}