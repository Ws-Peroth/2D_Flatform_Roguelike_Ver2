using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapModifier : TileMapMaker
{
    void Start()
    {
        // 맵 생성 시작
        GenerateTileMapObject();

        // 맵 수정 코루틴 호출
        StartCoroutine(ModifyTileMap());
    }

    private IEnumerator ModifyTileMap()
    {
        int mapNumber = 1;
        while (true)
        {
            yield return new WaitForSeconds(1.5f);

            if (isDebugMode)
            {
                // 디버그 모드면 타일 16번을 전부 공백 타일로 대체
                ModifyFilledTiles();
                yield return new WaitForSeconds(2f);
            }

            print($"End [{mapNumber}]");
            mapNumber++;

            // 타일맵 재생성
            MakeMapBSP();

            // 타일맵 스프라이트 수정
            ModifyTilemap();
        }
    }

    private void ModifyTilemap()
    {
        for (int y = 0; y < mapY; y++)
        {
            for (int x = 0; x < mapX; x++)
            {
                // 타일맵 종류를 가져옴
                int tileKind = map[y, x];
                
                // 새로 생성된 타일맵 적용
                tileMapObjects[y, x].GetComponent<SpriteRenderer>().sprite = tileSprites[tileKind];
            }
        }
    }

    private void ModifyFilledTiles()
    {
        for (int y = 0; y < mapY; y++)
        {
            for (int x = 0; x < mapX; x++)
            {
                // 타일 종류를 가져옴
                int tileKind = map[y, x];

                // 타일앱이 16번일 경우 0번 (공백타일)로 대체
                if (tileKind == 16)
                {
                    // 타일맵 정보 갱신
                    map[y, x] = 0;

                    // 타일맵 적용
                    tileMapObjects[y, x].GetComponent<SpriteRenderer>().sprite = tileSprites[0];
                }
            }
        }
    }
}

