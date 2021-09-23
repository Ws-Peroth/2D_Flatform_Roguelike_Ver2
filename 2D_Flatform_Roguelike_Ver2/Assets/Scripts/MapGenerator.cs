using System;
using UnityEngine;

public class MapGenerator : MapInitializer
{
    public void GenerateRandomMap()
    {
        // Initialize Map Data
        InitMapData();
        CreatStartStructure();
        MakeMap();
    }

    private void CreatStartStructure()
    {
        CopyStructureToMap(0, 0, 0);
    }

    private void MakeMap()
    {

    }

    private void CopyStructureToMap(int type, int x,  int y)
    {
        Type targetStructure = structureTypes[type];

        for (int i = 0; i < targetStructure.y; i++)
        {
            for(int j = 0; j < targetStructure.x; j++)
            {
                map[y + i, x + j] = targetStructure.structure[i, j];
            }
        }
    }

    private int GetStructureType()
    {
        return UnityEngine.Random.Range(1, structureTypes.Length);   // 0 = start structure
    }

    private bool CheckMapIndexRange(int checkPositionX, int checkPositionY)
    {
        return mapX > checkPositionX && mapY > checkPositionY;
    }

}
