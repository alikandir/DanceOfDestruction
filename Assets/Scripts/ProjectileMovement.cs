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
        Quaternion rotation = Quaternion.LookRotation(rb.velocity);
        //transform.rotation = new Quaternion(rotation.x-90,rotation.y,rotation.z,rotation.w);
        transform.rotation = rotation;
        transform.eulerAngles=new Vector3 (transform.eulerAngles.x-90,transform.eulerAngles.y,transform.eulerAngles.z);


    }
    public IEnumerator LifeTimeCounter()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

}
