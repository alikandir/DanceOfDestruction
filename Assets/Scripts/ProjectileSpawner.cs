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
    public GameObject whitePlanet;
    [SerializeField] float spawnCooldown = 3f;
    public float whiteHoleSpawnCooldown = 5f;
    public float projectileSpeed = 5f;
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
        StartCoroutine(WhiteHoleSpawnCoroutine());
    }
    private void Update()
    {
        distance = player.position.z - mainCamera.transform.position.z;
        leftBottomCorner = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, distance));
        rightTopCorner = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, distance));
        

    }

    Vector3 LocationGenerator()
    {
        Vector3 location = Vector3.zero;
        int side = Random.Range(0, 4); //0 is left 1 is top 2 is right 3 is bottom
        if (side == 0)
        {
            location = new Vector3(leftBottomCorner.x, Random.Range(leftBottomCorner.y, rightTopCorner.y), player.position.z);
        }
        else if (side == 1)
        {
            location = new Vector3(Random.Range(leftBottomCorner.x, rightTopCorner.x), rightTopCorner.y, player.position.z);
        }
        else if (side == 2)
        {
            location = new Vector3(rightTopCorner.x, Random.Range(leftBottomCorner.y, rightTopCorner.y), player.position.z);
        }
        else if (side == 3)
        {
            location = new Vector3(Random.Range(leftBottomCorner.x, rightTopCorner.x), leftBottomCorner.y, player.position.z);
        }
        return location;
    }
    Vector3 WhiteHoleLocationGenerator()
    {
        Vector3 location = Vector3.zero;

        location = new Vector3(Random.Range(-150,320), Random.Range(-150, 130), 0);

        return location;
    }

    void Spawn(float size=10f, float velocity=10f)
    {
        GameObject spawned = Instantiate(projectile, LocationGenerator(), Quaternion.identity);
        spawned.GetComponent<Edibles>().size = size;
        spawned.GetComponent<ProjectileMovement>().Velocity = velocity;        
    }
    void SpawnWhiteHole(float size = 10f)
    {
        GameObject spawned = Instantiate(whitePlanet, WhiteHoleLocationGenerator(), Quaternion.identity);
        spawned.GetComponent<Edibles>().size = size;
        
    }
    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnCooldown);

            playerSize = player.gameObject.GetComponent<PlayerSizeControl>().size;
            float spawnSize = SizeGenerator();
            Spawn(spawnSize,projectileSpeed);
            
              
        }
    }
    IEnumerator WhiteHoleSpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(whiteHoleSpawnCooldown);

            playerSize = player.gameObject.GetComponent<PlayerSizeControl>().size;
            float spawnSize = SizeGenerator();
            SpawnWhiteHole(spawnSize);
            
        }
    }

    float SizeGenerator()
    {
        int rand = Random.Range(0, 100);
        if (rand >= 95)
            return Random.Range(playerSize / 6, playerSize / 4);
        else if (rand >= 75)
            return Random.Range(playerSize / 8, playerSize / 6);
        else
            return Random.Range(playerSize / 20, playerSize / 8);
    }
}
