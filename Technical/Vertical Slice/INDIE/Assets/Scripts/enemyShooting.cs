using UnityEngine;
using System.Collections;

public class enemyShooting : MonoBehaviour {

    public GameObject bullet;

    private SphereCollider col;
    private Transform player;
    private bool shooting;
    private enemySight sight;
    private bool allowFire = false;
    private float rateofFire = 1.5f;


    void Awake()
    {
        col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sight = GetComponent<enemySight>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        rateofFire -= Time.deltaTime;
        if(rateofFire <= 0)
        {
            allowFire = true;
           
        }
        if (sight.canShoot && allowFire)
            ShootAI();
           
        
    }

    public void ShootAI()
    {
        shooting = true;
        
        float fractionalDistance = (col.radius - Vector3.Distance(transform.position, player.position)) / col.radius;

            GameObject _bullet = Instantiate(bullet, transform.position + transform.forward, transform.rotation) as GameObject;
            _bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 500f);
            allowFire = false;
            rateofFire = 1.5f;
            
    }


    void ShotEffects()
    {
        
    }
}