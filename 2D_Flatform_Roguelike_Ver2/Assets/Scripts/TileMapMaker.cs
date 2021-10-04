using System.Collections.Generic;
using UnityEngine;

public abstract class TileMapMaker : MapGeneratorBSP
{
    [SerializeField]
    protected List<Sprite> tileSprites = new List<Sprite>(); // Tilemap Sprites

    [SerializeField]
    private GameObject _tile;   // Dummy Tile Object

    private Vector3 _worldStart;

    private int _tileKind;
    private float _tileSize; // 타일 크기

    protected void InitializeTileMap()
    {
        tileMapObjects = new GameObject[mapY, mapX];
    }

    protected void GenerateTileMapObject()
    {
        // 타일 생성 시작 지점 설정
        _worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        // 타일 크기 설정
        _tileSize = _tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x;

        // 맵 생성 시작
        GenerateMapBSP();

        // 타일맵 생성
        GenerateTilemap();

        // 생성 종료
        // progressCount = 1;
    }

    private void GenerateTilemap()
    {
        for(var y = 0; y < mapY; y++)
        {
            for(var x = 0; x < mapX; x++)
            {
                // 해당 위치에 타일맵 생성
                tileMapObjects[y, x] = PlaceTile(x, y);
            }
        }
    }

    private GameObject PlaceTile(int x, int y)
    {
        // Dummy Tile 생성
        var newTile =
            Instantiate(_tile,
                        new Vector3(_worldStart.x + (_tileSize * x), _worldStart.y - (_tileSize * y), 0),
                        Quaternion.identity,
                        transform
                        );

        // map에서 생성할 타일의 종류를 가져옴
        _tileKind = map[y, x];

        // 타일 종류에 맞는 Sprite 부여
        newTile.GetComponent<SpriteRenderer>().sprite = tileSprites[_tileKind];
        return newTile;
    }
}