using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    float colliderHalfWidth;
    float colliderHalfHeight;
    // Start is called before the first frame update
    int point = 10;
    void Start()
    {
        float angle = Random.Range(0, 2 * Mathf.PI);
        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        GetComponent<Rigidbody2D>().AddForce(direction * 6, ForceMode2D.Impulse);

        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        colliderHalfWidth = collider.size.x / 2;
        colliderHalfHeight = collider.size.y / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < ScreenUtils.ScreenBottom)
        {
            Destroy(gameObject);
        }
        gameObject.transform.Rotate(new Vector3(0,0, 1)); //applying rotation
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.AddPoints(point);
            GameManager.Instance.AddFood();
            Destroy(gameObject);
        }
    }
}
