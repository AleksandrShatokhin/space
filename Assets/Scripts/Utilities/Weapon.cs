using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weapons: int
{
    Laser,
    Rocket,
    Unknown
}

public class Weapon 
{
    private GameObject projectile;
    public Weapons id;
    int leftBullets = 0;
    int maxBullets = 0;
    bool addBulletsByTime;

    AudioClip shootSound;

    public Weapon(Weapons id, GameObject projectile, int bullets, int maxBullets, AudioClip shootSound, bool addBullets = false)
    {
        this.id = id;
        this.projectile = projectile;
        leftBullets = bullets;
        this.maxBullets = maxBullets;
        addBulletsByTime = addBullets;
        this.shootSound = shootSound;
    } 

    public void AddBullets(int newBullets)
    {
        leftBullets += newBullets;

        if (maxBullets < leftBullets)
        {
            leftBullets = maxBullets;
        }

        if (leftBullets < 0)
        {
            leftBullets = 0;
        }   
    }

    public GameObject GetProjectile()
    {
        return projectile;
    }

    public int GetBullets() => leftBullets;

    public bool GettingBulletsByTime() => addBulletsByTime;


    public AudioClip GetShootingSound() => shootSound;

}
