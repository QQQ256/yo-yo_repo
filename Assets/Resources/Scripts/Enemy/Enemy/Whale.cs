using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whale : Enemy,IDamageable
{
    public float scale;

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
            AudioManager.PlayAudio(AudioName.Dead);
            health = 0;
        }
        animator.SetTrigger("hit");
    }

    public void Swallow()//animation event
    {
        targetPoint.GetComponent<Bomb>().TurnOff();
        targetPoint.gameObject.SetActive(false);
        // transform.localScale *= scale;
    }
    
}
