using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapMaker : MapGenerateBSP
{
    [SerializeField] private GameObject tile;   // Dummy Tile Object
    [SerializeField] protected List<Sprite> tileSprites = new List<Sprite>(); // Tilemap Sprites

    protected GameObject[,] tileMapObjects = new GameObject[MapInformation.y, MapInformation.x];

    private Vector3 worldStart;
    private int tileKind = 0;   // 현재 타일 종류
    private float tileSize; // 타일 크기

    protected void GenerateTileMapObject()
    {
        // 타일 크기 설정
        tileSize = tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x;

        // 시작 지점 설정
        worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        // 맵 생성 시작
        MakeMapBSP();

        // 타일맵 생성
        CreatTilemap();

        // 생성 종료
        progressCount = 1;
    }

    private void CreatTilemap()
    {
        for(int y = 0; y < mapY; y++)
        {
            for(int x = 0; x < mapX; x++)
            {
                // 해당 위치에 타일맵 생성
                tileMapObjects[y, x] = PlaceTile(x, y);
            }
        }
    }

    private GameObject PlaceTile(int x, int y)
    {
        // Dummy Tile 생성
        GameObject newTile =
            Instantiate(tile,
                        new Vector3(worldStart.x + (tileSize * x), worldStart.y - (tileSize * y), 0),
                        Quaternion.identity,
                        transform
                        );

        // map에서 생성할 타일의 종류를 가져옴
        tileKind = map[y, x];

        // 타일 종류에 맞는 Sprite 부여
        newTile.GetComponent<SpriteRenderer>().sprite = tileSprites[tileKind];
        return newTile;
    }


}