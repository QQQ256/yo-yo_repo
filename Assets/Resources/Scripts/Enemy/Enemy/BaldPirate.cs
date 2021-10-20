using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaldPirate : Enemy,IDamageable
{
    public void GetHit(float damage)
    {
        health -= damage;
        if (health < 1)
        {
            health = 0;
            AudioManager.PlayAudio(AudioName.Dead);
            isDead = true;
        }
        animator.SetTrigger("hit");
    }
}
