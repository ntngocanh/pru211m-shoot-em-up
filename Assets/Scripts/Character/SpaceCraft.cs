using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceCraft : MonoBehaviour
{
    [SerializeField]
    GameObject prefabExplosion;
    
    [SerializeField]
    string levelName;
    
    [SerializeField]
    GameObject bullet;

    [SerializeField]
    GameObject IonBlaster;
    private Rigidbody2D myRigidBody;
    // saved for efficiency
    float colliderHalfWidth;
    float colliderHalfHeight;

    // health of character
    int healthPoint = 5;

    // Fire support
    double canfire = 0.2;
    // movement support
    const float MoveUnitsPerSecond = 10;
    public static SpaceCraft instance = null;  
    public static int score = 0;
 
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
        DontDestroyOnLoad(gameObject);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //BoxCollider2D collider = GetComponent<BoxCollider2D>();
        //colliderHalfWidth = collider.size.x / 2;
        //colliderHalfHeight = collider.size.y / 2;
        //transform.position = new Vector3();
        print(healthPoint);
		
    }
    public static void HandlePointsAddedEvent(int points)
    {
	    score += points;
        //print("player: " + score);
	}
    // Update is called once per frame
    void Update()
    {
        // Die if healt point below 1
        if (healthPoint <= 0)
        {
            GameOver();
        }

        // convert mouse position to world position
        Vector3 position = Input.mousePosition;
        position.z = -Camera.main.transform.position.z;
        position = Camera.main.ScreenToWorldPoint(position);

        // move to position and clamp in screen
        float step = MoveUnitsPerSecond * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, position, step);
        transform.position = position;
        ClampInScreen();

        // shoot given bullets in given spread if click left mouse button
        if (Input.GetButton("Fire1") && (Time.time > canfire))
        {
            //Shoot(2, 10);
            ShootSingleBullet();
            canfire = Time.time + 0.5;
        }
    }

    // Shooting function
    void Shoot(int numberOfBullet, float spread)
    {
        float spreadRange = spread / 2f;
        for (int i = 0; i < numberOfBullet; i++)
        {
            float startRotation = Mathf.Atan2(Vector2.up.y, Vector2.up.x) * Mathf.Rad2Deg + spreadRange;
            float rotation = startRotation - (spread / ((float) numberOfBullet - 1f)) * i;
            GameObject bulletShooted = Instantiate<GameObject>(bullet, transform.position, Quaternion.Euler(0f, 0f, rotation));
            Bullet script = bulletShooted.GetComponent<Bullet>();
            script.Setup(new Vector2(Mathf.Cos(rotation * Mathf.Deg2Rad), Mathf.Sin(rotation * Mathf.Deg2Rad)));
        }
        
        //AudioSource source = GetComponent<AudioSource>();
        //source.PlayOneShot(audioClip);
    }

    void ShootSingleBullet()
    {
        GameObject bulletShooted = Instantiate<GameObject>(bullet, transform.position, Quaternion.identity);
        Bullet script = bullet.GetComponent<Bullet>();
        script.ApplyForce(new Vector2(1, 0));
    } 

    void ChangeBullet(GameObject newBullet)
    {
        bullet = newBullet;
    }

    /// Clamps the character in the screen
    void ClampInScreen()
    {
        // clamp position as necessary
        Vector3 position = transform.position;
        if (position.x - colliderHalfWidth < ScreenUtils.ScreenLeft)
        {
            position.x = ScreenUtils.ScreenLeft + colliderHalfWidth;
        }
        else if (position.x + colliderHalfWidth > ScreenUtils.ScreenRight)
        {
            position.x = ScreenUtils.ScreenRight - colliderHalfWidth;
        }
        if (position.y + colliderHalfHeight > ScreenUtils.ScreenTop)
        {
            position.y = ScreenUtils.ScreenTop - colliderHalfHeight;
        }
        else if (position.y - colliderHalfHeight < ScreenUtils.ScreenBottom)
        {
            position.y = ScreenUtils.ScreenBottom + colliderHalfHeight;
        }
        transform.position = position;
    }

    // Subtract HP when touch creep
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "FatBirdFall")
        {
            TakeDamage(1);
        }
        if (collision.collider.gameObject.tag == "Egg")
        {
            TakeDamage(1);
        }
        if (collision.collider.gameObject.tag == "IonBlasterBox")
        {
            ChangeBullet(IonBlaster);
        }
    }

    // game over
    void GameOver()
    {
        print("game over");
        Destroy(gameObject);
        SceneManager.LoadScene("StartScene");
    }


    void TakeDamage(int damage)
    {
		healthPoint = Mathf.Max(0, healthPoint - damage);

		// check for game over
		if (healthPoint == 0)
        {
            Destroy(gameObject);
            GameObject die = Instantiate(prefabExplosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(die, 1.2f);
        }
	}
}
