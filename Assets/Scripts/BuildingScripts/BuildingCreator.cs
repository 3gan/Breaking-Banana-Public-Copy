using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingCreator : MonoBehaviour
{

    public BuildingListController[] buildingListControllers;
    public GameObject controller;
    public GameObject player;

    public Tilemap placeHolderTM;
    public Tilemap groundTM;
    public Tilemap walkInFrontTM;
    public Tilemap collisionTM;
    public Tilemap walkBehinedTM;
    public Tilemap decorTM;

    public GameObject UI;
    public GameObject GreenHouseUIs;
    public GameObject ShedCollider;
    public GameObject greenHouseMenu;
    public GameObject greenHouseCollider;
    public GameObject labCollider;

    public GameObject BunkerCollier;
    public GameObject bunkerMenu;
    private int greenhousesbuilt;
    public void PlaceBuild(Vector3Int pos, int buildNum)
    {

        //build Small green house
        if (buildNum == 0)
        {
            pos.y--;
            for (int i = 0; i < 8; i++)
            {
                walkBehinedTM.SetTile(pos, buildingListControllers[buildNum].buildingSprites[i]);
                pos.x++;
                if (i == 3)
                {
                    pos.y--;
                    pos.x -= 4;
                }
            }

            pos.y--;
            pos.x -= 4;

            for (int i = 8; i < 24; i++)
            {
                collisionTM.SetTile(pos, buildingListControllers[buildNum].buildingSprites[i]);
                pos.x++;
                if (i == 11 || i == 15 || i == 19)
                {
                    pos.y--;
                    pos.x -= 4;
                }
            }

           

            GameObject ghc = Instantiate(greenHouseCollider, controller.transform);
            ghc.transform.position = new Vector3(pos.x - 2.3f, pos.y - 0.2f, 0);
            GameObject ghm = Instantiate(greenHouseMenu, GreenHouseUIs.transform);
            ghm.gameObject.transform.GetChild(0).gameObject.SetActive(false);

            // for(int i = 0;i < 2; i++)
            // {
            //Debug.Log(ghc + " " + ghm);
            ghm.GetComponent<GreenHouseController>().ghCollider = ghc.gameObject;
            ghc.GetComponent<GreenHouseMenuDiplay>().menu = ghm.gameObject;
            ghc.GetComponent<GreenHouseMenuDiplay>().exitButton = ghm.transform.GetChild(0).transform.GetChild(5).gameObject;
            ghc.GetComponent<GreenHouseMenuDiplay>().player = player.gameObject;
            //  }




        }
        //build Auction shed
        else if (buildNum == 1)
        {
            pos.y--;
            pos.x++;
            for (int i = 0; i < 4; i++)
            {
                walkBehinedTM.SetTile(pos, buildingListControllers[buildNum].buildingSprites[i]);
                pos.x++;
            }

            pos.y--;
            pos.x -= 5;
            for (int i = 4; i < 16; i++)
            {
                walkBehinedTM.SetTile(pos, buildingListControllers[buildNum].buildingSprites[i]);
                pos.x++;

                if (i == 9)
                {
                    pos.y--;
                    pos.x -= 6;
                }
            }

            pos.y--;
            pos.x -= 6;

            for (int i = 16; i < 28; i++)
            {
                collisionTM.SetTile(pos, buildingListControllers[buildNum].buildingSprites[i]);
                pos.x++;

                if (i == 21)
                {
                    pos.y--;
                    pos.x -= 6;
                }
            }

            ShedCollider.transform.position = new Vector3(pos.x - 3.3f, pos.y - 0.3f, 0);
        }
        //trailer lab
        else if (buildNum == 2)
        {
            pos.y--;
            for (int i = 0; i < 21; i++)
            {
                walkBehinedTM.SetTile(pos, buildingListControllers[buildNum].buildingSprites[i]);
                pos.x++;
                if (i == 6 || i == 13)
                {
                    pos.y--;
                    pos.x -= 7;
                }
            }

            pos.y--;
            pos.x -= 7;

            for (int i = 21; i < 44; i++)
            {
                collisionTM.SetTile(pos, buildingListControllers[buildNum].buildingSprites[i]);
                pos.x++;

                if (i == 28 || i == 36)
                {
                    pos.y--;
                    pos.x -= 8;
                }
            }

            labCollider.transform.position = new Vector3(pos.x - 4.7f, pos.y - 0.3f, 0);
        }
        else if (buildNum == 3)
        {
            walkInFrontTM.SetTile(pos, buildingListControllers[buildNum].buildingSprites[0]);
            pos.x++;
            walkInFrontTM.SetTile(pos, buildingListControllers[buildNum].buildingSprites[1]);
            pos.y--;
            pos.x--;
            walkInFrontTM.SetTile(pos, buildingListControllers[buildNum].buildingSprites[2]);
            pos.x++;
            walkInFrontTM.SetTile(pos, buildingListControllers[buildNum].buildingSprites[3]);

            GameObject BC = Instantiate(BunkerCollier, controller.transform);
            BC.transform.position = new Vector3(pos.x -0.25f, pos.y + 0.3f, 0);

            BC.GetComponent<BunkerCollider>().player = player.gameObject;
            BC.GetComponent<BunkerCollider>().menu = bunkerMenu.gameObject;
        }

    }

       
}
