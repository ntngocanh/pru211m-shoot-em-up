using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceCraft : MonoBehaviour
{
    [SerializeField]
    GameObject prefabExplosion;

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    GameObject IonBlaster;

    [SerializeField]
    GameObject NeutronGun;

    [SerializeField]
    GameObject LaserCanon;

    [SerializeField]
    GameObject Missile;

    // saved for efficiency
    float colliderHalfWidth;
    float colliderHalfHeight;

    // health of character
    int healthPoint = 5;

    // Fire support
    double canfire = 0.2;
    int levelGun = 2;
    GameObject bulletShooted1;
    GameObject bulletShooted2;
    GameObject bulletShooted3;
    string gunType;

    // movement support
    const float MoveUnitsPerSecond = 10;
    public static SpaceCraft instance = null;
    public static int score = 0;
    //number of missile
    int missile = 5;

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
        colliderHalfWidth = gameObject.GetComponent<CircleCollider2D>().radius;
        colliderHalfHeight = gameObject.GetComponent<CircleCollider2D>().radius;
    }


    // Start is called before the first frame update
    void Start()
    {
        ChangeBullet(IonBlaster);
        levelGun = 1;
        print(healthPoint);

    }
    // Update is called once per frame
    void Update()
    {
        // check for pausing game
        if (Input.GetKeyDown("escape"))
        {
            if (!GameManager.isPaused)
            {
                MenuManager.GoToMenu(MenuName.Pause);
                GameManager.isPaused = true;
            }
        }
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
        transform.position = position;
        ClampInScreen();

        // shoot given bullets in given spread if click left mouse button
        if (Input.GetButton("Fire1") && (Time.time > canfire))
        {
            ShootSingleBullet();
            canfire = Time.time + 0.5;
        }
        if (Input.GetMouseButtonDown(1))
        {
            fireMissile();
        }
        if (score % 20 == 0)
        {
            missile++;
        }
    }

    public void fireMissile()
    {
        if (GameManager.MissileCount > 0)
        {
            GameObject missileClone = Instantiate(Missile, transform.position, Quaternion.identity) as GameObject;
            GameManager.MissileCount = GameManager.MissileCount - 1;
        }
    }

    // Shooting function
    void Shoot(int numberOfBullet, float spread)
    {
        Debug.Log("Start rotation at: " + Mathf.Atan2(Vector2.up.y, Vector2.up.x) * Mathf.Rad2Deg);
        float spreadRange = spread / 2f;
        for (int i = 0; i < numberOfBullet; i++)
        {
            float startRotation = Mathf.Atan2(Vector2.up.y, Vector2.up.x) * Mathf.Rad2Deg + spreadRange;
            float rotation = startRotation - (spread / ((float) numberOfBullet - 1f)) * i;
            Debug.Log("This is the rotation of" + i + ":" + rotation);
            GameObject bulletShooted = Instantiate<GameObject>(bullet, transform.position, Quaternion.Euler(0f, 0f, rotation));
            Bullet script = bulletShooted.GetComponent<Bullet>();
            Debug.Log("This is direction of " + i + ":" + new Vector2(Mathf.Cos(rotation * Mathf.Deg2Rad), Mathf.Sin(rotation * Mathf.Deg2Rad)));
            script.Setup(new Vector2(Mathf.Cos(rotation * Mathf.Deg2Rad), Mathf.Sin(rotation * Mathf.Deg2Rad)));
            //script.ApplyForce(new Vector2(1, 0));
        }
    }

    void ShootSingleBullet()
    {
        switch (gunType)
        {
            case "LaserCannonCI2Weak":
                AudioManager.Play(AudioClipName.LaserCannon);
                break;
            case "IonBlasterSingle":
                AudioManager.Play(AudioClipName.IonBlaster);
                break;
            case "NeutronGunCI2Medium":
                AudioManager.Play(AudioClipName.NeutronGun);
                break;
            default:
                break;
        }
        if (levelGun >= 3) levelGun = 3;
        if (bullet != LaserCanon)
        {
            switch (levelGun)
            {
                case 1:
                    bulletShooted1 = Instantiate<GameObject>(bullet, transform.position, Quaternion.identity);
                    break;
                case 2:
                    bulletShooted1 = Instantiate<GameObject>(bullet, transform.position + Vector3.left * 0.3f, Quaternion.identity);
                    bulletShooted2 = Instantiate<GameObject>(bullet, transform.position + Vector3.right * 0.3f, Quaternion.identity);
                    break;
                case 3:
                    bulletShooted1 = Instantiate<GameObject>(bullet, transform.position + Vector3.left * 0.4f, Quaternion.identity);
                    bulletShooted2 = Instantiate<GameObject>(bullet, transform.position + Vector3.right * 0.4f, Quaternion.identity);
                    bulletShooted3 = Instantiate<GameObject>(bullet, transform.position, Quaternion.identity);
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (levelGun)
            {
                case 1:
                    bulletShooted1 = Instantiate<GameObject>(bullet, transform.position + Vector3.up * 5f, Quaternion.identity);
                    break;
                case 2:
                    bulletShooted1 = Instantiate<GameObject>(bullet, transform.position + Vector3.left * 0.3f + Vector3.up * 4.8f, Quaternion.identity);
                    bulletShooted2 = Instantiate<GameObject>(bullet, transform.position + Vector3.right * 0.3f + Vector3.up * 4.8f, Quaternion.identity);
                    break;
                case 3:
                    bulletShooted1 = Instantiate<GameObject>(bullet, transform.position + Vector3.left * 0.4f + Vector3.up * 4.8f, Quaternion.identity);
                    bulletShooted2 = Instantiate<GameObject>(bullet, transform.position + Vector3.right * 0.4f + Vector3.up * 4.8f, Quaternion.identity);
                    bulletShooted3 = Instantiate<GameObject>(bullet, transform.position + Vector3.up * 4.8f, Quaternion.identity);
                    break;
                default:
                    break;
            }
            Destroy(bulletShooted1, 0.1f);
            Destroy(bulletShooted2, 0.1f);
            Destroy(bulletShooted3, 0.1f);
        }

        Bullet script = bullet.GetComponent<Bullet>();
        script.ApplyForce(new Vector2(1, 0));
    }

    void ChangeBullet(GameObject newBullet)
    {
        bullet = newBullet;
        gunType = bullet.name;
        Debug.Log(gunType);
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "FatBirdFall":
                TakeDamage(1);
                break;
            case "Creep":
                TakeDamage(1);
                break;
            case "Asteroid":
                TakeDamage(1);
                break;
            case "Egg":
                TakeDamage(1);
                break;
            case "IonBlasterBox":
                if (gunType.Equals("IonBlasterSingle")) levelGun++;
                ChangeBullet(IonBlaster);
                break;
            case "NeutronGunBox":
                if (gunType.Equals("NeutronGunCI2Medium")) levelGun++;
                ChangeBullet(NeutronGun);
                break;
            case "LaserCannonBox":
                if (gunType.Equals("LaserCannonCI2Weak")) levelGun++;
                ChangeBullet(LaserCanon);
                break;
            case "Power-ups":
                levelGun += 1;
                Debug.Log(levelGun);
                break;
            case "Food":
                AudioManager.Play(AudioClipName.EatDrumStick);
                break;
        }
    }

    // game over
    void GameOver()
    {
        Destroy(gameObject);
        LevelManager.GameOver();
    }


    void TakeDamage(int damage)
    {
        GameManager.Instance.TakeDamage(damage);
        healthPoint -= damage;
        if(levelGun > 1) levelGun--;
        GameObject die = Instantiate(prefabExplosion, transform.position, Quaternion.identity) as GameObject;
        Destroy(die, 1.2f);
        // check for game over
        if (healthPoint == 0)
        {
            Destroy(gameObject);
            GameOver();
        }
    }
}
