using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameHUD : MonoBehaviour
{
    [SerializeField]
    public GameObject[] hearts;
    [SerializeField]
    public Text scoreText;
    [SerializeField]
    public Text levelText;
    [SerializeField]
    public Text foodText;
    [SerializeField]
    public Text missileText;
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
    void Start()
    {
        
    }

    void Update()
    {
        updateUI();
        CheckLivesChanged();
    }
    void updateUI(){
        scoreText.text = "Score: " + GameManager.Score;
        foodText.text = GameManager.FoodCount.ToString();
        missileText.text = GameManager.MissileCount.ToString();
    }
    /// <summary>
	/// Handles the points added event by updating the displayed score
	/// </summary>
	/// <param name="points">points to add</param>

    public void HandlePauseButtonClicked()
    {
        MenuManager.GoToMenu(MenuName.Pause);
    }

    public void CheckLivesChanged()
    {
        if (GameManager.Lives < hearts.Length)
        {
            Destroy(hearts[GameManager.Lives].gameObject);
        }
    }

    public void FireMissileButton(){
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        SpaceCraft script = player.GetComponent<SpaceCraft>();
        script.fireMissile();
    }


}
