using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatBirdCreep : Creep
{
    public GameObject egg;
    public GameObject food;
    public GameObject gift1;
    public GameObject gift2;
    public GameObject gift3;
    public GameObject diePrefab;
    // Start is called before the first frame update
    void Start()
    {
        Health = 1;
    }

    // Update is called once per frame
    void Update()
    {
        CreepFire();
    }

    void CreepFire()
    {
        if(Random.Range(0f, 10000f) < 1)
        {
            GameObject eggClone = Instantiate(egg, transform.position, Quaternion.identity) as GameObject;
            Egg script = egg.GetComponent<Egg>();
            script.ApplyForce(new Vector2(1, 0));
        }
    }
    public override void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {
            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
                GameObject die = Instantiate(diePrefab, transform.position, Quaternion.identity) as GameObject;
                Destroy(die, 0.3f);
                DropItem();
            }
        }
    }
    public override void DropItem()
    {
        if (Random.Range(0f, 3f) < 1)
        {
            GameObject foodClone = Instantiate(food, transform.position, Quaternion.identity) as GameObject;
        }
        if (Random.Range(0f, 10f) < 1)
        {
            int ran = Random.Range(0, 3);
            if(ran == 0)
            {
                GameObject giftClone = Instantiate(gift1, transform.position, Quaternion.identity) as GameObject;
                Gift script = gift1.GetComponent<Gift>();
                script.ApplyForce(new Vector2(1, 0));
            } else if (ran == 1)
            {
                GameObject giftClone = Instantiate(gift2, transform.position, Quaternion.identity) as GameObject;
                Gift script = gift2.GetComponent<Gift>();
                script.ApplyForce(new Vector2(1, 0));
            }
            else
            {
                GameObject giftClone = Instantiate(gift3, transform.position, Quaternion.identity) as GameObject;
                Gift script = gift3.GetComponent<Gift>();
                script.ApplyForce(new Vector2(1, 0));
            }
        }
    }
}
