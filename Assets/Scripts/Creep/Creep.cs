using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creep : MonoBehaviour
{
    
    protected int pointHit = 0;
    protected int pointDie = 0;
    protected int health;
    protected int Health{
        set{
            health = value;
        }
    }
    protected int PointHit{
        set{
            pointHit = value;
        }
    }
    protected int PointDie{
        set{
            pointDie = value;
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
    public virtual void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {
            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
                DropItem();
            }
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
