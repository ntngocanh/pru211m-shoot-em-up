using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    float colliderHalfWidth;
    float colliderHalfHeight;
    public GameObject eggYolk;
    // Start is called before the first frame update
    void Start()
    {
        GameObject spacecraft = GameObject.FindGameObjectWithTag("Player");
        GetComponent<Rigidbody2D>().AddForce(-1*spacecraft.transform.up * 4, ForceMode2D.Impulse);
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        colliderHalfWidth = collider.size.x / 2;
        colliderHalfHeight = collider.size.y / 2;
    }

    // Update is called once per frame
    void Update()
    {
        ClampInScreen();
        if (transform.position.y == ScreenUtils.ScreenBottom + colliderHalfHeight)
        {
            Destroy(gameObject);
            GameObject eggYolkClone = Instantiate(eggYolk, transform.position, Quaternion.identity) as GameObject;
            Destroy(eggYolkClone, 3);
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
        const float forceMagnitude = 5;
        GetComponent<Rigidbody2D>().AddForce(
            forceMagnitude * forceDirection,
            ForceMode2D.Impulse
        );
    }
    void ClampInScreen()
    {
        // clamp position as necessary
        Vector3 position = transform.position;
        if (position.x - colliderHalfWidth < ScreenUtils.ScreenLeft)
        {
            position.x = ScreenUtils.ScreenLeft + colliderHalfWidth;
        }
        else if (position.x + colliderHalfWidth > ScreenUtils.ScreenRight)
        {
            position.x = ScreenUtils.ScreenRight - colliderHalfWidth;
        }
        if (position.y + colliderHalfHeight > ScreenUtils.ScreenTop)
        {
            position.y = ScreenUtils.ScreenTop - colliderHalfHeight;
        }
        else if (position.y - colliderHalfHeight < ScreenUtils.ScreenBottom)
        {
            position.y = ScreenUtils.ScreenBottom + colliderHalfHeight;
            print(transform.position.y + "    " + (ScreenUtils.ScreenBottom + colliderHalfHeight));

        }
        transform.position = position;
    }
}
