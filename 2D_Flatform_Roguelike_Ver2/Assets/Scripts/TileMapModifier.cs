using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapModifier : TileMapMaker
{
    // Start is called before the first frame update
    void Start()
    {
        GenerateTileMapObject();
        StartCoroutine(ModifyTileMap());
    }

    private IEnumerator ModifyTileMap()
    {
        yield return null;

        print("END");
    }
}
