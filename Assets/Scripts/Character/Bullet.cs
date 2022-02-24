using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject prefabExplosion;
    protected Timer deathTimer;
    protected float lifeSeconds;

    [SerializeField]
    private Rigidbody2D myRigidbody;
    [SerializeField]
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        lifeSeconds = 5;
        deathTimer = gameObject.AddComponent<Timer>();
        deathTimer.Duration = lifeSeconds;
        deathTimer.Run();
        GameObject spacecraft = GameObject.FindGameObjectWithTag("Player");
        //GetComponent<Rigidbody2D>().AddForce(spacecraft.transform.up*10, ForceMode2D.Impulse);
        myRigidbody = GetComponent<Rigidbody2D>() as Rigidbody2D;
    }

    public void Setup(Vector2 moveDirection)
    {
        //Debug.Log("Velocity: " + myRigidbody.velocity);
        myRigidbody.velocity = moveDirection.normalized * 10f;
        //Debug.Log("rigidbody position" + myRigidbody.transform.position);
        //Debug.Log("Move Direction.normalized " + moveDirection.normalized + " and speed is: " + 87f);
        //Debug.Log("Velocity: " + moveDirection.normalized * 87f);
    }
    
    // Update is called once per frame
    void Update()
    {
        //this.transform.position = this.direction * this.speed * Time.deltaTime;

        // kill bullet when timer is done
        if (deathTimer.Finished)
        {
            Destroy(gameObject);
        }
    }
    // Destroy if collide with creep
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "FatBirdFall")
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //GameObject gameObject = GameObject.FindGameObjectWithTag("Bullet");
        if(collision.gameObject.tag == "FatBirdFall")
        {
            Destroy(gameObject);
        }
    }
    public void ApplyForce(Vector2 forceDirection){
        const float forceMagnitude = 10;
        GetComponent<Rigidbody2D>().AddForce(
            forceMagnitude*forceDirection,
            ForceMode2D.Impulse
        );
    }
}
