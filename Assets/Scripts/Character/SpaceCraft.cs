using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceCraft : MonoBehaviour
{
    [SerializeField]
    string levelName;
    [SerializeField]
    public GameObject bullet;
    private Rigidbody2D myRigidBody;
    // saved for efficiency
    float colliderHalfWidth;
    float colliderHalfHeight;

    // health of character
    int healthPoint = 5;

    // Fire support
    double canfire = 0.2;
    int levelGun = 3;
    float spread = 60.0f;
    // movement support
    const float MoveUnitsPerSecond = 10;

    // Start is called before the first frame update
    void Start()
    {
        //BoxCollider2D collider = GetComponent<BoxCollider2D>();
        //colliderHalfWidth = collider.size.x / 2;
        //colliderHalfHeight = collider.size.y / 2;
        transform.position = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("FatBirdFall").Length == 0) LoadLevel();
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

        // shoot if click left mouse button
        if (Input.GetButton("Fire1") && (Time.time > canfire))
        {
            Shoot(); 
            canfire = Time.time + 0.2;
        }
    }

    // Shooting function
    void Shoot()
    {
        float spreadRange = spread / 2f;
        Debug.Log(spreadRange);
        Debug.Log(spread);
        Debug.Log("-------------------------");
        for (int i = 0; i <= levelGun; i++)
        {
            float startRotation = Mathf.Atan2(Vector2.up.y, Vector2.up.x) * Mathf.Rad2Deg + spreadRange;
            float rotation = startRotation - spread * i / ((float) levelGun - 1f);
            Debug.Log(rotation);
            GameObject bulletShooted = Instantiate<GameObject>(bullet, transform.position, Quaternion.Euler(0f, 0f, rotation));
            Bullet script = bullet.GetComponent<Bullet>();
            script.Setup(new Vector2(Mathf.Cos(rotation * Mathf.Deg2Rad), Mathf.Sin(rotation) * Mathf.Deg2Rad));
            //script.ApplyForce(new Vector2(1, 0));
        }
        
        //AudioSource source = GetComponent<AudioSource>();
        //source.PlayOneShot(audioClip);
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
            healthPoint--;
        }
    }

    // game over
    void GameOver()
    {
        print("game over");
        SceneManager.LoadScene("GameOver");
    }

    void LoadLevel(){
        SceneManager.LoadScene(levelName);
    }
}