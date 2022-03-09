using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameHUD : MonoBehaviour
{
    [SerializeField]
    public Text scoreText;
    
    [SerializeField]
    Text levelText;
    public int score = 0;
    public static GameHUD instance = null;  
 
         //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
             if (instance == null)
             {
                 //if not, set instance to this
                 instance = this;
             }
             //If instance already exists and it's not this:
             else if (instance != this)
             {
                Destroy(gameObject);   
             }
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(this.gameObject);
        
    }
    void Start(){
        // scoreText.transform.SetParent(this.transform);
        // scoreText = gameObject.AddComponent<Text>();
        // scoreText.transform.position = new Vector3(-300, 200, 0);
        // add listener for PointsAddedEvent
		//EventManager.AddListener(EventName.PointsAddedEvent, HandlePointsAddedEvent);

		// initialize score text
		scoreText.text = "Score: " + score;
    }

    void Update(){
		scoreText.text = "Score: " + GameManager.score;
    }
    /// <summary>
	/// Gets the score
	/// </summary>
	/// <value>the score</value>
	public int Score
    {
		get { return score; }
	}
    /// <summary>
	/// Handles the points added event by updating the displayed score
	/// </summary>
	/// <param name="points">points to add</param>
	private void HandlePointsAddedEvent(int points)
    {
		score += points;
		scoreText.text = "Score: " + score;
        //print("Score: " + score);
	}

    public void HandlePauseButtonClicked(){
        MenuManager.GoToMenu(MenuName.Pause);
    }
}
