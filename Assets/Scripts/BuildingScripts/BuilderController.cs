using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class BuilderController : MonoBehaviour
{

    public BuildingListController[] buildingListControllers;

    public GameObject buildingMenu;
    public GameObject player;
    public GameObject hotbar;
    public GameObject moneyTab;
    public GameObject SusMeter;
    public GameObject cursor;

    public GameObject confirmButton;
    public GameObject cancelButton;

    public GameObject buildCam;
    public GameObject playerCam;

    public Tilemap collisonTM;
    public Tilemap walkBehinedTM;
    public Tilemap walkInFrontTM;
   
    private Vector3 playerExitPosition;
    private Vector3Int buldingPosition;

    private int buildNum;
    private BuildingCreator buildingCreator;
    
    
    private void Start()
    {
        buildingCreator = FindAnyObjectByType<BuildingCreator>();
       
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            buildingMenu.SetActive(true);
            playerExitPosition = player.transform.position + new Vector3(0, -2, 0); // Move player down when exiting
            player.SetActive(false);
        }
    }

    public void CloseBuildingMenu()
    {
        buildingListControllers[buildNum].buildNowButton.gameObject.SetActive(false);
        buildingListControllers[buildNum].notEnoughMoneyButton.gameObject.SetActive(false);
        buildingListControllers[buildNum].notEnoughResourceButton.gameObject.SetActive(false);
        buildingMenu.SetActive(false);
        player.transform.position = playerExitPosition;
        player.SetActive(true);
    }

    public void BuildScreen(int buildingNum)
    {
        buildNum = buildingNum;
        cursor.GetComponent<CursorController>().setSprite(buildingNum);
        hotbar.SetActive(false);
        moneyTab.SetActive(false);
        SusMeter.SetActive(false);
        playerCam.SetActive(false);
        buildingMenu.SetActive(false);

        confirmButton.SetActive(true);
        cancelButton.SetActive(true);
        buildCam.SetActive(true);

    }
    public bool checkNoOverlaps(Vector3Int worldPos, int width, int height)
    {

        int XB = worldPos.x - width / 2;
        int YB = worldPos.y - height / 2;
        int XT = worldPos.x + width / 2;    
        int YT = worldPos.y + height / 2;

        for (int i = XB; i < XB + width; i++)
        {
           
            for (int j = YB; j < YB + height; j++)
            {
                //check if new build is overlapping pervious build or plant
                if (collisonTM.HasTile(new(i, j, 0)) || walkBehinedTM.HasTile(new(i, j, 0)) || walkInFrontTM.HasTile(new(i, j, 0)))
                {
                    return false;

                }

                //check sides
                if (XB < -28.5 || XT > 34.5 || YT > 16.5 || YB < -20.5)
                {
                    return false;
                }
            }
        }
     
        return true;

    }
    public void setBuildPosition(Vector3Int worldPos, int width, int height)
    {
        // set to top right of sprite
        int X = worldPos.x - width / 2;
        int Y = worldPos.y + height / 2;

        Vector3Int pos = new(X, Y, 0);

        buldingPosition = pos;
    }
    public void ConfirmBuild()
    {
        buildingCreator.PlaceBuild(buldingPosition, buildNum);
        cursor.GetComponent<CursorController>().mode = 0;
        hotbar.SetActive(true);
        moneyTab.SetActive(true);
        SusMeter.SetActive(true);
        playerCam.SetActive(true);
        buildingMenu.SetActive(true);

        confirmButton.SetActive(false);
        cancelButton.SetActive(false);
        buildCam.SetActive(false);

        buildingListControllers[buildNum].UseItemsAndMoney();

        if (buildNum == 1 || buildNum == 2)
        {
            buildingListControllers[buildNum].isSoldOut = true;
            buildingListControllers[buildNum].soldOut.gameObject.SetActive(true);
        }
       
    }

    public void CancelBuild()
    {
        cursor.GetComponent<CursorController>().mode = 0;
        hotbar.SetActive(true);
        moneyTab.SetActive(true);
        SusMeter.SetActive(true);
        playerCam.SetActive(true);
        buildingMenu.SetActive(true);

        confirmButton.SetActive(false);
        cancelButton.SetActive(false);
        buildCam.SetActive(false);
    }
}
