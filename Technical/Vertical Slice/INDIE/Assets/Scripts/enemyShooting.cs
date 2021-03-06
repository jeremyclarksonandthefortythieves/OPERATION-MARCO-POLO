﻿using UnityEngine;
using System.Collections;

public class enemyShooting : MonoBehaviour {

    public GameObject bullet;
    public bool canShoot = false;
    public int ammo = 12;
    public float reloadSpeed = 3.0f;

    private SphereCollider col;
    private Transform player;
    private bool shooting;
    private enemySight sight;
    private float rateofFire = 1.5f;
   


    void Awake()
    {
        col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sight = GetComponent<enemySight>();
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

        // If AI's raycast hit player, rate of fire is met and there is ammo left in clip, call shooting function
        if (sight.allowFire && canShoot && ammo > 0)
            ShootAI();
        
        if(ammo <= 0)
        {
            reloadSpeed -= Time.deltaTime;

            if(reloadSpeed <= 0)
            {
                ammo = 12;
                reloadSpeed = 3.0f;
            }
        }
        
    }

    void ShootAI()
    { 
        
        float fractionalDistance = (col.radius - Vector3.Distance(transform.position, player.position)) / col.radius;

		GameObject _bullet = Instantiate(bullet, transform.position + transform.up + transform.forward, transform.rotation) as GameObject;
            _bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 750f);
            ammo--;
            canShoot = false;
            rateofFire = 1.5f;
		GetComponent<AudioSource>().Play();
    }

}