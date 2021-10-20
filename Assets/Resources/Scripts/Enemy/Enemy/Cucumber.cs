using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cucumber : Enemy,IDamageable
{
    public void TurnOff()//animation event
    {
        targetPoint.GetComponent<Bomb>().TurnOff();
    }

    public void GetHit(float damage)
    {
        health -= damage;
        if (health < 1)
        {
            isDead = true;
            health = 0;
        }
        animator.SetTrigger("hit");
    }
}
