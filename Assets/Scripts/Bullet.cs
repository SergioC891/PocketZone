using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 4.0f;
    [SerializeField] private string carnivalCreatureGameObjName;
    [SerializeField] private string zombieGameObjName;
    [SerializeField] private Vector3 defaultBulletPositionOffset = new Vector3(0.35f, 0.09f, 0.0f);

    private bool fireFlag = false;
    private float shootDelayTime = 1.0f;
    private float bulletDirection;

    void Update()
    {
        if (fireFlag)
        {
            transform.Translate(0, -bulletDirection * bulletSpeed * Time.deltaTime, 0);
        }
    }

    public void shoot(Transform playerTransform)
    {
        StartCoroutine(shootProcess(playerTransform));
    }

    IEnumerator shootProcess(Transform playerTransform)
    {
        fireFlag = true;

        bulletDirection = Mathf.Sign(playerTransform.localScale.x);
        this.transform.position = new Vector3(playerTransform.position.x + (bulletDirection * defaultBulletPositionOffset.x), playerTransform.position.y + defaultBulletPositionOffset.y, 0.0f);

        yield return new WaitForSeconds(shootDelayTime);

        finalizeShootProcess();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (
            (other.name == carnivalCreatureGameObjName
            || other.name == zombieGameObjName)
           )
        {
            finalizeShootProcess();
        }
    }

    void finalizeShootProcess()
    {
        fireFlag = false;
        this.gameObject.SetActive(false);
    }
}