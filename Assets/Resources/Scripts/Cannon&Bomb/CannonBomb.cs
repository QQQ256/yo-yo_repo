using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBomb : MonoBehaviour
{
    public float startTime;
    public float waitTime;
    public LayerMask groundCheck;
    private Collider2D _collider2D;
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _collider2D = GetComponent<Collider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            if (Time.time > startTime + waitTime)
            {
                _animator.Play("Explosion");
            }
        }
    }
    
    public void Explosion()
    {
        AudioManager.PlayAudio(AudioName.Explosion);
        _collider2D.enabled = false;

        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, 3f, groundCheck);
        
        _rigidbody2D.gravityScale = 0;
        
        foreach (var items in collider2Ds)
        {
            Vector3 pos = transform.position - items.transform.position;
            // //进过测试爆炸之后只有左右两边的力，然而真实情况是还需要一个往上的力，👇
            //修改版本
            items.GetComponent<Rigidbody2D>().AddForce((-pos + Vector3.up) * 5,ForceMode2D.Impulse);
            
            if (items.CompareTag("Player"))
            {
                items.GetComponent<IDamageable>().GetHit(3);
            }
            
            if (items.CompareTag("Enemy"))
            {
                items.GetComponent<IDamageable>().GetHit(3);
            }
        }
    }

    public void DestroyBomb()
    {
        Destroy(gameObject);
    }
}
