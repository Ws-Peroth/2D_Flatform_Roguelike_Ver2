using UnityEngine;
public struct MapInformation
{
    public const int X = 50;   // Map x 크기
    public const int Y = 50;   // Map y 크기
}

public class MapInitializer : MonoBehaviour
{
    [SerializeField]
    protected bool isDebugMode;

    protected GameObject[,] tileMapObjects;
    protected int[,] map;
    protected int mapX;
    protected int mapY;

    // public float progressCount;
    protected Type startStructure;

    protected void InitializeMapData(int X = MapInformation.X, int Y = MapInformation.Y)
    {
        // Progress 값 리셋
        // progressCount = 0;

        print($"map size [{Y}, {X}]");
        map = new int[Y, X];

        mapX = map.GetLength(0);
        mapY = map.GetLength(1);

        // map의 값 초기화
        for (var y = 0; y < mapY; y++)
        {
            for (var x = 0; x < mapX; x++)
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
        x = structure.GetLength(0);
        y = structure.GetLength(1);
    }
}