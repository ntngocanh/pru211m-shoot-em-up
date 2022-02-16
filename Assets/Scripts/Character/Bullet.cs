using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject prefabExplosion;

    // Start is called before the first frame update
    void Start()
    {
        GameObject spacecraft = GameObject.FindGameObjectWithTag("Player");
        GetComponent<Rigidbody2D>().AddForce(spacecraft.transform.up*10, ForceMode2D.Impulse);
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
    // Destroy if collide with creep
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
