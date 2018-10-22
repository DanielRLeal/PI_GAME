using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour {

    public Image BackgroundImg;
    private bool IsShowned = false;
    private float transition = 0.0f;
	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (!IsShowned)
            return;

        transition += Time.deltaTime;
        BackgroundImg.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, transition);

    }

    public void ToggleEndMenu(int score)
    {
        gameObject.SetActive(true);
            IsShowned = true;
    }

    public void ToMenu()
    {
        SceneManager.LoadScene( "Menu");

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
