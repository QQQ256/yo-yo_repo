using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using static System.Random;
using Random = System.Random;

public class Cannon : MonoBehaviour
{
    public static Cannon instance;
    public Transform attackPoint;
    public GameObject cannonBomb;
    public Animator animator;
    public float kickForce;
    public float distinction;
    public bool isPlayer;
    private GameObject sign;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        sign = transform.GetChild(0).gameObject;
        animator = GetComponent<Animator>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sign.SetActive(true);
            isPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sign.SetActive(false);
            isPlayer = false;
        }
    }

    public void Attack()//animation event
    {
        var bomb = Instantiate(cannonBomb,attackPoint.transform.position,attackPoint.transform.rotation);
        // float randomForce = Random.r
        bomb.GetComponent<Rigidbody2D>().AddForce(new Vector2(distinction,1)*kickForce,ForceMode2D.Impulse);
        // cannonBomb.GetComponent<Rigidbody2D>().AddForce(Vector3.up * kickForce,ForceMode2D.Impulse);
    }

    public void KickCannonBomb()//animation event
    {
        // Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(explosionPoint.transform.position, 3f,bombLayer);
        // foreach (var items in collider2Ds)
        // {
        //     Vector3 pos = explosionPoint.transform.position - cannonBomb.transform.position;
        //     // kickForce = 
        //     items.GetComponent<Rigidbody2D>().AddForce((pos + Vector3.up) * kickForce,ForceMode2D.Impulse);
        // }
        
    }
   
}
