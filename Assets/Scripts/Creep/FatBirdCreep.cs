using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatBirdCreep : Creep
{
    // Start is called before the first frame update
    void Start()
    {
        Health = 1;
    }

    public Vector2 target1;
    public Vector2 target2;
    bool arrived1;
    bool arrived2;
    public bool fly;
    float speed = 9;
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
    }
}
