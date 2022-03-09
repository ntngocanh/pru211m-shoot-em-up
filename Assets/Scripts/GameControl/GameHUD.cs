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
    void Start()
    {
        // scoreText.transform.SetParent(this.transform);
        // scoreText = gameObject.AddComponent<Text>();
        // scoreText.transform.position = new Vector3(-300, 200, 0);
        // add listener for PointsAddedEvent
        //EventManager.AddListener(EventName.PointsAddedEvent, HandlePointsAddedEvent);

        // initialize score text
        scoreText.text = "Score: " + score;
    }

    void Update()
    {
        scoreText.text = "Score: " + GameManager.score;
        CheckLivesChanged();
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

    public void HandlePauseButtonClicked()
    {
        MenuManager.GoToMenu(MenuName.Pause);
    }

    public void CheckLivesChanged()
    {
        if (GameManager.lives < hearts.Length)
        {
            Destroy(hearts[GameManager.lives].gameObject);
        }
    }

    public void GetCoroutine(){
        StartCoroutine(ShowMessage("Abc", 2));
    }
    IEnumerator ShowMessage(string message, float delay)
    {
        levelText.text = message;
        levelText.enabled = true;
        yield return new WaitForSeconds(delay);
        levelText.enabled = false;
    }
}
