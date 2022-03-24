using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    public float moveSpeed = 3f;
    public AudioSource audioSource;
    public GameObject explosion;

    private GameObject point;
    void Start()
    {
        point = new GameObject();
        Vector3 vector = new Vector3(0, 0, 0);
        point.transform.position = vector;
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        //print(transform.position.y);
        //Vector3 direction = point.transform.position - gameObject.transform.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //rb.rotation = angle;
        //direction.Normalize();
        //movement = direction;
        transform.position = Vector2.MoveTowards(transform.position, point.transform.position, moveSpeed * Time.deltaTime);
        transform.up = point.transform.position - transform.position;
        if (transform.position.x == point.transform.position.x && transform.position.y == point.transform.position.y)
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(audioSource.clip, point.transform.position);
            GameObject explosionCLone = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            explosionCLone.transform.localScale += new Vector3(4, 2, -1);
            Destroy(explosionCLone, 2f);

            FatBirdCreep[] creeps = GameObject.FindObjectsOfType(typeof(FatBirdCreep)) as FatBirdCreep[];

            foreach (FatBirdCreep i in creeps)
            {
                i.TakeDamage();
            }

            Asteroid[] asteroids = GameObject.FindObjectsOfType(typeof(Asteroid)) as Asteroid[];

            foreach (Asteroid i in asteroids)
            {
                i.TakeDamage();
            }
        }
    }

}
