using UnityEngine;

public class PatrolState : EnemyState
{
    public override void EnterState(Enemy enemy)
    {
        enemy.animState = 0;//开始游走为idle模式 0 -> patrolState
        if(enemy.isStatic)//禁止行动模式 当玩家进入视野的时候会跟随玩家移动
            return;
        enemy.SwitchPoint();
        // Debug.Log(enemy+"enter into patrol state!");
    }

    public override void onUpdate(Enemy enemy)
    {
        //播放完idle之后再去播放跑步动作

        if (enemy.transforms.Count > 0)//攻击检测到了碰撞体！切换状态
        { 
            enemy.TransitionToState(enemy.AttackState);
        }
        
        if(enemy.isStatic)
            return;

        if (!enemy.animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            enemy.animState = 1;// 1 -> runState
            enemy.MoveToTarget();
        }

        if (Mathf.Abs(enemy.targetPoint.position.x - enemy.transform.position.x) < 0.01f)
        {
            // enemy.SwitchPoint();
            enemy.TransitionToState(enemy.PatrolState);
        }
    }
}
