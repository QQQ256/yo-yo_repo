using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Captain : Enemy,IDamageable
{
    // public bool isSkilled;
    private SpriteRenderer spite;
    public void GetHit(float damage)
    {
        health -= damage;
        if (health < 1)
        {
            health = 0;
            isDead = true;
        }
        animator.SetTrigger("hit");
    }

    public override void Init()//use the flip of the spite to rotate the enemy
    {
        base.Init();
        spite = GetComponent<SpriteRenderer>();
        // isSkilled = false;
    }

    public override void Update()
    {
        base.Update();
        if (animState == 0)// control the animState to flip the captain, if you do not do this, the captain will flip eternally.
        {
            spite.flipX = false;//TODO
        }
    }

    public override void SkillAttack()
    {
        base.SkillAttack();
        if (animator.GetCurrentAnimatorStateInfo(1).IsName("skill"))//按照动画片段的时间长度来描述 逃跑要跑多久
        {
            spite.flipX = true;
            var position = transform.position;
            if (transform.position.x > targetPoint.position.x)//玩家在炸弹左边
            {
                transform.position = Vector2.MoveTowards(transform.position, position + Vector3.right, speed * 3 * Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, position + Vector3.left, speed * 3 * Time.deltaTime);
            }
        }
        else
        {
            spite.flipX = false;
        }
    }
}
