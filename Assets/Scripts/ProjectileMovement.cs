using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] float Velocity;
    [SerializeField] Transform player;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = (player.position - this.transform.position).normalized * Velocity;
    }

}
