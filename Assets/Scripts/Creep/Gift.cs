using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject spacecraft = GameObject.FindGameObjectWithTag("Player");
        GetComponent<Rigidbody2D>().AddForce(-1 * spacecraft.transform.up * 2, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < ScreenUtils.ScreenBottom)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
    public void ApplyForce(Vector2 forceDirection)
    {
        const float forceMagnitude = 2;
        GetComponent<Rigidbody2D>().AddForce(
            forceMagnitude * forceDirection,
            ForceMode2D.Impulse
        );
    }
}
