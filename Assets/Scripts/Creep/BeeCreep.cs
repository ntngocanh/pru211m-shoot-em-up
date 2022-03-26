using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeCreep : Creep
{
    public GameObject egg;
    public GameObject food;
    public GameObject gift1;
    public GameObject gift2;
    public GameObject gift3;
    public GameObject diePrefab;
    public GameObject powerup;
    Timer spawnTimer;

    public Vector2 target1;
    public Vector2 target2;
    bool arrived1;
    bool arrived2;
    public bool fly;
    float speed = 9;
    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = gameObject.AddComponent<Timer>();
        StartRandomTimer();
    }
    // Update is called once per frame
    void Update()
    {
        if(fly){
            
            if((Vector2)transform.position == target1) arrived1 = true;
            if((Vector2)transform.position == target2) arrived2 = true;
            if(!arrived1 && !arrived2){
            transform.position = Vector2.MoveTowards(transform.position, target1, Time.deltaTime * speed);
            }else if(!arrived2){
                transform.position = Vector2.MoveTowards(transform.position, target2, Time.deltaTime * speed);
            }
        }
        if(spawnTimer.Finished) {
            CreepFire();
            StartRandomTimer();
            }
    }
    void FixedUpdate(){
        
    }
    void StartRandomTimer()
    {
		spawnTimer.Duration = Random.Range(minEggSpawnTime, maxEggSpawnTime);
		spawnTimer.Run();
	}
    void CreepFire()
    {
        GameObject eggClone = Instantiate(egg, transform.position, Quaternion.identity) as GameObject;
        Egg script = eggClone.GetComponent<Egg>();
        script.ApplyForce(new Vector2(0,-1), eggForce);
    }
    public override void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet") || coll.gameObject.CompareTag("Player"))
        {
            TakeDamage();
        }
    }
    public void DropFood(int from, int to){
        int rand = Random.Range(from, to+1);
        for(int i = 0; i < rand; i++){
            GameObject foodClone = Instantiate(food, transform.position, Quaternion.identity) as GameObject;
        }
    }
    public override void DropItem()
    {
        if (Random.Range(0f, 18f) < 1)
        {
            int ran = Random.Range(0, 10);
            if (ran == 0)
            {
                GameObject powerupclone = Instantiate(powerup, transform.position, Quaternion.identity) as GameObject;
                Powerup script = powerup.GetComponent<Powerup>();
                script.ApplyForce(new Vector2(1, 0));
            }
            else if (ran > 0 && ran < 4)
            {
                GameObject giftClone = Instantiate(gift2, transform.position, Quaternion.identity) as GameObject;
                Gift script = gift2.GetComponent<Gift>();
                script.ApplyForce(new Vector2(1, 0));
            }
            else if (ran > 3 && ran < 7)
            {
                GameObject giftClone = Instantiate(gift3, transform.position, Quaternion.identity) as GameObject;
                Gift script = gift3.GetComponent<Gift>();
                script.ApplyForce(new Vector2(1, 0));
            }
            else
            {
                GameObject giftClone = Instantiate(gift1, transform.position, Quaternion.identity) as GameObject;
                Gift script = gift1.GetComponent<Gift>();
                script.ApplyForce(new Vector2(1, 0));
            }
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {
            TakeDamage();
        }
    }
    public void TakeDamage()
    {
        health--;
        GameManager.Instance.AddPoints(pointHit);
        if (health <= 0)
        {
            GameManager.Instance.AddPoints(pointDie);
            Destroy(gameObject);
            GameObject die = Instantiate(diePrefab, transform.position, Quaternion.identity) as GameObject;
            Destroy(die, 0.15f);
            DropItem();
            DropFood(2, 4);
        }else{
            DropFood(1, 2);
        }
    }
}
