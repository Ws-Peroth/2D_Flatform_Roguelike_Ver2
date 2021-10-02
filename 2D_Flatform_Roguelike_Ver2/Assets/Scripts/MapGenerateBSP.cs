using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapGenerateBSP : MapInitializer
{
    // 통로 최대 범위
    private readonly int wideMax = 5;
    
    // 각 구역별 border구역 크기
    private readonly int borderSizeX = 2;
    private readonly int borderSizeY = 2;

    private int boardLimit;
    private int BoardLimit 
    { 
        get => boardLimit;

        // 적용할 border의 최솟값은 1
        set => boardLimit = value > 1 ? value : 1;
    }

    // 방 분할 비율
    private int RoomDivideRatio { get => Random.Range(0, 3) + 4; }

    public void MakeMapBSP()
    {
        // 정보 초기화
        InitMapData();

        // 맵 정보 초기화
        InitMapGenerationData();

        // 맵 생성
        DivideMap(10, 0, 0, mapX, mapX);
    }

    private MapLocationPosition DivideMap(int depth, int startX, int startY, int endX, int endY)
    {
        // depth  : 깊이
        int lengthX = endX - startX;
        int lengthY = endY - startY;

        // 깊이가 0 이거나 사용 가능한 공간이 10 이하일 경우에 방을 생성
        if (depth == 0 || lengthX <= 10 || lengthY <= 10)
        {
            return MakeRoom(startX, startY, endX, endY);
        }
        MapLocationPosition temp1, temp2;
        int dividenum;


        // X의 길이가 더 길 경우 세로방향으로 분할
        if (lengthX > lengthY)
        {
            dividenum = RoomDivideRatio * lengthX / 10;

            // x 좌표의 끝을 현재 x + 나눌 범위 로 갱신 
            temp1 = DivideMap(depth - 1, startX, startY, startX + dividenum, endY);

            // x 좌표의 시작부분을 현재 x + 나눌 범위로 갱신
            temp2 = DivideMap(depth - 1, startX + dividenum, startY, endX, endY);

            // 분할한 두 방을 병합
            MergeVerticalRoom(temp1, temp2);
        }
        else    // 가로 방향으로 분할
        {
            dividenum = RoomDivideRatio * lengthY / 10;

            // y 좌표의 끝을 현재 y + 나눌 범위로 갱신
            temp1 = DivideMap(depth - 1, startX, startY, endX, startY + dividenum);

            // y 좌표의 시작 부분을 현재 y + 나눌 범위로 갱신
            temp2 = DivideMap(depth - 1, startX, startY + dividenum, endX, endY);

            // 분할한 두 방을 병합
            MergeHorizonRoom(temp1, temp2);
        }

        return new MapLocationPosition(temp1.startX, temp1.startY, temp1.endX, temp1.endY);
    }
    private MapLocationPosition MakeRoom(int startX, int startY, int endX, int endY)
    {
        // (startX, startY) ~ (endX, endY) 의 구역 중, border만큼을 띄우고 방을 생성
        for (int y = startY + borderSizeY; y < endY - borderSizeY; y++)
        {
            for (int x = startX + borderSizeX; x < endX - borderSizeX; x++)
            {
                map[y, x] = 0;
            }
        }

        // 실제 방의 위치를 반환함
        // 방이 생성된 이치는 border의 안쪽임으로, border만큼 연산을 하여 반환
        return new MapLocationPosition
            (
                startX + borderSizeX,
                startY + borderSizeY,
                endX - borderSizeX - 1,
                endY - borderSizeY - 1
            );
    }

    private void MergeVerticalRoom(MapLocationPosition temp1, MapLocationPosition temp2)
    {
        // 생성할 통로의 넓이
        int wide;

        // 임의의 넓이를 받아옴
        wide = GetRandomVerticalWide(0, wideMax, temp1.startY, temp1.endY);

        // 두 구역의 시작부분끼리 이어진 통로를 생성
        ConnectVerticalRoom(temp1.endX, temp2.startX, temp1.startY, wide);

        // 임의의 넓이를 받아옴
        wide = GetRandomVerticalWide(0, wideMax, temp1.startY, temp1.endY);

        // 두 구역의 끝부분끼리 이어진 통로를 생성
        ConnectVerticalRoom(temp1.endX, temp2.startX, temp1.endY, wide);
    }

    private int GetRandomVerticalWide(int min, int max, int startLimit, int endLimit, int defaultValue = 0)
    {
        // 기본값 설정
        int wide = defaultValue;

        // 기본값을 부여할 기준값 설정
        BoardLimit = borderSizeY / 2;

        for (int i = 0; i < 3; i++)
        {
            // 임의의 통로 범위 설정
            wide = Random.Range(min, max + 1);

            // 통로 범위가 map을 초과하는지 검사
            // 통로 범위가 map을 초과하면 기본값을 대입
            if (startLimit - (wide / 2) <= BoardLimit)
                wide = defaultValue;

            else if (endLimit + (wide / 2) >= mapY - BoardLimit)
                wide = defaultValue;
        }

        return wide;
    }

    private void ConnectVerticalRoom(int start, int end, int lockedPosition, int connectWide = 0)
    {
        // Debug용 함수
        int tileNum = GetTileNumber(connectWide);

        // 통로 좌우 넓이
        int wide = connectWide / 2;

        // 두 구역에 통로 연결
        for (int x = start; x < end; x++)
        {
            // 통로 넓이 적용
            for (int y = lockedPosition - wide; y <= lockedPosition + wide; y++)
            {
                map[y, x] = tileNum;
            }
        }
    }

    private void MergeHorizonRoom(MapLocationPosition temp1, MapLocationPosition temp2)
    {
        // 생성할 통로의 넓이
        int wide;

        // 임의의 넓이를 받아옴
        wide = GetRandomHorizonWide(0, wideMax, temp1.startX, temp1.endX);

        // 두 구역의 시작부분끼리 이어진 통로를 생성
        ConnectHorizonRoom(temp1.endY, temp2.startY, temp1.startX, wide);

        // 임의의 넓이를 받아옴
        wide = GetRandomHorizonWide(0, wideMax, temp1.startX, temp1.endX);

        // 두 구역의 끝부분끼리 이어진 통로를 생성
        ConnectHorizonRoom(temp1.endY, temp2.startY, temp1.endX, wide);
    }

    private int GetRandomHorizonWide(int min, int max, int startLimit, int endLimit, int defaultValue = 0)
    {
        // 기본값 설정
        int wide = defaultValue;

        // 기본값을 부여할 기준값 설정
        BoardLimit = borderSizeX / 2;

        for (int i = 0; i < 3; i++)
        {
            // 임의의 통로 범위 설정
            wide = Random.Range(min, max + 1);

            // 임의의 통로 범위가 map을 초과하는지 검사
            // 통로 범위가 map을 초과하면 기본값을 대입
            if (startLimit - (wide / 2) <= BoardLimit)
                wide = defaultValue;

            else if (endLimit + (wide / 2) >= mapX - BoardLimit)
                wide = defaultValue;
        }

        return wide;
    }

    private void ConnectHorizonRoom(int start, int end, int lockedPosition, int connectWide = 0)
    {
        // Debug용 함수
        int tileNum = GetTileNumber(connectWide);

        // 통로 좌우 넓이
        int wide = connectWide / 2;

        // 두 구역에 통로 연결
        for (int y = start; y < end; y++)
        {
            // 통로 넓이 적용
            for (int x = lockedPosition - wide; x <= lockedPosition + wide; x++)
            {
                map[y, x] = tileNum;
            }
        }
    }

    private int GetTileNumber(int wide)
    {
        // 디버그 모드면 채워진 블럭 반환
        if (isDebugMode) return 16;

        // 아니면 비어있는 블럭 반환
        return 0;
    }
}