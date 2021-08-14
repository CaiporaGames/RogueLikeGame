using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
    public int mapWidth = 7;
    public int mapHeight = 7;
    public int roomsToGenerate = 12;
    public GameObject roomPrefab;

    int roomCount;
    bool roomsInstantiated;

    Vector2 firstRoomPos;

    bool[,] map;
    List<Room> roomObjects = new List<Room>();

    public static Generation instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Random.InitState(12);
        Generate();
    }

    public void Generate()
    {
        map = new bool[mapWidth, mapHeight];

        CheckRoom(3,3,0,Vector2.zero,true);
        InstantiateRooms();
        FindObjectOfType<Player>().transform.position = firstRoomPos
    }

    void CheckRoom(int x, int y, int remaining, Vector2 generalDirection, bool firstRoom = false)
    {
        if (roomCount >= roomsToGenerate)
        {
            return;
        }
        if (x < 0 || x > mapWidth - 1 || y < 0 || y > mapHeight - 1)
        {
            return;
        }
        if (firstRoom == false && remaining <= 0)
        {
            return;
        }
        if (map[x,y] == true)
        {
            return;
        }
        if (firstRoom == true)
        {
            firstRoomPos = new Vector2(x,y);
        }

        roomCount++;
        map[x, y] = true;

        bool north = Random.value > (generalDirection == Vector2.up ? 0.2f : 0.8f);
        bool south = Random.value > (generalDirection == Vector2.down ? 0.2f : 0.8f);
        bool west = Random.value > (generalDirection == Vector2.left ? 0.2f : 0.8f);
        bool east = Random.value > (generalDirection == Vector2.right ? 0.2f : 0.8f);

        int maxRemaining = roomsToGenerate / 4;

        if (north || firstRoom)
        {
            CheckRoom(x, y + 1, firstRoom ? maxRemaining : remaining - 1, firstRoom ? Vector2.up : generalDirection);
        }
        if (south || firstRoom)
        {
            CheckRoom(x, y - 1, firstRoom ? maxRemaining : remaining - 1, firstRoom ? Vector2.down : generalDirection);
        }
        if (west || firstRoom)
        {
            CheckRoom(x - 1, y, firstRoom ? maxRemaining : remaining - 1, firstRoom ? Vector2.left : generalDirection);
        }
        if (east || firstRoom)
        {
            CheckRoom(x + 1, y, firstRoom ? maxRemaining : remaining - 1, firstRoom ? Vector2.right : generalDirection);
        }

    }

    void InstantiateRooms()
    {

    }

    void CalculateKeyAndExit()
    {

    }

}
