using UnityEngine;
using System.Collections;

public class enemyShooting : MonoBehaviour {

    public GameObject bullet;
    public bool canShoot = false;

    private SphereCollider col;
    private Transform player;
    private bool shooting;
    private enemySight sight;
    private float reloadSpeed = 2.0f;
    private float reloadTimer;
    private float rateofFire = 1.5f;
    private int ammo = 12;


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
            canShoot = true;
           
        }
        else
            canShoot = false;


        if (sight.allowFire && canShoot)
            ShootAI();
        
        if(ammo <= 0)
        {
            reloadSpeed -= Time.deltaTime;

            if(reloadSpeed <= 0)
            {
                ammo = 12;
            }
        }
        
    }

    public void ShootAI()
    { 
        
        float fractionalDistance = (col.radius - Vector3.Distance(transform.position, player.position)) / col.radius;
            
        if(ammo > 0)
        {
            GameObject _bullet = Instantiate(bullet, transform.position + transform.forward, transform.rotation) as GameObject;
            _bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 750f);
            ammo--;
            canShoot = false;
            rateofFire = 1.5f;
        }
            

            
            
    }


    void ShotEffects()
    {
        
    }
}