﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShooterTowerController : TowerController
{
    [Header("Single Shooter Tower Stats")]
    public int pooledObjects = 4;
    public int singleShooterBaseAttack = 3;
    public int singleShooterMidAttack = 5;
    public int singleShooterHighAttack = 7;
    public float singleShooterBaseRange = 2f;
    public float singleShooterMidRange = 3f;
    public float singleShooterHighRange = 4f;


    public override void Start()
    {
        GetComponent<BoxCollider2D>().size = new Vector2((range*2), (range*2));
        count = 0f;
        currentAttack = singleShooterBaseAttack;
        currentRange = singleShooterBaseRange; 
    }

    //Upgrade Tower attack speed, range, rotation speed, and attack damage
    public void upgradeSingleShooterTower(){
        if (level == towerLevel.start){
            currentAttack = singleShooterMidAttack;
            currentRange = singleShooterMidRange;
            GetComponent<BoxCollider2D>().size = new Vector2((range*2), (range*2));
        }
        if (level == towerLevel.mid){
            currentAttack = singleShooterHighAttack;
            currentRange = singleShooterHighRange;
            GetComponent<BoxCollider2D>().size = new Vector2((range*2), (range*2));
        }
        if (level == towerLevel.high){
            return;
        }
        rotationSpeed += 0.5f;
        timeBeforeNextShot += .25f;
    }

    public override void Shoot(){
        if(target == null){
            return;
        }
       GameObject ProjectileGO = ObjectPool.SharedInstance.GetPooledObject(pooledObjects);
        ProjectileController Projectile = ProjectileGO.GetComponent<ProjectileController>();
        Projectile.MissleReceiveStats(currentAttack);
        if (Projectile != null)
        {
            ProjectileGO.transform.position = transform.position;
            ProjectileGO.transform.rotation = transform.rotation;
            ProjectileGO.SetActive(true);
            Projectile.ReceiveTarget(target, damage); //Pass target to ProjectileController and damage amount
        }  
   }
    
}
