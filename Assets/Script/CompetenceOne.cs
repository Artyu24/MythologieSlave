using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CompetenceOne : MonoBehaviour
{
    


    /*void FixedUpdate()
    {
        //SHOOT
        if (!isActive)
            delay += Time.fixedDeltaTime;

        if (isShooting && delay >= 1f)
        {
            delay = 0;
            isActive = true;
            AxeAttack axeObject = Instantiate(axe, spawnAxePoint.position, Quaternion.identity);
            axeObject.Speed = bulletSpeed;
            axeObject.Damage = bulletDamage;
            axeObject.NbrEnemyStrikeMax = nbrEnemyStrike;
        }
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
            isShooting = true;
        else if (context.canceled)
            isShooting = false;
    }
    */
}
