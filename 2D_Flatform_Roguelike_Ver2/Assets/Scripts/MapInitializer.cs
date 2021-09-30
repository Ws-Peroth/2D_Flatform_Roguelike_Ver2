using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct MapInformation
{
    public static int x = 60;   // Map x size
    public static int y = 60;   // Map y size
}
public class MapInitializer : MonoBehaviour
{
    public float progressCount;

    protected int[,] map = new int[MapInformation.y, MapInformation.x];
    protected int mapX;
    protected int mapY;
    protected Type startStructure;

    protected void InitMapData()
    {
        // Progress Reset
        progressCount = 0;

        mapX = map.GetLength(1);
        mapY = map.GetLength(0);

        // Set Default Tile
        for (int y = 0; y < mapY; y++)
        {
            for (int x = 0; x < mapX; x++)
            {
                map[y, x] = 1;
            }
        }

        // Set Types
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

    protected class MapLocation
    {
        public int x1, y1;
        public int x2, y2;
        public int x3, y3;
        public int x4, y4;
        public MapLocation()
        {
            SetData(0, 0, 0, 0, 0, 0, 0, 0);
        }
        public MapLocation(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
        {
            SetData(x1, x2, x3, x4, y1, y2, y3, y4);
        }

        public MapLocation SetData(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
        {
            this.y1 = y1; this.x1 = x1;
            this.y2 = y2; this.x2 = x2;
            this.y3 = y3; this.x3 = x3;
            this.y4 = y4; this.x4 = x4;
            return this;
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