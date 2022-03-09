using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    int points;
    int healthPoints;
    // Start is called before the first frame update
    void Start()
    {
                // apply impulse force to get teddy bear moving
        ApplyForce(GetRandomDirectionVector());
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {
            print("Bullet hit");
            //AudioManager.Play(AudioClipName.AsteroidHit);
            Destroy(coll.gameObject);

            // destroy or split as appropriate
            if (transform.localScale.x < 0.5f)
            {
                Destroy(gameObject);
            }
            else
            {
                // shrink asteroid to half size
                Vector3 scale = transform.localScale;
                scale.x /= 2;
                scale.y /= 2;
                transform.localScale = scale;

                // cut collider radius in half
                CircleCollider2D collider = GetComponent<CircleCollider2D>();
                collider.radius /= 2;

                // clone twice and destroy original
                GameObject newAsteroid = Instantiate<GameObject>(gameObject,
                                         transform.position, Quaternion.identity);
                newAsteroid.GetComponent<Asteroid>().StartMoving(
                    Random.Range(0, 2 * Mathf.PI));
                GameObject newAsteroid2 = Instantiate<GameObject>(gameObject,
                    transform.position, Quaternion.identity);
                newAsteroid2.GetComponent<Asteroid>().StartMoving(
                    Random.Range(0, 2 * Mathf.PI));
                Destroy(gameObject);
            }
        }
    }

    public void ApplyForce(Vector2 forceDirection){
        const float forceMagnitude = 5;
        GetComponent<Rigidbody2D>().AddForce(
            forceMagnitude*forceDirection,
            ForceMode2D.Impulse
        );
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    Vector2 GetRandomDirectionVector()
    {
        float angle = Random.Range(0, 2 * Mathf.PI);
        return new Vector2(
            Mathf.Cos(angle), Mathf.Sin(angle));
    }
    public void StartMoving(float angle)
    {
        // apply impulse force to get asteroid moving
        const float MinImpulseForce = 0f;
        const float MaxImpulseForce = 0.5f;
        Vector2 moveDirection = new Vector2(
            Mathf.Cos(angle), Mathf.Sin(angle));
        float magnitude = Random.Range(MinImpulseForce, MaxImpulseForce);
        GetComponent<Rigidbody2D>().AddForce(
            moveDirection * magnitude,
            ForceMode2D.Impulse);
    }

}
