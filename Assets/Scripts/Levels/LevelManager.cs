using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public static void GameOver(){
        
        MenuManager.GoToMenu(MenuName.HighScore);
        Destroy(GameObject.FindGameObjectWithTag("Player"));
		Destroy(GameObject.FindGameObjectWithTag("HUD"));
		//MenuManager.GoToMenu(MenuName.Main);
    }


}
