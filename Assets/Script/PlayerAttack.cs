using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private bool isShooting;
    private float delay;
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform spawnBulletPoint;

    private float bulletSpeed = 5;
    private int bulletDamage = 50;

    void FixedUpdate()
    {
        //SHOOT
        delay += Time.fixedDeltaTime;

        if (isShooting && delay >= 0.25f)
        {
            delay = 0;
            Bullet actualBullet = Instantiate(bullet, spawnBulletPoint.position, Quaternion.identity);
            actualBullet.GetSpeed = bulletSpeed;
            actualBullet.GetDamage = bulletDamage;
        }
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
            isShooting = true;
        else if (context.canceled)
            isShooting = false;
    }
}
