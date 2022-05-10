using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTrapController : MonoBehaviour
{
    public float shootingRate;
    public float shootingSpeed;
    public float bulletSpeed;
    public float bulletDamage;

    public Rigidbody bullet;

    public Transform shooter;

    private float nextShootTime;

    private void OnEnable()
    {
        nextShootTime = Time.time + shootingRate;
    }

    private void Update()
    {
        if (Time.time >= nextShootTime)
        {
            ShootBullet();
            nextShootTime += shootingRate;
        }
    }

    private void ShootBullet()
    {
        var shoot = Instantiate(bullet, shooter.transform.position, transform.rotation);
        shoot.SendMessage("SetAttackDamage", bulletDamage);

        shoot.AddForce(transform.forward * bulletSpeed);

        Destroy(shoot.gameObject, shootingRate + shootingSpeed);
    }

}
