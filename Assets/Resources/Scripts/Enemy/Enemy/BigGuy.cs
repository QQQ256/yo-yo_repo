using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigGuy : Enemy,IDamageable
{
    public Transform pickPoint;

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

    public void pick_Up_Bomb()//animation event
    {
        if (targetPoint.CompareTag("Bomb") && !hasBomb)
        {
            targetPoint.gameObject.transform.position = pickPoint.position;
            //改变子父的位置，为了让炸弹跟随蓝胖子移动
            targetPoint.SetParent(pickPoint);
            targetPoint.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            
            hasBomb = true;
        }
    }

    public void throw_bomb()
    {
        if (hasBomb)
        {
            targetPoint.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            targetPoint.SetParent(transform.parent.parent);
            if (FindObjectOfType<PlayerController>().gameObject.transform.position.x - targetPoint.transform.position.x < 0)
            {
                targetPoint.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1,1)*5,ForceMode2D.Impulse);
            }
            else
            {
                targetPoint.GetComponent<Rigidbody2D>().AddForce(new Vector2(1,1)*5,ForceMode2D.Impulse);
            }
        }

        hasBomb = false;
    }
}
