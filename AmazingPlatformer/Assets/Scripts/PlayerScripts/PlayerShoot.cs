﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject fireBullet;

    // Update is called once per frame
    void Update()
    {
        ShootBullet();
    }

    void ShootBullet()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            GameObject bullet = Instantiate(fireBullet, transform.position, Quaternion.identity);
            
            //to make sure that the bullet shot is project from the direction player faces
            bullet.GetComponent<FireBullet>().Speed *= transform.localScale.x;
        }
    }
}
