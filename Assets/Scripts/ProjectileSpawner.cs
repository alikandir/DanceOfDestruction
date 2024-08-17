using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Transform player;
    [SerializeField] GameObject projectile;
    [SerializeField] float spawnCooldown = 3f;
    float distance;
    Vector3 leftBottomCorner;
    Vector3 rightTopCorner;

    // Start is called before the first frame update
    void Start()
    {
        distance = player.position.z - mainCamera.transform.position.z;
        leftBottomCorner = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, distance));
        rightTopCorner = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, distance));
        StartCoroutine(SpawnCoroutine());
    }


    Vector3 LocationGenerator()
    {
        Vector3 location = Vector3.zero;
        int side = Random.Range(0, 4); //0 is left 1 is top 2 is right 3 is bottom
        if (side == 0)
        {
            location = new Vector3(leftBottomCorner.x, Random.Range(leftBottomCorner.y,rightTopCorner.y) , player.position.z);
        }
        else if (side == 1)
        {
            location = new Vector3(Random.Range(leftBottomCorner.x, rightTopCorner.x), rightTopCorner.y, player.position.z);
        }
        else if(side == 2)
        {
            location = new Vector3(rightTopCorner.x, Random.Range(leftBottomCorner.y, rightTopCorner.y) , player.position.z);
        }
        else if(side == 3)
        {
            location = new Vector3(Random.Range(leftBottomCorner.x, rightTopCorner.x),leftBottomCorner.y ,player.position.z );
        }

        return location;
    }

    void Spawn(int size=10)
    {
        GameObject spawned = Instantiate(projectile, LocationGenerator(), Quaternion.identity);
        spawned.GetComponent<Edibles>().size = size;
    }
    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnCooldown);
            Spawn(Random.Range(player.gameObject.GetComponent<PlayerSizeControl>().size/2, player.gameObject.GetComponent<PlayerSizeControl>().size));
        }
    }
}
