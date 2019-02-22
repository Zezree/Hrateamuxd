using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour {
    
    public float Timer_text = 10f;
    private Text timerSeconds;


	// Use this for initialization
	void Start () {
        timerSeconds = GetComponent<Text>();

		
	}
	
	// Update is called once per frame
	void Update () {
        Timer_text -= Time.deltaTime;
        timerSeconds.text = "Převrat: " + Timer_text.ToString("f2");
        if (Timer_text <= 0)
        {
            
            SceneManager.LoadScene("scéna");
            Timer_text = Timer_text;
            
        }
	}
}
