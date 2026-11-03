using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemController : MonoBehaviour 
{
    public DualGridTilemap dualGridTM;

    public Tilemap placeHolderTM;
    public Tilemap groundTM;
    public Tilemap walkInFrontTM;
    public Tilemap collisionTM;
    public Tilemap walkBehinedTM;
    public Tilemap decorTM;

    public PlantDictionary PD;
  
    public PlantController PC;

    public HotbarController hotbar;
    public SusSystem susSystem;
    private InventoryController inventoryController;

    private void Start()
    {
        inventoryController = FindAnyObjectByType<InventoryController>();
    }
    public void UseItemrightClick(Vector3Int pos)
    {
        int ActiveItem = hotbar.GetItemInSlot();
        //turn garss to dirt with hoe
        if (ActiveItem == 1)
        {
            dualGridTM.SetCell(pos, dualGridTM.grassPlaceholderTile);
        }

    }

        public void UseItem(Vector3Int pos)
    {
        int ActiveItem = hotbar.GetItemInSlot();

        
        //turn garss to dirt with hoe
        if (ActiveItem == 1)
        {
            dualGridTM.SetCell(pos, dualGridTM.dirtPlaceholderTile);
        }

        //place soil on dirt
        if (ActiveItem == 2 && placeHolderTM.GetTile(pos) == dualGridTM.dirtPlaceholderTile && walkInFrontTM.GetTile(pos) == null && collisionTM.GetTile(pos) == null && walkBehinedTM.GetTile(pos) == null)
        {
            walkInFrontTM.SetTile(pos, PD.bananaPlant[0]);
            inventoryController.removeFromSlot();
        }

        //plant banana on soiled dirt
        if (ActiveItem == 6 && walkInFrontTM.GetTile(pos) == PD.bananaPlant[0] && collisionTM.GetTile(pos) == null)
        {
            walkInFrontTM.SetTile(pos, null);
            collisionTM.SetTile(pos, PD.bananaPlant[1]);
            PC.BananaPlantStartPlanting(pos, PD.bananaPlant, 6);
            inventoryController.removeFromSlot();
        }

        //Chop down Grown bananaTrees
        if (ActiveItem == 7 && collisionTM.GetTile(pos) == PD.bananaPlant[7])
        {
            walkInFrontTM.SetTile(pos, PD.bananaPlant[0]);
            collisionTM.SetTile(pos, null);
            walkBehinedTM.SetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), null);
            PC.BananaPlantChopped(pos, 5, 6);

            if(Random.Range(0, 100) <= 10){
                PC.BananaPlantChopped(pos, 36, 37);
            }
        }

        //plant Black banana on soiled dirt
        if (ActiveItem == 13 && walkInFrontTM.GetTile(pos) == PD.bananaPlant[0] && collisionTM.GetTile(pos) == null)
        {
            walkInFrontTM.SetTile(pos, null);
            collisionTM.SetTile(pos, PD.BlackPlant[1]);
            PC.BananaPlantStartPlanting(pos, PD.BlackPlant, 13);
            inventoryController.removeFromSlot();

            //chance increase sus when planted
            susSystem.PlantingSusIncrease(50, 1);
        }

        //Chop down Grown Black bananaTrees
        if (ActiveItem == 7 && collisionTM.GetTile(pos) == PD.BlackPlant[7])
        {
            walkInFrontTM.SetTile(pos, PD.bananaPlant[0]);
            collisionTM.SetTile(pos, null);
            walkBehinedTM.SetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), null);
            PC.BananaPlantChopped(pos, 8, 13);

            
        }

        //plant coc banana on soiled dirt
        if (ActiveItem == 14 && walkInFrontTM.GetTile(pos) == PD.bananaPlant[0] && collisionTM.GetTile(pos) == null)
        {
            walkInFrontTM.SetTile(pos, null);
            collisionTM.SetTile(pos, PD.CocPlant[1]);
            PC.BananaPlantStartPlanting(pos, PD.CocPlant, 14);
            inventoryController.removeFromSlot();

            //chance increase sus when planted
            susSystem.PlantingSusIncrease(50, 1);

        }

        //Chop down Grown Coc bananaTrees
        if (ActiveItem == 7 && collisionTM.GetTile(pos) == PD.CocPlant[7])
        {
            walkInFrontTM.SetTile(pos, PD.bananaPlant[0]);
            collisionTM.SetTile(pos, null);
            walkBehinedTM.SetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), null);
            PC.BananaPlantChopped(pos, 9, 14);

           
        }

        //plant fent banana on soiled dirt
        if (ActiveItem == 15 && walkInFrontTM.GetTile(pos) == PD.bananaPlant[0] && collisionTM.GetTile(pos) == null)
        {
            walkInFrontTM.SetTile(pos, null);
            collisionTM.SetTile(pos, PD.FentPlant[1]);
            PC.BananaPlantStartPlanting(pos, PD.FentPlant, 15);
            inventoryController.removeFromSlot();

            //chance increase sus when planted
            susSystem.PlantingSusIncrease(75, 0.5f);
        }

        //Chop down Grown Fent bananaTrees
        if (ActiveItem == 7 && collisionTM.GetTile(pos) == PD.FentPlant[7])
        {
            walkInFrontTM.SetTile(pos, PD.bananaPlant[0]);
            collisionTM.SetTile(pos, null);
            walkBehinedTM.SetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), null);
            PC.BananaPlantChopped(pos, 11, 15);

        
        }

        //plant Meth banana on soiled dirt
        if (ActiveItem == 16 && walkInFrontTM.GetTile(pos) == PD.bananaPlant[0] && collisionTM.GetTile(pos) == null)
        {
            walkInFrontTM.SetTile(pos, null);
            collisionTM.SetTile(pos, PD.MethPlant[1]);
            PC.BananaPlantStartPlanting(pos, PD.MethPlant,16);
            inventoryController.removeFromSlot();

            //chance increase sus when planted
            susSystem.PlantingSusIncrease(75, 0.5f);
        }

        //Chop down Grown Meth bananaTrees
        if (ActiveItem == 7 && collisionTM.GetTile(pos) == PD.MethPlant[7])
        {
            walkInFrontTM.SetTile(pos, PD.bananaPlant[0]);
            collisionTM.SetTile(pos, null);
            walkBehinedTM.SetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), null);
            PC.BananaPlantChopped(pos, 10, 16);

        }
        
        //plant LSD banana on soiled dirt
        if (ActiveItem == 17 && walkInFrontTM.GetTile(pos) == PD.bananaPlant[0] && collisionTM.GetTile(pos) == null)
        {
            walkInFrontTM.SetTile(pos, null);
            collisionTM.SetTile(pos, PD.LSDPlant[1]);
            PC.BananaPlantStartPlanting(pos, PD.LSDPlant, 17);
            inventoryController.removeFromSlot();

            //chance increase sus when planted
            susSystem.PlantingSusIncrease(60, 0.8f);
        }

        //Chop down Grown LSD bananaTrees
        if (ActiveItem == 7 && collisionTM.GetTile(pos) == PD.LSDPlant[7])
        {
            walkInFrontTM.SetTile(pos, PD.bananaPlant[0]);
            collisionTM.SetTile(pos, null);
            walkBehinedTM.SetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), null);
            PC.BananaPlantChopped(pos, 12, 17);
        }

        //plant Staight banana on soiled dirt
        if (ActiveItem == 20 && walkInFrontTM.GetTile(pos) == PD.bananaPlant[0] && collisionTM.GetTile(pos) == null)
        {
            walkInFrontTM.SetTile(pos, null);
            collisionTM.SetTile(pos, PD.StarightPlant[0]);
            walkBehinedTM.SetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), PD.StarightPlant[1]);
            PC.BananaPlantStartPlanting(pos, PD.StarightPlant, 20);
            inventoryController.removeFromSlot();
        }

        //Chop down Grown Staight bananaTrees
        if (ActiveItem == 7 && collisionTM.GetTile(pos) == PD.StarightPlant[7])
        {
            walkInFrontTM.SetTile(pos, PD.bananaPlant[0]);
            collisionTM.SetTile(pos, null);
            walkBehinedTM.SetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), null);
            PC.BananaPlantChopped(pos, 18, 20);
        }

        //plant TNT banana on soiled dirt
        if (ActiveItem == 21 && walkInFrontTM.GetTile(pos) == PD.bananaPlant[0] && collisionTM.GetTile(pos) == null)
        {
            walkInFrontTM.SetTile(pos, null);
            collisionTM.SetTile(pos, PD.TNTPlant[0]);
            walkBehinedTM.SetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), PD.TNTPlant[1]);
            PC.BananaPlantStartPlanting(pos, PD.TNTPlant, 21);
            inventoryController.removeFromSlot();

            //chance increase sus when planted
            susSystem.PlantingSusIncrease(75, 0.5f);
        }

        //Chop down Grown TNT bananaTrees
        if (ActiveItem == 7 && collisionTM.GetTile(pos) == PD.TNTPlant[7])
        {
            walkInFrontTM.SetTile(pos, PD.bananaPlant[0]);
            collisionTM.SetTile(pos, null);
            walkBehinedTM.SetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), null);
            PC.BananaPlantChopped(pos, 19, 21);

        }

        //plant Acid banana on soiled dirt
        if (ActiveItem == 23 && walkInFrontTM.GetTile(pos) == PD.bananaPlant[0] && collisionTM.GetTile(pos) == null)
        {
            walkInFrontTM.SetTile(pos, null);
            collisionTM.SetTile(pos, PD.acidPlant[1]);
            PC.BananaPlantStartPlanting(pos, PD.acidPlant, 23);
            inventoryController.removeFromSlot();

            //chance increase sus when planted
            susSystem.PlantingSusIncrease(75, 0.5f);
        }

        //Chop down Grown Acid bananaTrees
        if (ActiveItem == 7 && collisionTM.GetTile(pos) == PD.acidPlant[7])
        {
            walkInFrontTM.SetTile(pos, PD.bananaPlant[0]);
            collisionTM.SetTile(pos, null);
            walkBehinedTM.SetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), null);
            PC.BananaPlantChopped(pos, 22, 23);

            
        }

        //plant Beer banana on soiled dirt
        if (ActiveItem == 25 && walkInFrontTM.GetTile(pos) == PD.bananaPlant[0] && collisionTM.GetTile(pos) == null)
        {
            walkInFrontTM.SetTile(pos, null);
            collisionTM.SetTile(pos, PD.beerPlant[1]);
            PC.BananaPlantStartPlanting(pos, PD.beerPlant, 25);
            inventoryController.removeFromSlot();

            //chance increase sus when planted
            susSystem.PlantingSusIncrease(90, 0.3f);
        }

        //Chop down Grown Beer bananaTrees
        if (ActiveItem == 7 && collisionTM.GetTile(pos) == PD.beerPlant[7])
        {
            walkInFrontTM.SetTile(pos, PD.bananaPlant[0]);
            collisionTM.SetTile(pos, null);
            walkBehinedTM.SetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), null);
            PC.BananaPlantChopped(pos, 24, 25);

           
        }

        //plant Bloody banana on soiled dirt
        if (ActiveItem == 27 && walkInFrontTM.GetTile(pos) == PD.bananaPlant[0] && collisionTM.GetTile(pos) == null)
        {
            walkInFrontTM.SetTile(pos, null);
            collisionTM.SetTile(pos, PD.bloodyPlant[1]);
            PC.BananaPlantStartPlanting(pos, PD.bloodyPlant, 27);
            inventoryController.removeFromSlot();

            //chance increase sus when planted
            susSystem.PlantingSusIncrease(75, 0.5f);
        }

        //Chop down Grown Bloody bananaTrees
        if (ActiveItem == 7 && collisionTM.GetTile(pos) == PD.bloodyPlant[7])
        {
            walkInFrontTM.SetTile(pos, PD.bananaPlant[0]);
            collisionTM.SetTile(pos, null);
            walkBehinedTM.SetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), null);
            PC.BananaPlantChopped(pos, 26, 27);


        }

        //plant Glass banana on soiled dirt
        if (ActiveItem == 29 && walkInFrontTM.GetTile(pos) == PD.bananaPlant[0] && collisionTM.GetTile(pos) == null)
        {
            walkInFrontTM.SetTile(pos, null);
            collisionTM.SetTile(pos, PD.glassPlant[1]);
            PC.BananaPlantStartPlanting(pos, PD.glassPlant, 29);
            inventoryController.removeFromSlot();

            //chance increase sus when planted
            susSystem.PlantingSusIncrease(90, 0.3f);
        }

        //Chop down Grown Glass bananaTrees
        if (ActiveItem == 7 && collisionTM.GetTile(pos) == PD.glassPlant[7])
        {
            walkInFrontTM.SetTile(pos, PD.bananaPlant[0]);
            collisionTM.SetTile(pos, null);
            walkBehinedTM.SetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), null);
            PC.BananaPlantChopped(pos, 28, 29);


        }

        //plant Grenade banana on soiled dirt
        if (ActiveItem == 31 && walkInFrontTM.GetTile(pos) == PD.bananaPlant[0] && collisionTM.GetTile(pos) == null)
        {
            walkInFrontTM.SetTile(pos, null);
            collisionTM.SetTile(pos, PD.grenadePlant[1]);
            PC.BananaPlantStartPlanting(pos, PD.grenadePlant, 31);
            inventoryController.removeFromSlot();

            //chance increase sus when planted
            susSystem.PlantingSusIncrease(60, 0.8f);
        }

        //Chop down Grown Grenade bananaTrees
        if (ActiveItem == 7 && collisionTM.GetTile(pos) == PD.grenadePlant[7])
        {
            walkInFrontTM.SetTile(pos, PD.bananaPlant[0]);
            collisionTM.SetTile(pos, null);
            walkBehinedTM.SetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), null);
            PC.BananaPlantChopped(pos, 30, 31);
        }

        //plant nuke banana on soiled dirt
        if (ActiveItem == 33 && walkInFrontTM.GetTile(pos) == PD.bananaPlant[0] && collisionTM.GetTile(pos) == null)
        {
            walkInFrontTM.SetTile(pos, null);
            collisionTM.SetTile(pos, PD.nukePlant[1]);
            PC.BananaPlantStartPlanting(pos, PD.nukePlant, 33);
            inventoryController.removeFromSlot();

            //chance increase sus when planted
            susSystem.PlantingSusIncrease(50, 1);
        }

        //Chop down Grown Nuke bananaTrees
        if (ActiveItem == 7 && collisionTM.GetTile(pos) == PD.nukePlant[7])
        {
            walkInFrontTM.SetTile(pos, PD.bananaPlant[0]);
            collisionTM.SetTile(pos, null);
            walkBehinedTM.SetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), null);
            PC.BananaPlantChopped(pos, 32, 33);

           
        }


        //plant Plutonium banana on soiled dirt
        if (ActiveItem == 35 && walkInFrontTM.GetTile(pos) == PD.bananaPlant[0] && collisionTM.GetTile(pos) == null)
        {
            walkInFrontTM.SetTile(pos, null);
            collisionTM.SetTile(pos, PD.plutoniumePlant[1]);
            PC.BananaPlantStartPlanting(pos, PD.plutoniumePlant, 35);
            inventoryController.removeFromSlot();

            //chance increase sus when planted
            susSystem.PlantingSusIncrease(50, 1);
        }

        //Chop down Grown Plutonium bananaTrees
        if (ActiveItem == 7 && collisionTM.GetTile(pos) == PD.plutoniumePlant[7])
        {
            walkInFrontTM.SetTile(pos, PD.bananaPlant[0]);
            collisionTM.SetTile(pos, null);
            walkBehinedTM.SetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), null);
            PC.BananaPlantChopped(pos, 34, 35);

           
        }

        //plant Poisonous banana on soiled dirt
        if (ActiveItem == 37 && walkInFrontTM.GetTile(pos) == PD.bananaPlant[0] && collisionTM.GetTile(pos) == null)
        {
            walkInFrontTM.SetTile(pos, null);
            collisionTM.SetTile(pos, PD.poisonousPlant[1]);
            PC.BananaPlantStartPlanting(pos, PD.poisonousPlant, 37);
            inventoryController.removeFromSlot();
        }

        //Chop down Grown Poisonous bananaTrees
        if (ActiveItem == 7 && collisionTM.GetTile(pos) == PD.poisonousPlant[7])
        {
            walkInFrontTM.SetTile(pos, PD.bananaPlant[0]);
            collisionTM.SetTile(pos, null);
            walkBehinedTM.SetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), null);
            PC.BananaPlantChopped(pos, 36, 37);
        }


        // plant Razorblade banana on soiled dirt
        if (ActiveItem == 39 && walkInFrontTM.GetTile(pos) == PD.bananaPlant[0] && collisionTM.GetTile(pos) == null)
        {
            walkInFrontTM.SetTile(pos, null);
            collisionTM.SetTile(pos, PD.razorPlant[1]);
            PC.BananaPlantStartPlanting(pos, PD.razorPlant, 39);
            inventoryController.removeFromSlot();
            //chance increase sus when planted
            susSystem.PlantingSusIncrease(75, 0.5f);
        }

        //Chop down Grown Razorblade bananaTrees
        if (ActiveItem == 7 && collisionTM.GetTile(pos) == PD.razorPlant[7])
        {
            walkInFrontTM.SetTile(pos, PD.bananaPlant[0]);
            collisionTM.SetTile(pos, null);
            walkBehinedTM.SetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), null);
            PC.BananaPlantChopped(pos, 38, 39);

            
        }

        // plant Steel banana on soiled dirt
        if (ActiveItem == 41 && walkInFrontTM.GetTile(pos) == PD.bananaPlant[0] && collisionTM.GetTile(pos) == null)
        {
            walkInFrontTM.SetTile(pos, null);
            collisionTM.SetTile(pos, PD.steelPlant[1]);
            PC.BananaPlantStartPlanting(pos, PD.steelPlant, 41);
            inventoryController.removeFromSlot();

            //chance increase sus when planted
            susSystem.PlantingSusIncrease(90, 0.3f);
        }

        //Chop down Grown Steel bananaTrees
        if (ActiveItem == 7 && collisionTM.GetTile(pos) == PD.steelPlant[7])
        {
            walkInFrontTM.SetTile(pos, PD.bananaPlant[0]);
            collisionTM.SetTile(pos, null);
            walkBehinedTM.SetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), null);
            PC.BananaPlantChopped(pos, 40, 41);

            
        }

        // plant Stones banana on soiled dirt
        if (ActiveItem == 43 && walkInFrontTM.GetTile(pos) == PD.bananaPlant[0] && collisionTM.GetTile(pos) == null)
        {
            walkInFrontTM.SetTile(pos, null);
            collisionTM.SetTile(pos, PD.stonePlant[1]);
            PC.BananaPlantStartPlanting(pos, PD.stonePlant, 43);
            inventoryController.removeFromSlot();

            //chance increase sus when planted
            susSystem.PlantingSusIncrease(90, 0.3f);
        }

        //Chop down Grown Stones bananaTrees
        if (ActiveItem == 7 && collisionTM.GetTile(pos) == PD.stonePlant[7])
        {
            walkInFrontTM.SetTile(pos, PD.bananaPlant[0]);
            collisionTM.SetTile(pos, null);
            walkBehinedTM.SetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), null);
            PC.BananaPlantChopped(pos, 42, 43);

            
        }

        // plant toxic banana on soiled dirt
        if (ActiveItem == 45 && walkInFrontTM.GetTile(pos) == PD.bananaPlant[0] && collisionTM.GetTile(pos) == null)
        {
            walkInFrontTM.SetTile(pos, null);
            collisionTM.SetTile(pos, PD.toxicPlant[1]);
            PC.BananaPlantStartPlanting(pos, PD.toxicPlant, 45);
            inventoryController.removeFromSlot();


            //chance increase sus when planted
            susSystem.PlantingSusIncrease(60, 0.8f);
        }

        //Chop down Grown toxic bananaTrees
        if (ActiveItem == 7 && collisionTM.GetTile(pos) == PD.toxicPlant[7])
        {
            walkInFrontTM.SetTile(pos, PD.bananaPlant[0]);
            collisionTM.SetTile(pos, null);
            walkBehinedTM.SetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), null);
            PC.BananaPlantChopped(pos, 44, 45);


        }

        // plant Weed banana on soiled dirt
        if (ActiveItem == 47 && walkInFrontTM.GetTile(pos) == PD.bananaPlant[0] && collisionTM.GetTile(pos) == null)
        {
            walkInFrontTM.SetTile(pos, null);
            collisionTM.SetTile(pos, PD.weedPlant[0]);
            walkBehinedTM.SetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), PD.weedPlant[1]);
            PC.BananaPlantStartPlanting(pos, PD.weedPlant, 47);
            inventoryController.removeFromSlot();
            //chance increase sus when planted
            susSystem.PlantingSusIncrease(90, 0.3f);
        }

        //Chop down Grown weed bananaTrees
        if (ActiveItem == 7 && collisionTM.GetTile(pos) == PD.weedPlant[7])
        {
            walkInFrontTM.SetTile(pos, PD.bananaPlant[0]);
            collisionTM.SetTile(pos, null);
            walkBehinedTM.SetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), null);
            PC.BananaPlantChopped(pos, 46, 47);

           
        }

        // plant Wood banana on soiled dirt
        if (ActiveItem == 49 && walkInFrontTM.GetTile(pos) == PD.bananaPlant[0] && collisionTM.GetTile(pos) == null)
        {
            walkInFrontTM.SetTile(pos, null);
            collisionTM.SetTile(pos, PD.woodPlant[1]);
            PC.BananaPlantStartPlanting(pos, PD.woodPlant, 49);
            inventoryController.removeFromSlot();

            //chance increase sus when planted
            susSystem.PlantingSusIncrease(90, 0.3f);
        }

        //Chop down Grown wood bananaTrees
        if (ActiveItem == 7 && collisionTM.GetTile(pos) == PD.woodPlant[7])
        {
            walkInFrontTM.SetTile(pos, PD.bananaPlant[0]);
            collisionTM.SetTile(pos, null);
            walkBehinedTM.SetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), null);
            PC.BananaPlantChopped(pos, 48, 49);


        }
    }
}


