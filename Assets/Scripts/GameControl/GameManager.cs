using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager {
    private static GameManager instance;
    public static int score = 0;
    private GameManager() {
        // initialize your game manager here. Do not reference to GameObjects here (i.e. GameObject.Find etc.)
        // because the game manager will be created before the objects
    }    
    public static int Score
    {
		get { return score; }
	}
    public static GameManager Instance {
        get {
            if(instance==null) {
                instance = new GameManager();
            }
 
            return instance;
        }
    }
 
    // Add your game mananger members here
    public void Pause(bool paused) {

    }
    public void AddPoints(int points)
    {
        score += points;
	}
}