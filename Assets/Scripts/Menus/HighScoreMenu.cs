using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreMenu : MonoBehaviour
{
    [SerializeField]
	Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = GameManager.Score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HandleQuitButtonOnClickEvent()
    {
		// unpause game, destroy menu, and go to main menu
		//Time.timeScale = 1;
		Destroy(gameObject);
        Destroy(GameObject.FindGameObjectWithTag("Player"));
		Destroy(GameObject.FindGameObjectWithTag("HUD"));
		MenuManager.GoToMenu(MenuName.Main);
	}
}
