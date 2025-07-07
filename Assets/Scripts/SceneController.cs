using System.Collections;
using UnityEngine;


public class SceneController : MonoBehaviour
{
    public int zombiesAmount = 5;
    public int carnivalCreaturesAmount = 5;
    public string zombieGameObjName = "Zombie(Clone)";
    public string carnivalCreatureGameObjName = "CarnivalCreature(Clone)";

    public Vector2 minXYZombiePlace = new Vector2(-6.0f, -1.5f);
    public Vector2 maxXYZombiePlace = new Vector2(2.0f, 2.5f);

    public Vector2 minXYCarnivalCreaturePlace = new Vector2(5.25f, -2.5f);
    public Vector2 maxXYCarnivalCreaturePlace = new Vector2(8.75f, 5.5f);


    void Start()
    {
        StartCoroutine(createEnemiesProcess());
    }

    IEnumerator createEnemiesProcess()
    {
        yield return new WaitForSeconds(0.3f);

        createEnemies(zombieGameObjName, zombiesAmount);
        createEnemies(carnivalCreatureGameObjName, carnivalCreaturesAmount);
    }

    void createEnemies(string enemyName, int enemyAmount)
    {
        for (int j = 0; j < enemyAmount; j++)
        {
            createEnemy(enemyName);
        }
    }

    void createEnemy(string enemyName)
    {
        GameObject enemy = ObjectPool.SharedInstance.GetPooledObject(enemyName);

        if (enemy != null)
        {
            enemy.SetActive(true);
            putEnemyInScene(enemy);
        }
    }

    void putEnemyInScene(GameObject enemy)
    {
        Vector3 putPosition = Vector3.zero;

        if (enemy.name == zombieGameObjName)
        {
            putPosition.x = Random.Range(minXYZombiePlace.x, maxXYZombiePlace.x);
            putPosition.y = Random.Range(minXYZombiePlace.y, maxXYZombiePlace.y);
            enemyDirection(enemy);
        }

        if (enemy.name == carnivalCreatureGameObjName)
        {
            putPosition.x = Random.Range(minXYCarnivalCreaturePlace.x, maxXYCarnivalCreaturePlace.y);
            putPosition.y = Random.Range(minXYCarnivalCreaturePlace.y, maxXYCarnivalCreaturePlace.y);
            enemyDirection(enemy);
        }

        enemy.transform.position = putPosition;
    }

    void enemyDirection(GameObject enemy)
    {
        Vector3 enemyLocalScale = enemy.GetComponent<Transform>().localScale;

        enemy.GetComponent<Transform>().localScale = new Vector3(Mathf.Sign(Random.Range(-1, 1)) * enemyLocalScale.x, enemyLocalScale.y, enemyLocalScale.z);
    }
}