using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCherry : MonoBehaviour
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
            if (PlayerController.Instance.health == 3)
            {
                _animator.Play("CherryCollected");
                return;
            }
            _animator.Play("CherryCollected");
            PlayerController.Instance.health += 1;
            UIManager.instance.UpdateHealth(PlayerController.Instance.health);
        }
    }

    public void DestroyCherry()//animation event
    {
        Destroy(gameObject);
    }
}
