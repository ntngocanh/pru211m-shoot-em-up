using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject prefabExplosion;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up*10, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Destroy if collide with creep
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("Bullet");
        if(collision.gameObject.tag == "FatBirdFall")
        {
            Destroy(gameObject);
        }
    }
}
