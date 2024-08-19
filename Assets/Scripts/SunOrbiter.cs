using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunOrbiter : MonoBehaviour
{
    [System.Serializable]
    public class Planet
    {
        public GameObject planetPrefab;  // The planet prefab
        public float orbitDistance;      // Distance from the sun
        public float orbitSpeed;         // Speed of orbit around the sun
        public float rotationSpeed;      // Speed of planet's own rotation
        [HideInInspector] public GameObject planetObject; // Reference to the instantiated planet
    }

    public List<Planet> planets = new List<Planet>();  // List of planets to be initialized

    void Start()
    {
        foreach (Planet planet in planets)
        {
            // Spawn the planet at the correct orbit distance
            Vector3 spawnPosition = transform.position + new Vector3(planet.orbitDistance, 0, 0);
            planet.planetObject = Instantiate(planet.planetPrefab, spawnPosition, Quaternion.identity);
            planet.planetObject.transform.parent = transform;  // Make the planet a child of the sun for easier management
        }
    }

    void Update()
    {
        foreach (Planet planet in planets)
        {
            // Check if the planet still exists
            if (planet.planetObject != null)
            {
                // Rotate the planet around the sun
                planet.planetObject.transform.RotateAround(transform.position, Vector3.forward, planet.orbitSpeed * Time.deltaTime);

                // Rotate the planet around its own axis
                planet.planetObject.transform.Rotate(Vector3.up, planet.rotationSpeed * Time.deltaTime);
            }
        }
    }
}
