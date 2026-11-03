using System;
using System.Collections;
using UnityEngine;

public partial class CursorController : MonoBehaviour {
    
    public GameObject player;
    public ItemController itemController;
    public int xDis;
    public int yDis;

    public Camera buildCam;
    private Vector2 playerPos;
    private UIcontroller menu;
    private Vector3Int buldingPos;

    [SerializeField] Sprite[] cursorSprites;
    [SerializeField] Sprite[] buldingsSprites;

    public int mode = 0;
    private int currentBuild = 0;
    private BuilderController buildController;
    private void Start()
    {
        menu = FindAnyObjectByType<UIcontroller>();
        Vector2 playerPos = player.transform.position;
        buildController = FindAnyObjectByType<BuilderController>();
    }
    void Update() {

        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();

        if (mode == 0)
        {
            var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3Int tilePos = GetWorldPosTile(mouseWorldPos);
            transform.position = tilePos + new Vector3(0.5f, 0.5f, -1);

            Vector2 playerPos = player.transform.position;


            //Determine the postion of cursor relative to player
            if (tilePos.x > playerPos.x + xDis || tilePos.x < playerPos.x - xDis || tilePos.y > playerPos.y + yDis - 1 || tilePos.y < playerPos.y - yDis - 1)
            {
                sprite.color = Color.red; // turn red and disable if to far away

                if (Input.GetMouseButtonDown(0))
                {
                    sprite.sprite = cursorSprites[1];
                }
            }
            else //in range for item use
            {
                sprite.color = Color.white; // turn whit and active if in range

                if (Input.GetMouseButtonDown(0))
                {
                    sprite.sprite = cursorSprites[1];
                    itemController.UseItem(tilePos);
                }

                if (Input.GetMouseButtonDown(1))
                {
                    sprite.sprite = cursorSprites[1];
                    itemController.UseItemrightClick(tilePos);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                sprite.sprite = cursorSprites[0];
            }
        }
        else if (mode == 1)//switch to the building camers
        {
            var mouseWorldBuildPos = buildCam.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int tilePos = GetWorldPosTile(mouseWorldBuildPos);
            transform.position = tilePos;
            StartCoroutine(wait());
            if (buildController.checkNoOverlaps(tilePos, (int)sprite.sprite.bounds.size.x, (int)sprite.sprite.bounds.size.y)) //check if overalping with other build or is in ainvalid position
            {
                //if position if valid on click confim position
                sprite.color = Color.white;
                if (Input.GetMouseButtonDown(0))
                {
                    buildController.setBuildPosition(tilePos, (int)sprite.sprite.bounds.size.x, (int)sprite.sprite.bounds.size.y);
                    mode = 2;
                }
            }
            else
            {
                sprite.color = Color.red;
            }

        }
        else if (mode == 2)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mode = 1;
            }
        }
    }

    public void setSprite(int num)
    {
        mode = 1;
        currentBuild = num;
        gameObject.GetComponent<SpriteRenderer>().sprite = buldingsSprites[num];
    }
    public static Vector3Int GetWorldPosTile(Vector3 worldPos) {
        int xInt = Mathf.FloorToInt(worldPos.x);
        int yInt = Mathf.FloorToInt(worldPos.y);
        return new(xInt, yInt, 0);
    }

    IEnumerator wait()
    {
        //returning 0 will make it wait 1 frame
        yield return 0;
    }
}
