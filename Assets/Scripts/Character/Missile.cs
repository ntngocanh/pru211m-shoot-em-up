using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private GameObject point;
    private Rigidbody2D rb;
    public float moveSpeed = 3f;
    private Vector2 movement;
    void Start()
    {
        point = new GameObject();
        Vector3 vector = new Vector3(0, 0, 0);
        point.transform.position = vector;
        rb = this.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        print(transform.position.y);
        if (Mathf.Abs(transform.position.y) < 0.01)
        {
            Destroy(gameObject);
        }
        Vector3 direction = point.transform.position - gameObject.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
        
    }
    private void FixedUpdate()
    {
        
        moveCharacter(movement);
    }
    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

}
