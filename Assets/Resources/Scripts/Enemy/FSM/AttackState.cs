using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyState
{
     public override void EnterState(Enemy enemy)
        {
            enemy.animState = 2;
            enemy.targetPoint = enemy.transforms[0];
        }
    
        public override void onUpdate(Enemy enemy)
        {
            //有炸弹则不执行下面的判断
            if (enemy.hasBomb)
            {
                return;
            }

            //完善逻辑 检测没有敌人的时候则切换回巡游状态
            if (enemy.transforms.Count == 0)
            {
                enemy.TransitionToState(enemy.PatrolState);
            }
    
            if (enemy.transforms.Count > 1)
            {
                foreach (var t in enemy.transforms)
                {
                    if (Mathf.Abs(enemy.transform.position.x - t.position.x) 
                        <Mathf.Abs(enemy.transform.position.x - enemy.targetPoint.position.x))
                    {
                        enemy.targetPoint = t;
                    }
                }
            }
    
            if (enemy.transforms.Count == 1)
            {
                enemy.targetPoint = enemy.transforms[0];
            }
    
            if (enemy.targetPoint.CompareTag("Player"))
            {
                enemy.BasicAttack();
            }
    
            if (enemy.targetPoint.CompareTag("Bomb"))
            {
                enemy.SkillAttack();
            }
            
            enemy.MoveToTarget();
        }
}
