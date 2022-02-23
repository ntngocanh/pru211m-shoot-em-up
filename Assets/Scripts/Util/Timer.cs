using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float totalSecond = 0;
    float elapsedSecond = 0;
    bool running = false;
    bool started = false;

    public void Run()
    {
        if (totalSecond > 0)
        {
            running = true;
            started = true;
            elapsedSecond = 0;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            elapsedSecond += Time.deltaTime;
            if (elapsedSecond >= totalSecond)
            {
                running = false;
            }
        }
    }
    public float Duration
    {
        set
        {
            if (!running)
            {
                totalSecond = value;
            }
        }
    }
    public bool Finished
    {
        get { return started && !running; }
    }
    public bool Running
    {
        get { return running; }
    }
}
