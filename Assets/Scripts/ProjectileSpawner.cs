using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Transform player;
    [SerializeField] GameObject projectile;
    [SerializeField] float spawnCooldown = 3f;
    float playerSize;
    float distance;
    Vector3 leftBottomCorner;
    Vector3 rightTopCorner;

    // Start is called before the first frame update
    void Start()
    {
        player= GameObject.Find("Player").GetComponent<Transform>();
        distance = player.position.z - mainCamera.transform.position.z;
        leftBottomCorner = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, distance));
        rightTopCorner = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, distance));
        StartCoroutine(SpawnCoroutine());
    }
    private void Update()
    {

        leftBottomCorner = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, distance));
        rightTopCorner = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, distance));
        

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

    void Spawn(float size=10)
    {
        GameObject spawned = Instantiate(projectile, LocationGenerator(), Quaternion.identity);
        spawned.GetComponent<Edibles>().size = size;
        if (Random.Range(0f, 1f) <= 0.8f)
        {
            spawned.GetComponent<Edibles>().isMakingBig=true;
        }
        else 
        {
            spawned.GetComponent<Edibles>().isMakingBig = false;
        }
        
    }
    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnCooldown);

            playerSize = player.gameObject.GetComponent<PlayerSizeControl>().size;
            float spawnSize= Random.Range(playerSize / 2, playerSize);
            Spawn(spawnSize);
            
              
        }
    }
}
