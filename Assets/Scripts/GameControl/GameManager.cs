using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager{
    private static GameManager instance;
    private static int score = 0;
    private static int lives = 5;
    private static int foodCount = 0;
    private static int missileCount = 0;
    public static bool isPaused = false;
    private GameManager() {
        // initialize your game manager here. Do not reference to GameObjects here (i.e. GameObject.Find etc.)
        // because the game manager will be created before the objects
    }    
    public static int Score
    {
		get { return score; }
        set {score = value;}
	}
    public static int Lives
    {
		get { return lives; }
        set {lives = value;}
	}
    public static int FoodCount
    {
		get { return foodCount; }
        set {foodCount = value;}
	}
    public static int MissileCount
    {
		get { return missileCount; }
        set {missileCount = value;}
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
        Score = Score + points;
	}
    public void AddFood(){
        foodCount++;
        if(foodCount >= 25){
            missileCount+=1;
            foodCount-=25;
        }
    }
    public void SubtractMissile(){
        if(missileCount > 0)
            missileCount -=1;
    }
    public void TakeDamage(int damage){
        lives -= damage;
    }
    public void Reset()
    {
        score = 0;
        lives = 5;
        foodCount = 0;
        missileCount = 0;
        isPaused = false;
    }
    public void LoadLevel(string levelName){
        SceneManager.LoadScene(levelName);
    }

}
