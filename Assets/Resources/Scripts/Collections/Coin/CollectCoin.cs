using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
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
            AudioManager.PlayAudio(AudioName.CollectCoin);
            // Model.GetInstance().Number += 1;
            // CoinUI.instance.currentCoinNumber += 1;
            // GameManager.Instance.coins.Remove(other.GetComponent<GameObject>());
            _animator.Play("CoinCollected");
        }
    }

    public void DestroyCoin()//animation event
    {
        //收集之后金币+1
        Controller.AddCoinNumber();
        GameManager.Instance.coins.Remove(gameObject);
        Destroy(gameObject);
    }
    
}
