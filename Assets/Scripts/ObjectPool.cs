using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject zombieObjToPool;
    public int zombieAmountToPool;
    public GameObject carnivalCreatureObjToPool;
    public int carnivalCreatureAmountToPool;
    public GameObject bulletObjToPool;
    public int bulletAmountToPool;
    private int totalAmountToPool;

    private void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();

        putObjectToPool(zombieObjToPool, zombieAmountToPool);
        putObjectToPool(carnivalCreatureObjToPool, carnivalCreatureAmountToPool);
        putObjectToPool(bulletObjToPool, bulletAmountToPool);

        totalAmountToPool = zombieAmountToPool + carnivalCreatureAmountToPool + bulletAmountToPool;
    }

    private void putObjectToPool(GameObject objectToPool, int amountToPool)
    {
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    public GameObject GetPooledObject(string objName)
    {
        for (int i = 0; i < totalAmountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].name == objName)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }
}