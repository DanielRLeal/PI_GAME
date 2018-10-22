using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public BarbarianController bc;
    public Text scoreText;
	
   	void Update() {
        scoreText.text = "Score: " + bc.score;
	}
}
