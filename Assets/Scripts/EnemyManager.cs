using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.VFX;
public enum EnemyType
{
    Archer1,
    OneHand,
    TwoHand

}
public enum PatrolType
{
    Linear, Random, Loop
}

public class EnemyManager : Singleton<EnemyManager>
{
    public Transform[] spawnPoints;
    public string[] enemyNames;
    public GameObject[] enemyTypes;

    public List<GameObject> enemies;

    public string killCondition = "Two";

    private void Start()
    {
        //SpawnAtRandom();
        //SpawnEnemises();
        StartCoroutine(SpawnDelay());
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            SpawnAtRandom();
            
            GetEnemyCount();
        }

        if (Input.GetKeyUp(KeyCode.K))
        {

            KillAllEnemies();
            //KillEnemy(enemies[0]);
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            KillSpecificEnemy(killCondition);

        }
    }



    /// <summary>
    /// kills a specific enemy
    /// </summary>
    /// <param name="enemy_">the enemy we want to kill</param>
    public void KillEnemy(GameObject enemy_)
    {
        if (enemies.Count == 0)
            return;

        

        //Destroy(enemy_);
        enemies.Remove(enemy_);
        GetEnemyCount();
    }

    /// <summary>
    /// kills all enemies in our scene
    /// </summary>
    void KillAllEnemies()
    {
        if (enemies.Count == 0)
            return;

        for (int i = enemies.Count -1; i >= 0; i--)
        {
            KillEnemy(enemies[0]);
        }
        GetEnemyCount();
    }

    /// <summary>
    /// kills specifi enemies
    /// </summary>
    /// <param name="_condition">the condition we want to kill</param>
    void KillSpecificEnemy(string _condition)
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].name.Contains(_condition))
                KillEnemy(enemies[i]);
        }
        GetEnemyCount();
    }

    /// <summary>
    /// shows the number of enemies
    /// </summary>
    private void GetEnemyCount()
    {
        _UI.EnemyCount(enemies.Count);
    }
    
    /// <summary>
    /// Sets a random enemy name
    /// </summary>
    /// <param name="_enemy"></param>
    void SetEnemyName(GameObject _enemy)
    {
        //_enemy.GetComponent<Enemy>().SetName(enemyNames[Random.Range(0,enemyNames.Length)]);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public string GetEnemyName()
    {
        return enemyNames[Random.Range(0, enemyNames.Length)];
    }


    /// <summary>
    /// spawns an enemy at every spawnpoint
    /// </summary>
    public void SpawnEnemises()
    {
       

        for (int i = 0; i <= spawnPoints.Length; i++)
        {
            int rnd = Random.Range(0, enemyTypes.Length - 1);
            GameObject enemy = Instantiate(enemyTypes[rnd], spawnPoints[i].position, spawnPoints[i].rotation);
            enemies.Add(enemy);
            SetEnemyName(enemy);
        }
        GetEnemyCount();

    }

    /// <summary>
    /// spawn a enemy after a randome amount of time
    /// </summary>
    /// <returns></returns>

    IEnumerator SpawnDelay()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int rnd = Random.Range(0, enemyTypes.Length);
            GameObject enemy = Instantiate(enemyTypes[rnd], spawnPoints[i].position, spawnPoints[i].rotation);
            enemies.Add(enemy);
            GetEnemyCount();
            //SetEnemyName(enemy);
            yield return new WaitForSeconds(2);
           
        }
       
       

    }

    /// <summary>
    /// spawns a random eneny at random spawnpoint
    /// </summary>
    public void SpawnAtRandom()
    {
        int rndEnemy = Random.Range(0, enemyTypes.Length - 1);
        int rndSpawnPoint = Random.Range(0, spawnPoints.Length - 1);

        GameObject enemy = Instantiate(enemyTypes[rndEnemy], spawnPoints[rndSpawnPoint].position, spawnPoints[rndSpawnPoint].rotation);
        //add enemy to enemies list
        enemies.Add(enemy);
        GetEnemyCount();
        SetEnemyName(enemy);
    }

    public Transform GetRandomSpawnpoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }

    /// <summary>
    /// event system
    /// </summary>
    private void OnEnable()
    {
        Enemy.OnEnemyDie += KillEnemy;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDie -= KillEnemy;
    }

    void Examples()
    {
        //print(enemyNames.length)
        print(enemyNames[enemyNames.Length - 1]);


        //spawn in the first skeleton in the first spawnpoint
        GameObject first = Instantiate(enemyTypes[0], spawnPoints[0].position, spawnPoints[0].rotation);

        first.name = enemyNames[0];

        //spawn in in that last skeleton in the last postion
        GameObject last = Instantiate(enemyTypes[enemyTypes.Length - 1], spawnPoints[enemyTypes.Length - 1]);
        last.name = enemyNames[enemyNames.Length - 1];


        //how a loop is written
        for (int i = 0; i <= 1000 ; i++)
        {
            print(i);
        }

        //create a loop within a loop for a wall
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Instantiate(wall, new Vector3(i, j, 0), transform.rotation);
            }

        }
    }   
   
}
