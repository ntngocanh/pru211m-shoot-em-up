using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepSpawner1 : MonoBehaviour
{
    [SerializeField]
    GameObject prefabFatBird;
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

        float totalBlockWidth = blocksPerRow * blockWidth;
        float leftBlockOffset = ScreenUtils.ScreenLeft + creepWidth*1.5f;

        float topRowOffset = ScreenUtils.ScreenTop - creepHeight;

        // add rows of blocks
        Vector2 currentPosition = new Vector2(leftBlockOffset, topRowOffset);
        for (int row = 0; row < 3; row++)
        {
            for (int column = 0; column < blocksPerRow; column++)
            {
                GameObject spawned = Instantiate(prefabFatBird, currentPosition,
                    Quaternion.identity);
                Vector3 newScale = new Vector3(transformScale, transformScale, transformScale);
                spawned.transform.localScale = newScale;
                currentPosition.x += creepWidth*1.5f;
            }

            // move to next row
            currentPosition.x = leftBlockOffset;
            currentPosition.y -= creepHeight;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
