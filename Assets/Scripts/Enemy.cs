using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GameBehaviour
{
    public static event Action<GameObject> OnEnemyHit = null;
    public static event Action<GameObject> OnEnemyDie = null;


    public EnemyType myType;
    public float moveDistance = 100f;
    public float speed = 1f;

    public string myName;
    
    public int myHealth;
    private int baseHealth = 100;
    int maxHealth;
    float baseSpeed = 1;
    public int myScore;
    public float myAttackRange = 2;
    public int myDamage = 20;

    [Header("AI")]
    public PatrolType myPatrol;
    //needed for all patrol
    public Transform moveToPos;
    EnemyHealthBar healthBar;
    public Animator anim;
    
 
    //need for loop patrol
    Transform startPos;
    Transform endPos;
    bool reverse;

    //need for linear movement
    int patrolPoint = 0;

    



    void Start()
    {
        anim = GetComponent<Animator>();
        switch (myType)
        {
            case EnemyType.Archer1:
                 myHealth = maxHealth = baseHealth / 2;
                speed = baseSpeed * 2;
                myPatrol = PatrolType.Loop;
                myScore = 5;

                break;

            case EnemyType.TwoHand:
                myHealth = maxHealth = baseHealth * 2;
                speed = baseSpeed / 2;
                myPatrol = PatrolType.Random;
                myScore = 10;
                myDamage = 40;
                break;

            case EnemyType.OneHand:
                myHealth = baseHealth = maxHealth;
                speed = baseSpeed;
                myPatrol = PatrolType.Linear;
                myScore = 3;
                myDamage = 20;
                break;
        }

        //SetName(_EM.GetEnemyName());
        

        healthBar = GetComponentInChildren<EnemyHealthBar>();

        SetUpAI();
        if(GetComponentInChildren<EnemyWeapon>() != null)
        GetComponentInChildren<EnemyWeapon>().damage = myDamage;

        
    }

   
    public void Hit(int _damage)
    {
        myHealth -= _damage;
        //healthBar.UpdateHealthBar(myHealth, maxHealth);
        //ScaleObject(this.gameObject, transform.localScale * 1.1f);


        _GM.AddScore(myScore);
        if (myHealth <= 0)
        {
            Die();
        }
        else
        {
            int rnd = UnityEngine.Random.Range(1, 4);

            PlayAnimation("Hit");
           OnEnemyHit?.Invoke(this.gameObject);
        }
       
            
        
    }

    public void Die()
    {
        //_GM.AddScore(myPoints);
        //_EM.KillEnemy(this.gameObject);
        //Destroy(this.gameObject);

        GetComponent<Collider>().enabled = false;
        PlayAnimation("Die");
        OnEnemyDie?.Invoke(this.gameObject);

        StopAllCoroutines();
        
        
    }
    void SetUpAI()
    {
        
        startPos = Instantiate( new GameObject(),transform.position,transform.rotation).transform;
        endPos = _EM.GetRandomSpawnpoint();
        moveToPos = endPos;
        StartCoroutine(Move());
    }
      
    void PlayAnimation(string _animationName)
    {
        int rnd = UnityEngine.Random.Range(1, 4);
        anim.SetTrigger(_animationName + rnd);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))

            StopAllCoroutines();

        if (Input.GetKeyDown(KeyCode.H))
            Hit(30);
    }

    //public void SetName(string _name)
    //{
    //    myName = _name;
    //    healthBar.SetName(_name);
    //}



    IEnumerator Move()
    {
        switch(myPatrol)
        {
            case PatrolType.Linear:
                moveToPos = _EM.spawnPoints[patrolPoint];
                patrolPoint = patrolPoint != _EM.spawnPoints.Length ? patrolPoint + 1 : 0;
                break;
            
            case PatrolType.Random:
                moveToPos = _EM.GetRandomSpawnpoint();
                break;
            
            case PatrolType.Loop:
                moveToPos = reverse ? startPos : endPos;
                reverse = !reverse;
                break;
        }

        transform.LookAt(moveToPos);
        while(Vector3.Distance(transform.position, moveToPos.position) > 0.3f)
        {
            if(Vector3.Distance(transform .position, _PLAYER.transform.position) > myAttackRange)
            {
                StopAllCoroutines();
                StartCoroutine(Attack());
                yield break;
            }
            transform.position = Vector3.MoveTowards(transform.position,moveToPos.position, Time.deltaTime * speed);
            yield return null;
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(Move());

       
    }

    IEnumerator Attack()
    {
        PlayAnimation("Attack");
        yield return new WaitForSeconds(1);
        StartCoroutine(Move());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Projectile"))
        {
            Hit(collision.gameObject.GetComponent<Projectile>().damage);
        }
    }

    /*IEnumerator Move()
    {
        for(int i = 0; i < moveDistance; i++)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
                yield return null;

        }
        transform.Rotate(Vector3.up * 100);
        yield return new WaitForSeconds(Random.Range(1, 3));
        StartCoroutine(Move());
    */
}
   