using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
public class PlantController : MonoBehaviour
{
    public float maxGrowthTime = 60;
    public float minGrowthTime = 30;
    private float growthTime;

    public float maxWaterTime = 30;
    public float minWaterTime = 15;
    private float waterTime;

    public int maxPlantingBananaYeild = 3;
    public int minPlantingBananaYeild = 1;
    private int plantingBananaYeild;

    public int maxBananaYeild = 12;
    public int minBananaYeild = 5;
    private int bananaYeild;

    private int growthStages = 5;

    public PlantDictionary PD;
    public ItemDictionary ID;

    public Tilemap collisonTM;
    public Tilemap walkBehinedTM;

    public float minDropDistance = 1f;
    public float maxDropDistance = 2f;

    private ItemDictionary itemDictionary;
    private void Start()
    {
        itemDictionary = FindAnyObjectByType<ItemDictionary>();
    }
    //Controlles the planting proccess for the regualr banana plant
    public void BananaPlantStartPlanting(Vector3Int pos, Tile[] tiles, int activeItem)
    {
        float growthMulti = itemDictionary.GetItemPrefab(activeItem).GetComponent<Item>().growthMultplier;

        //get times and amounts for growth and drops
        growthTime = (Random.Range(minGrowthTime, maxGrowthTime) * growthMulti) /growthStages;
        waterTime = Random.Range(minWaterTime, maxWaterTime);
        
        StartCoroutine(BananaPlantStage1to2(pos, tiles));
    }
    public void BananaPlantChopped(Vector3Int pos, int banana, int platingbanana)
    {
        plantingBananaYeild = Random.Range(minPlantingBananaYeild, maxPlantingBananaYeild);
        bananaYeild = Random.Range(minBananaYeild, maxBananaYeild);

        //drop banana items when chopped down
        for (int i = 0; i < plantingBananaYeild; i++)
        {
            Vector3 dropOffset = Random.insideUnitCircle.normalized * Random.Range(minDropDistance, maxDropDistance);
            Vector3 dropPosition = pos + dropOffset;

            GameObject droppedItem = Instantiate(ID.GetItemPrefab(platingbanana), dropPosition, Quaternion.identity);
            droppedItem.GetComponent<BounceEffect>().StartBounce();
        }

        for(int i = 0; i < bananaYeild; i++)
        {
            Vector3 dropOffset = Random.insideUnitCircle.normalized * Random.Range(minDropDistance, maxDropDistance);
            Vector3 dropPosition = pos + dropOffset;

            GameObject droppedItem = Instantiate(ID.GetItemPrefab(banana), dropPosition, Quaternion.identity);
            droppedItem.GetComponent<BounceEffect>().StartBounce();
        }
    }
    IEnumerator BananaPlantStage1to2(Vector3Int pos, Tile[] tiles)
    {
        yield return new WaitForSecondsRealtime(growthTime);
        collisonTM.SetTile(pos, tiles[2]);
        walkBehinedTM.SetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), null);
        StartCoroutine(BananaPlantStage2to3(pos, tiles));
    }
    IEnumerator BananaPlantStage2to3(Vector3Int pos, Tile[] tiles)
    {
        yield return new WaitForSecondsRealtime(growthTime);
        collisonTM.SetTile(pos, tiles[3]);
        walkBehinedTM.SetTile(new Vector3Int(pos.x, pos.y+1, pos.z), tiles[4]);
        StartCoroutine(BananaPlantStage3to4(pos, tiles));
    }

    IEnumerator BananaPlantStage3to4(Vector3Int pos, Tile[] tiles)
    {
        yield return new WaitForSecondsRealtime(growthTime);
        collisonTM.SetTile(pos, tiles[5]);
        walkBehinedTM.SetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), tiles[6]);
        StartCoroutine(BananaPlantStage4to5(pos, tiles));
    }

    IEnumerator BananaPlantStage4to5(Vector3Int pos, Tile[] tiles)
    {
        yield return new WaitForSecondsRealtime(growthTime);
        collisonTM.SetTile(pos, tiles[7]);
        walkBehinedTM.SetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), tiles[8]);
    }




}
