using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    Material material;
    void Awake(){
        material = GetComponent<Renderer>().material;
        DontDestroyOnLoad(gameObject);
    }
    [SerializeField]
    float speed = 0.07f;
    // Start is called before the first frame update
    void Start()
    {
        //offset = new Vector2(xVelocity, yVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = new Vector2(0, Time.deltaTime * speed);
        material.mainTextureOffset += offset;
    }
}
