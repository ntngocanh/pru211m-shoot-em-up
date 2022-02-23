using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatBirdCreep : Creep
{
    public GameObject egg;
    public GameObject food;
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
        if(Random.Range(0f, 5000f) < 1)
        {
            GameObject eggClone = Instantiate(egg, transform.position, Quaternion.identity) as GameObject;
            Egg script = egg.GetComponent<Egg>();
            script.ApplyForce(new Vector2(1, 0));
        }
    }
    public override void DropItem()
    {
        if (Random.Range(0f, 2f) < 1)
        {
            GameObject foodClone = Instantiate(food, transform.position, Quaternion.identity) as GameObject;
        }
    }
}
