using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EasterEggSpawner : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject[] EasterEggObjects;
    float distance;
    Vector3 leftBottomCorner;
    Vector3 rightTopCorner;
    Transform player;
    Timer easterSpawnTimer;
    int easterCounter = 0;
    [SerializeField] float easterEggSpawnTime = 120;
    [SerializeField] float velocity = 3f;
    float zOffset = 3f;

    // Start is called before the first frame update
    void Start()
    {
        easterSpawnTimer = new Timer(easterEggSpawnTime);
        easterSpawnTimer.StartTimer();
        player = GameObject.Find("Player").GetComponent<Transform>();
        distance = player.position.z - mainCamera.transform.position.z;
        leftBottomCorner = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, distance));
        rightTopCorner = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, distance));
    }

    // Update is called once per frame
    void Update()
    {
        distance = player.position.z - mainCamera.transform.position.z;
        leftBottomCorner = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, distance));
        rightTopCorner = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, distance));
        if (easterSpawnTimer.TimeOut)
        {
            Spawn(easterCounter);
            easterSpawnTimer.StartTimer();
            easterCounter++;
            if(easterCounter == EasterEggObjects.Length)
            {
                easterCounter = 0;
                this.enabled = false;
            }
 
        }
    }


    void Spawn(int index)
    {   
        GameObject spawned = Instantiate(EasterEggObjects[index], LocationGenerator(), EasterEggObjects[index].transform.rotation);
        spawned.AddComponent<Rigidbody>();
        spawned.GetComponent<Rigidbody>().useGravity = false;
        if(spawned.transform.position.x>player.position.x)
        {
            //sola hiz
            spawned.GetComponent<Rigidbody>().velocity = new Vector3(-1,0,0) * velocity;
        }
        else
        {
            spawned.GetComponent<Rigidbody>().velocity = new Vector3(1, 0, 0) * velocity;
        }
        
    }

    Vector3 LocationGenerator()
    {
        Vector3 location = Vector3.zero;
        int side = Random.Range(0, 2); //0 is left 1 is top 2 is right 3 is bottom
        if (side == 0)
        {
            location = new Vector3(leftBottomCorner.x, Random.Range(leftBottomCorner.y, rightTopCorner.y), player.position.z+zOffset);
        }
        else if (side == 1)
        {
            location = new Vector3(rightTopCorner.x, Random.Range(leftBottomCorner.y, rightTopCorner.y), player.position.z+zOffset);
        }

        return location;
    }
}
