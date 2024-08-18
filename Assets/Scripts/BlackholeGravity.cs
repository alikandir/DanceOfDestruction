using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeGravity : MonoBehaviour
{
    public float gravityStrength = 10f; // The strength of the gravitational pull
    public float gravityRadius = 5f;    // The radius within which objects are affected by the gravity

    void FixedUpdate()
    {
        // Find all colliders within the gravity radius
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, gravityRadius);

        foreach (Collider col in objectsInRange)
        {
            Rigidbody rb = col.attachedRigidbody;
            if (rb != null && rb.gameObject != this.gameObject)
            {
                // Calculate direction from the object to the black hole
                Vector3 directionToBlackHole = (transform.position - rb.position).normalized;

                // Apply gravitational force to the object
                rb.AddForce(directionToBlackHole * gravityStrength * Time.fixedDeltaTime, ForceMode.Acceleration);
            }
        }
        
    }
    void OnDrawGizmosSelected()
    {
        // Visualize the gravity radius in the editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, gravityRadius);
    }
}
