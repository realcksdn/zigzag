
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private GameObject tileRrefab;
    [SerializeField]
    private Transform currentTile;

    [SerializeField]
    private int spawnTileCountAtStart = 100;

    [SerializeField] private FeverManager feverManager;

    private void Awake()
    {
        for (int i = 0; i < spawnTileCountAtStart; ++ i)
        {
            CreateTile();
        }
    }

    void CreateTile()
    {
        GameObject clone = Instantiate(tileRrefab);

        clone.transform.SetParent(transform);

        clone.GetComponent<Tile>().Setup(this); //4-2 타일 재생할때 TileSpawner 필요해서 Setup메소드 매개변수로 넘김

        clone.transform.GetChild(1).GetComponent<Item>().Setup(gameController);
        clone.transform.GetChild(1).GetComponent<Item>().Setup1(feverManager);
        SpawnTile(clone.transform);
    }
    public void SpawnTile(Transform tile)
    {
        tile.gameObject.SetActive(true);

        int index = Random.Range(0, 2);
        Vector3 addPosition = index == 0 ? Vector3.right : Vector3.forward;
        tile.position = currentTile.position + addPosition;

        currentTile = tile;

        int spawnItem = Random.Range(0, 100);
        if (spawnItem < 20)
        {
            tile.GetChild(1).gameObject.SetActive(true);
        }
    }
}
