using UnityEngine;

public class MapGenerator : MapInitializer
{
    public void GenerateRandomMap()
    {
        // Initialize Map Data
        InitMapData();
        MakeMap();
    }
    private void MakeMap()
    {
        print("Start Make Map");
        DivideMap(5, 0, 0, mapY, mapX);
    }

    private MapLocation DivideMap(int depth, int x1, int y1, int x2, int y2)
    {
        MapLocation location = new MapLocation();

        // 2. 방을만듦
        if (depth == 0 || x2 - x1 <= 10 || y2 - y1 <= 10)
        {
            return MakeRoom(x1, x2, y1, y2);
        }

        // 1. 분할
        // 3. 병합
        int rLen = x2 - x1;
        int cLen = y2 - y1;
        MapLocation temp1, temp2;


        if (rLen / cLen > 1 || (cLen / rLen < 1 && IsVerticalWay()))
        {
            // 세로로 분할
            int divideNum = (x2 - x1) * GetRandomDivideSize();

            // 방 분할
            temp1 = DivideMap(depth - 1, x1, y1, x1 + divideNum, y2);
            temp2 = DivideMap(depth - 1, x1 + divideNum, y1, x2, y2);

            // 방 병합
            MergeRoomVertical(temp1, temp2);
        }
        else
        {
            // 가로로 분할
            int divideNum = (y2 - y1) * GetRandomDivideSize();

            // 방 분할
            temp1 = DivideMap(depth - 1, x1, y1, x2, y1 + divideNum);
            temp2 = DivideMap(depth - 1, x1, y1 + divideNum, x2, y2);

            // 방 병합
            MergeRoomHorizon(temp1, temp2);
        }
        location.SetData(temp1.x1, temp1.y1, temp1.x2, temp1.y2, temp2.x3, temp2.y3, temp2.x4, temp2.y4);

        return location;
    }

    private MapLocation MakeRoom(int x1, int x2, int y1, int y2)
    {
        for (int y = x1 + 2; y < x2 - 2; y++)
        {
            for (int x = y1 + 2; x < y2 - 2; x++)
            {
                map[y, x] = 0;
            }
        }

        return new MapLocation(x1 + 2, y1 + 2, x2 - 3, y2 - 3, x1 + 2, y1 + 2, x2 - 3, y2 - 3);
    }

    private bool IsVerticalWay() => Random.Range(0, 2) % 2 == 0;    

    private int GetRandomDivideSize() => (Random.Range(0, 4) + 4) / 10;
    

    private void MergeRoomVertical(MapLocation temp1, MapLocation temp2)
    {
        map[temp1.x4 + 1, (temp1.y3 + temp1.y4) / 2] = 0;
        map[temp1.x4 + 2, (temp1.y3 + temp1.y4) / 2] = 0;

        map[temp2.x1 - 1, (temp2.y1 + temp2.y2) / 2] = 0;
        map[temp2.x1 - 2, (temp2.y1 + temp2.y2) / 2] = 0;

        int rmin = Mathf.Min((temp1.y3 + temp1.y4) / 2, (temp2.y1 + temp2.y2) / 2);
        int rmax = Mathf.Max((temp1.y3 + temp1.y4) / 2, (temp2.y1 + temp2.y2) / 2);

        for (int x = rmin; x <= rmax; x++)
        {
            map[temp2.x1 - 2, x] = 0;
        }
    }
    private void MergeRoomHorizon(MapLocation temp1, MapLocation temp2)
    {
        map[(temp1.x3 + temp1.x4) / 2, temp1.y4 + 1] = 0;
        map[(temp1.x3 + temp1.x4) / 2, temp1.y4 + 2] = 0;

        map[(temp2.x1 + temp2.x2) / 2, temp2.y1 - 1] = 0;
        map[(temp2.x1 + temp2.x2) / 2, temp2.y1 - 2] = 0;

        int rmin = Mathf.Min((temp1.x3 + temp1.x4) / 2, (temp2.x1 + temp2.x2) / 2);
        int rmax = Mathf.Max((temp1.x3 + temp1.x4) / 2, (temp2.x1 + temp2.x2) / 2);

        for (int i = rmin; i <= rmax; i++)
        {
            map[i, temp2.y1 - 2] = 0;
        }
    }



}
