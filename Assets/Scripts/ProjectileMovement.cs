using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float lifeTime=2;
    public float Velocity;
    // Start is called before the first frame update
    void Start()
    {
        Transform player = GameObject.Find("Player").GetComponent<Transform>();
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = (player.position - this.transform.position).normalized * Velocity;
        StartCoroutine(LifeTimeCounter());
    }
    public IEnumerator LifeTimeCounter()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

}
