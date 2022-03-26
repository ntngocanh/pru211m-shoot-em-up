using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class SpawnFromFile : MonoBehaviour
{
    Spawner spawner;
    public GameObject prefabFatBird;
    public GameObject prefabBat;
    public string levelName;
    public string nextLevelName;
    Vector3 newScale;
    private Dictionary<string, Spawner> dictionary;
    // Start is called before the first frame update
    void Start()
    {
        ReadFromJson(levelName);
        SpawnMultiple();
    }

    void ReadFromJson(string levelName)
    {
        using (StreamReader r = new StreamReader("Assets/Resources/spawninginfo.json"))
        {
            string json = r.ReadToEnd();
            dictionary = JsonConvert.DeserializeObject<Dictionary<string, Spawner>>(json);
            print(dictionary[levelName].creepName);
            spawner = dictionary[levelName];
        }
    }

    void SpawnMultiple()
    {
        // retrieve block size
        GameObject tempBlock = Instantiate<GameObject>(prefabBat);
        BoxCollider2D collider = tempBlock.GetComponent<BoxCollider2D>();
        float blockWidth = collider.size.x;
        float blockHeight = collider.size.y;
        Destroy(tempBlock);

        int blocksPerRow = spawner.creepPerRow;
        // calculate blocks per row and make sure left block position centers row
        float screenWidth = ScreenUtils.ScreenRight - ScreenUtils.ScreenLeft;
        //calculate scale to transform creep and creep size
        float creepWidth = screenWidth / (1.5f * (blocksPerRow + 1));
        float transformScale = creepWidth / blockWidth;
        float creepHeight = blockHeight * transformScale;

        float leftBlockOffset = ScreenUtils.ScreenLeft + creepWidth * 1.5f;

        float rightBlockOffset = ScreenUtils.ScreenRight - creepWidth * 1.5f;

        float topRowOffset = ScreenUtils.ScreenTop - creepHeight * spawner.topRowOffset;

        Vector2 leftSpawnPoint = new Vector2(ScreenUtils.ScreenLeft, ScreenUtils.ScreenTop);
        Vector2 rightSpawnPoint = new Vector2(ScreenUtils.ScreenRight, ScreenUtils.ScreenTop);

        //Vector2 currentPosition = new Vector2(rightBlockOffset, topRowOffset);
        //Vector2 target1 = new Vector2(leftBlockOffset, topRowOffset);
        newScale = new Vector3(transformScale, transformScale, transformScale);

        int moveToRight = 1;
        for (int row = 0; row < spawner.row; row++)
        {
            Vector2 spawnPoint = new Vector2();
            Vector2 target1 = new Vector2(leftBlockOffset, topRowOffset);
            Vector2 currentPosition = new Vector2(rightBlockOffset, topRowOffset);
            currentPosition.y -= row * creepHeight * 1.2f;
            target1.y -= row * creepHeight * 1.2f;
            if (row % 2 == 0)
            {
                spawnPoint = leftSpawnPoint;
                currentPosition.x = rightBlockOffset;
            }
            else
            {
                spawnPoint = rightSpawnPoint;
                currentPosition.x = leftBlockOffset;
                target1.x = rightBlockOffset;
            }
            spawnPoint.x -= creepWidth * 1.5f * moveToRight;
            spawnPoint.y += creepHeight * 1.2f;
            for (int column = 0; column < blocksPerRow; column++)
            {
                SpawnCreep(spawnPoint, target1, currentPosition);
                currentPosition.x -= creepWidth * 1.5f * moveToRight;
                spawnPoint.y += creepHeight * 1.2f;
            }
            moveToRight *= -1;
        }
    }
    void SpawnCreep(Vector2 spawnPoint, Vector2 target1, Vector2 target2)
    {
        if (spawner.creepName.Equals("FatBirdCreep"))
        {
            GameObject spawned = Instantiate(prefabFatBird, spawnPoint, Quaternion.identity);
            spawned.transform.localScale *= spawner.scale;
            FatBirdCreep script = spawned.GetComponent<FatBirdCreep>();
            script.Health = spawner.health;
            script.PointHit = spawner.pointHit;
            script.PointDie = spawner.pointDie;
            script.target1 = target1;
            script.target2 = target2;
            script.MinEggSpawnTime = spawner.minEggSpawnTime;
            script.MaxEggSpawnTime = spawner.maxEggSpawnTime;
            script.EggForce = spawner.eggForce;
            script.fly = true;
        }
        else if(spawner.creepName.Equals("BatCreep")){
            GameObject spawned = Instantiate(prefabBat, spawnPoint, Quaternion.identity);
            spawned.transform.localScale *= spawner.scale;
            BatCreep script = spawned.GetComponent<BatCreep>();
            script.Health = spawner.health;
            script.PointHit = spawner.pointHit;
            script.PointDie = spawner.pointDie;
            script.target1 = target1;
            script.target2 = target2;
            script.MinEggSpawnTime = spawner.minEggSpawnTime;
            script.MaxEggSpawnTime = spawner.maxEggSpawnTime;
            script.EggForce = spawner.eggForce;
            script.fly = true;
        }
        else{
            GameObject spawned = Instantiate(prefabFatBird, spawnPoint, Quaternion.identity);
            spawned.transform.localScale *= spawner.scale;
            FatBirdCreep script = spawned.GetComponent<FatBirdCreep>();
            script.Health = spawner.health;
            script.PointHit = spawner.pointHit;
            script.PointDie = spawner.pointDie;
            script.target1 = target1;
            script.target2 = target2;
            script.MinEggSpawnTime = spawner.minEggSpawnTime;
            script.MaxEggSpawnTime = spawner.maxEggSpawnTime;
            script.EggForce = spawner.eggForce;
            script.fly = true;
        }

    }

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("FatBirdFall").Length == 0
            && GameObject.FindGameObjectsWithTag("Creep").Length == 0
            && GameObject.FindGameObjectsWithTag("Food").Length == 0
            && GameObject.FindGameObjectsWithTag("Egg").Length == 0
            && GameObject.FindGameObjectsWithTag("NeutronGunBox").Length == 0
            && GameObject.FindGameObjectsWithTag("IonBlasterBox").Length == 0
            && GameObject.FindGameObjectsWithTag("LaserCannonBox").Length == 0)
            Invoke("LoadLevel", 1);
    }

    void LoadLevel()
    {
        GameManager.Instance.LoadLevel(nextLevelName);
    }
}
