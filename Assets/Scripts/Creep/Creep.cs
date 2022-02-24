using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creep : MonoBehaviour
{
    protected int health;
    protected int Health{
        set{
            health = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {
            health--;
            if (health <= 0) Destroy(gameObject);
            DropItem();
        }
    }
    public virtual void DropItem()
    {}
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {
            health--;
            if (health <= 0) Destroy(gameObject);
        }
    }
}
