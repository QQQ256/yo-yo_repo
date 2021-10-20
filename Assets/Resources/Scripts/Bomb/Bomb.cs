using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bomb : MonoBehaviour
{
    private Animator _animator;
    private Collider2D _collider2D;
    private Rigidbody2D _rigidbody2D;
    public float startTime;
    public float waitTime;
    public float bombForce;
    
    [Header("Status Check")]
    public float radius;
    public LayerMask groundCheck;

    [Header("bomb")] public bool isBomb;
    
    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _collider2D = GetComponent<Collider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        //解决爆炸问题
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("bomb_off"))
        {
            if (Time.time > startTime + waitTime)
            {
                _animator.Play("bomb_explosion");
                
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,radius);
    }

    public virtual void Explosion()//animation event 爆炸动画的第一帧执行
    {

        //bomb的检测中会将自己检测进去，爆炸的时候会有一个向上的力，因为自己会去和周围的环境碰撞
        //解决方法就是在检测之前将自身的碰撞体关闭，增加一个rigid2d的，mass为0的一个组件
        _collider2D.enabled = false; 
        
        //获得检测范围内所有的有碰撞体的东西，然后判断其方向是在爆炸的左边还是右边等
        //判断方向的方法就是通过相减的方式可以得出那个碰撞体对应爆炸点的一个坐标，比如说xyz中哪个是负的就在哪里
        //实现之后就要给其添加力，但是需要注意的是添加的力一定是反方向来添加的，为了物体炸出去，所以加的力需要加负号
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, radius, groundCheck);
        
        _rigidbody2D.gravityScale = 0;
        
        foreach (var items in collider2Ds)
        {
            Vector3 pos = transform.position - items.transform.position;
            // //进过测试爆炸之后只有左右两边的力，然而真实情况是还需要一个往上的力，👇
            //修改版本
            items.GetComponent<Rigidbody2D>().AddForce((-pos + Vector3.up) * bombForce,ForceMode2D.Impulse);

            //熄灭的炸弹会被重新点燃 因此需要判断是否是熄灭的炸弹 如果是燃烧的炸弹则会发生错误
            if (items.CompareTag("Bomb") && items.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("bomb_off"))
            {
                isBomb = true;
                TurnOn();
            }
        }
    }

    public void DestroyBomb() //animation event
    {
        
        Destroy(gameObject);
    }

    public void BombSound()
    {
        AudioManager.PlayAudio(AudioName.Explosion);
    }
    
    public void TurnOff()
    {
        _animator.Play("bomb_off");
        gameObject.layer = LayerMask.NameToLayer("NPC");
        isBomb = false;
        //熄灭之后的炸弹不能再作为player的目标
        //因此需要将其的图层改成不和其发生关系的图层 这样就不会再去检测了
    }

    private void TurnOn()
    {
        startTime = Time.time;//由于计时器开炸弹放下时刻就会启动，因此如果不重新为其计算时间，炸弹则会瞬间爆炸 这不是想要的效果
        _animator.Play("bomb_on");
        gameObject.layer = LayerMask.NameToLayer("Bomb");
    }
    
}
