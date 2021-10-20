using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    private GameObject _alarmSign;
    private Collider2D _collider2D;
    private EnemyState _currentState;
    public Animator animator;
    public int animState;
    public readonly PatrolState PatrolState = new PatrolState();
    public readonly AttackState AttackState = new AttackState();
    public Transform pointA, pointB;
    public Transform targetPoint;
    public Slider healthBar;
    public float speed;
    public bool isTryOut;

    [Header("Base Setting")] 
    public float health;
    public bool isDead;
    public bool hasBomb;
    public bool isBoss;
    public bool isIdle;
    public bool isStatic;
    [Header("Attack Check")] 
    public float attackRate;
    // public float skillAttackRate;
    public float attackRange;
    private float _nextAttack = 0;
    
    [Header("Attack List")]
    public List<Transform> transforms = new List<Transform>();
    
    [Header("Coins & Cherry")]
    public GameObject coinPrefabs;
    public GameObject cherryPrefabs;
    public LayerMask coinLayer;
    public Transform underPoint;
    public int coinNumber;
    public virtual void Init()
    {
        //这样写的好处是在子类中可以在原有的基础上继续写一些本身不需要的代码 比较之方便
        _collider2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        _alarmSign = transform.GetChild(0).gameObject;
        //开始界面不需要进入下面的方法，直接返回
        if (SceneManager.GetActiveScene().name != "StartScene") return;
        isIdle = true;

    }

    public void Awake()
    {
        Init();
    }

    public virtual void Start()
    {
        //不使用这个赋值方法 使用新的虚方法
        // animator = GetComponent<Animator>();
        //每个enemy都有idle状态 和 攻击 状态 可以都写一个这东西
        HealthBar();
        GameManager.Instance.IsEnemy(this);//加入enemy的list 敌人都挂了该脚本 因此在进入之后都会加入该list之中
        if(isIdle)
            return;
        TransitionToState(PatrolState);//when the game start, let the enemy go in patrol state
        if(isBoss)
            UIManager.instance.SetBossHealth(health);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        UpdateHealth();
        if (isDead)
        {
            //死亡判断
            animator.SetBool("dead",isDead);//被炸弹炸死需要在update中一直判断
            Destroy(gameObject,0.2f);//captain
            return;
        }
        if(isIdle)
            return;
        if (isBoss)
            UIManager.instance.ReduceHealth(health);
        if (isTryOut)
            return;
        _currentState.onUpdate(this);//永远都是当前脚本的挂载者执行 巡逻
        animator.SetInteger("state",animState);//update enemy's current state frequently
    }

    private void HealthBar()
    {
        healthBar.maxValue = health;
    }

    private void UpdateHealth()
    {
        healthBar.value = health;
    }

    //状态机的状态转变 ->首先获得要转变的状态值 然后进入该状态
    public void TransitionToState(EnemyState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }

    public void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
        FlipDirection();
    }

    public void BasicAttack()//对玩家的基本攻击
    {
        //计时器给定攻击间隔和范围判定
        if (!(Vector2.Distance(this.transform.position, targetPoint.transform.position) < attackRange)) return;
        if (!(Time.time > _nextAttack)) return;
        Debug.Log("basic attack");
        animator.SetTrigger("attack");
        _nextAttack = Time.time + attackRate;
    }
    public virtual void SkillAttack()//对炸弹的攻击
    {
        if (Vector2.Distance(transform.position,targetPoint.transform.position) < attackRange)
        {
            if (Time.time > _nextAttack)
            {
                Debug.Log("skill attack");
                animState = 2;
                animator.SetTrigger("skillAttack");
                _nextAttack = Time.time + attackRate;
            }
            // StartCoroutine()
        }
    }

    private void FlipDirection()
    {
        //on the right side of the enemy
        transform.rotation = Quaternion.Euler(0f, transform.position.x < targetPoint.position.x 
                            ? 180f : 0f, 0f);
    }

    public void SwitchPoint()
    {
        //自动计算离哪个点远 总是朝远的点走
        var position = transform.position;
        //TODO 目标会将展示牌作为目标
        targetPoint = Mathf.Abs(pointA.position.x - position.x) 
                      > Mathf.Abs(pointB.position.x - position.x) ? pointA : pointB;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //如果已经包含了该物体 那么就不添加
        if (!transforms.Contains(other.transform) && !hasBomb && !isDead && !GameManager.Instance.gameOver)
        {
            transforms.Add(other.transform);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        transforms.Remove(other.transform);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!isDead && !GameManager.Instance.gameOver)
            StartCoroutine(OnAlarm());
    }

    IEnumerator OnAlarm()
    {
        _alarmSign.SetActive(true);
        yield return new WaitForSeconds(animator.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length);
        _alarmSign.SetActive(false);
    }

    public void Explosion()//animation event
    {
        _collider2D.enabled = false; 
        
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(underPoint.transform.position, 3f,coinLayer);
        
        foreach (var items in collider2Ds)
        {
            Vector3 pos = underPoint.transform.position - items.transform.position;
            // //进过测试爆炸之后只有左右两边的力，然而真实情况是还需要一个往上的力，👇
            //修改版本
            items.GetComponent<Rigidbody2D>().AddForce((-pos + Vector3.up) * 2f,ForceMode2D.Impulse);
        }
    }
    
    private void Create()//animation event
    {
        for (int i = 0; i < coinNumber; i++)
        {
            float x = Random.Range(-1, 3);
            float y = Random.Range(1, 3);
            // Instantiate(coinPrefabs,new Vector3(transform.position.x+x,transform.position.y+y),transform.rotation);
            Instantiate(coinPrefabs,new Vector3(transform.position.x+x,transform.position.y+y),transform.rotation);
            GameManager.Instance.coins.Add(coinPrefabs);
        }

        foreach (var coin in GameManager.Instance.coins)
        {
            float randomForce = Random.Range(2, 5);
            coin.GetComponent<Rigidbody2D>().AddForce(Vector3.up * randomForce,ForceMode2D.Impulse);
        }

        var probability = Random.Range(1, 10);
        if(probability % 2 == 1)
            Instantiate(cherryPrefabs,new Vector3(transform.position.x+1,transform.position.y+1),transform.rotation);
    }

    
}
