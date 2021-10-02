using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct MapInformation
{
    public static int x = 50;   // Map x 크기
    public static int y = 50;   // Map y 크기
}
public class MapInitializer : MonoBehaviour
{
    [SerializeField] protected bool isDebugMode;

    protected int[,] map = new int[MapInformation.y, MapInformation.x];
    protected int mapX;
    protected int mapY;

    public float progressCount;
    protected Type startStructure;

    protected void InitMapData()
    {
        // Progress 리셋
        progressCount = 0;

        // 시작지점 구조물 템플릿
        startStructure = new Type(new int[,]
            {
                { 2,  4,  4,  4,  4,  4,  4,  4,  4,  4,  3 },
                { 6,  0,  0,  0,  0,  0,  0,  0,  0,  0,  11 },
                { 6,  0,  0,  0,  0,  0,  0,  0,  0,  0,  11 },
                { 6,  0,  0,  0,  0,  0,  0,  0,  0,  0,  11 },
                { 6,  0,  0,  0,  0,  0,  0,  0,  0,  0,  11 },
                { 6,  0,  0,  0,  0,  0,  0,  0,  0,  0,  11 },
                { 6,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1 },
                { 6,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1 },
                { 5, 13, 13, 13, 13, 13, 13, 13, 13, 13,  9 }
            });
    }
    protected void InitMapGenerationData()
    {
        mapX = map.GetLength(1);
        mapY = map.GetLength(0);

        // map의 값 초기화
        for (int y = 0; y < mapY; y++)
        {
            for (int x = 0; x < mapX; x++)
            {
                map[y, x] = 1;
            }
        }
    }

    /// <summary>
    /// [position information class] field : startX = 0, startY = 0, endX = 0, endY = 0
    /// </summary>
    protected class MapLocationPosition
    {
        public int startX, endX;
        public int startY, endY;

        public MapLocationPosition(int startX = 0, int startY = 0, int endX = 0, int endY = 0)
        {
            this.startX = startX;
            this.startY = startY;

            this.endX = endX;
            this.endY = endY;
        }

    }

}

public struct Type
{
    public int x;
    public int y;
    public int[,] structure;

    public Type(int[,] structure)
    {
        this.structure = structure;
        x = structure.GetLength(1);
        y = structure.GetLength(0);
    }
}