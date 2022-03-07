using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : MonoBehaviour
{
    [SerializeField]
    string nextLevelName;
    [SerializeField]
    GameObject prefabFatBird;
    Vector3 newScale;
    // Start is called before the first frame update
    void Start()
    {
        // retrieve block size
        GameObject tempBlock = Instantiate<GameObject>(prefabFatBird);
        BoxCollider2D collider = tempBlock.GetComponent<BoxCollider2D>();
        float blockWidth = collider.size.x;
        float blockHeight = collider.size.y;
        Destroy(tempBlock);
        
        int blocksPerRow = 10;
        // calculate blocks per row and make sure left block position centers row
        float screenWidth = ScreenUtils.ScreenRight - ScreenUtils.ScreenLeft;
        //calculate scale to transform creep and creep size
        float creepWidth = screenWidth / (1.5f*(blocksPerRow + 1));
        float transformScale = creepWidth/blockWidth;
        float creepHeight = blockHeight * transformScale;

        float leftBlockOffset = ScreenUtils.ScreenLeft + creepWidth*1.5f;

        float rightBlockOffset = ScreenUtils.ScreenRight - creepWidth*1.5f;

        float topRowOffset = ScreenUtils.ScreenTop - creepHeight;

        Vector2 leftSpawnPoint = new Vector2(ScreenUtils.ScreenLeft, ScreenUtils.ScreenTop);
        Vector2 rightSpawnPoint = new Vector2(ScreenUtils.ScreenRight, ScreenUtils.ScreenTop);
        
        //Vector2 currentPosition = new Vector2(rightBlockOffset, topRowOffset);
        //Vector2 target1 = new Vector2(leftBlockOffset, topRowOffset);
        newScale = new Vector3(transformScale, transformScale, transformScale);
        
        int moveToRight = 1;
        for(int row = 0; row < 3; row++){
            Vector2 spawnPoint = new Vector2();
            Vector2 target1 = new Vector2(leftBlockOffset, topRowOffset);
            Vector2 currentPosition = new Vector2(rightBlockOffset, topRowOffset);
            currentPosition.y -= row*creepHeight*1.2f;
            target1.y -= row*creepHeight*1.2f;
            if(row%2 == 0){
                spawnPoint = leftSpawnPoint;
                currentPosition.x = rightBlockOffset;
            }else{
                spawnPoint = rightSpawnPoint;
                currentPosition.x = leftBlockOffset;
                target1.x = rightBlockOffset;
            }
            spawnPoint.x -= creepWidth*1.5f*moveToRight;
            spawnPoint.y += creepHeight*1.2f;
            for(int column = 0; column < blocksPerRow; column++){
                SpawnCreep(spawnPoint, target1, currentPosition);
                currentPosition.x -= creepWidth*1.5f*moveToRight;
                spawnPoint.y += creepHeight*1.2f;
            }
            moveToRight *= -1;
        }    
        
    }

    void SpawnCreep(Vector2 spawnPoint, Vector2 target1, Vector2 target2){
        GameObject spawned = Instantiate(prefabFatBird, spawnPoint, Quaternion.identity);
        spawned.transform.localScale = newScale;
        FatBirdCreep script = spawned.GetComponent<FatBirdCreep>();
        script.target1 = target1;
        script.target2 = target2;
        script.fly = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("FatBirdFall").Length == 0) GameManager.Instance.LoadLevel(nextLevelName);
    }
}
