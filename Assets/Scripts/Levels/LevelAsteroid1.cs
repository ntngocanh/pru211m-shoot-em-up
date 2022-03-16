using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAsteroid1 : MonoBehaviour
{
    [SerializeField]
    string nextLevelName;
    // needed for spawning
    [SerializeField]
    GameObject prefabAsteroid;
    // spawn location support
    const int SpawnBorderSize = 50;
    int minSpawnX;
    int maxSpawnX;
    int minSpawnY;
    int maxSpawnY;
    // Start is called before the first frame update
    void Start()
    {
        // save spawn boundaries for efficiency
        minSpawnX = SpawnBorderSize;
        maxSpawnX = Screen.width - SpawnBorderSize;
        minSpawnY = SpawnBorderSize;
        maxSpawnY = Screen.height - SpawnBorderSize;
        SpawnBear();
    }

    // Update is called once per frame
    void SpawnBear()
    {
        // generate random location and create new teddy bear
        Vector3 location = new Vector3(Random.Range(minSpawnX, maxSpawnX),
            Random.Range(minSpawnY, maxSpawnY),
            -Camera.main.transform.position.z);
        Vector3 worldLocation = Camera.main.ScreenToWorldPoint(location);
        GameObject teddyBear = Instantiate(prefabAsteroid) as GameObject;
        teddyBear.transform.position = worldLocation;
    }
    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Asteroid").Length == 0
            && GameObject.FindGameObjectsWithTag("NeutronGunBox").Length == 0
            && GameObject.FindGameObjectsWithTag("IonBlasterBox").Length == 0
            && GameObject.FindGameObjectsWithTag("LaserCannonBox").Length == 0) Invoke("LoadLevel", 2);;
    }

    void LoadLevel(){
        GameManager.Instance.LoadLevel(nextLevelName);
    }
}
