﻿using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [Header("Projectile Stats")]
    public float projectileSpeed = 20f;
    private int attack;
    protected Transform projectileTarget;

    Rigidbody2D rb2d;

    void Start()
    {
        
    }

    //Allows Turret Controller to find nearest enemy and pass that enemy as the target here that the bullet should lock onto
    public void ReceiveTarget(Transform turretTarget, int d)
    {
        projectileTarget = turretTarget;
        attack = d;
    }


    void Update()
    {
        //If not enemy to shoot, destroy bullet, do not calculate distance or direction
        if (projectileTarget == null)
        {
            gameObject.SetActive(false);
            return;
        }

        //Calculate distance(based on speed) and direction for projectile to go
        Vector3 path = projectileTarget.position - transform.position;
        float frameDistance = projectileSpeed * Time.deltaTime;

        //Check if projectile has reached the target enemy
        if (path.magnitude <= frameDistance)
        {
            HitTarget();
            return;
        }

        transform.Translate(path.normalized * frameDistance, Space.World); //Move Projectile Towards Target 
    }

    //Decremet enemy health based on turret damage, destroy enemy when health is 0.
    public virtual void HitTarget(){}
}
