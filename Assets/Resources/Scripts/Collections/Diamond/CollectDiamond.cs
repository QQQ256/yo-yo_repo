using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectDiamond : MonoBehaviour
{
    private Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //收集之后金币+1
            AudioManager.PlayAudio(AudioName.CollectDiamond);
            _animator.Play("DiamondCollected");
        }
    }
    
    public void DestroyDiamond()//animation event
    {
        Controller.AddDiamondNumber();
        Destroy(gameObject);
    }

}
