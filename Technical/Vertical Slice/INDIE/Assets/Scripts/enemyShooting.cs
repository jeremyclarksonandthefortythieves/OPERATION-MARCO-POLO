using UnityEngine;
using System.Collections;

public class enemyShooting : MonoBehaviour {

    public GameObject bullet;

    private SphereCollider col;
    private Transform player;
    private bool shooting;
    

    void Awake()
    {
        col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {
        if (!shooting)
            
            Shoot();

    }


    void Shoot()
    {
        shooting = true;

        float fractionalDistance = (col.radius - Vector3.Distance(transform.position, player.position)) / col.radius;

        GameObject _bullet = Instantiate(bullet, transform.position + transform.forward, transform.rotation) as GameObject;
        _bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 500f);

    }


    void ShotEffects()
    {
        
    }
}