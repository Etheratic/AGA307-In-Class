using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyType myType;
    public float moveDistance = 100f;
    public float speed = 1f;
    
    private int enemyHealth;
    public int baseHealth;
    float baseSpeed = 1;

    [Header("AI")]
    public PatrolType myPatrol;
    //needed for all patrol
    public Transform moveToPos;

    public EnemyManager _EM;
 
    //need for loop patrol
    Transform startPos;
    Transform endPos;
    bool reverse;

    //need for linear movement
    int patrolPoint = 0;



    void Start()
    {
        _EM = FindObjectOfType<EnemyManager>();


        switch (myType)
        {
            case EnemyType.Archer1:
                 enemyHealth = baseHealth / 2;
                speed = baseSpeed * 2;
                myPatrol = PatrolType.Loop;
                break;

            case EnemyType.TwoHand:
                enemyHealth = baseHealth * 2;
                speed = baseSpeed / 2;
                myPatrol = PatrolType.Random;
                break;

            case EnemyType.OneHand:
                enemyHealth = baseHealth;
                speed = baseSpeed;
                myPatrol = PatrolType.Linear;
                break;
        }

        SetUpAI();
    }

    void SetUpAI()
    {
        
        startPos = Instantiate( new GameObject(),transform.position,transform.rotation).transform;
        endPos = _EM.GetRandomSpawnpoint();
        moveToPos = endPos;
        StartCoroutine(Move());
    }
      

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            StopAllCoroutines();
    }

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
            transform.position = Vector3.MoveTowards(transform.position,moveToPos.position, Time.deltaTime * speed);
            yield return null;
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(Move());
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
   