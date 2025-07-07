using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    [SerializeField] private string playerGameObjName = "Player";
    [SerializeField] private string bulletGameObjName = "Bullet(Clone)";

    public float speed = 0.3f;
    public float attackSpeed = 0.7f;
    public float delayBeetwenAttacks = 5.0f;
    public float attackTime = 0.7f;

    private bool attackStarted = false;
    private bool delayBeetwenAttacksFlag = false;

    private GameObject playerGameObj;
    private Vector3 characterLocalScale;

    void Start()
    {
        playerGameObj = GameObject.Find(playerGameObjName);

        characterLocalScale = this.gameObject.transform.localScale;

        speed = Mathf.Sign(characterLocalScale.x) * speed;
    }

    void Update()
    {
        if (!attackStarted)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        else
        {
            attackMove();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == playerGameObjName && (!attackStarted && !delayBeetwenAttacksFlag))
        {
            StartCoroutine(attack());
        }
        else if (collision.name != bulletGameObjName && collision.name != playerGameObjName)
        {
            characterLocalScale.x = -characterLocalScale.x;
            this.gameObject.transform.localScale = characterLocalScale;
            speed = -speed;
        }
    }

    void attackMove()
    {
        Vector3 targetPosition = playerGameObj.transform.position;
        Vector3 directionToMove = targetPosition - transform.position;
        float maxLength = Vector3.Distance(transform.position, targetPosition);

        directionToMove = directionToMove.normalized * Time.deltaTime * attackSpeed;
        transform.position = transform.position + Vector3.ClampMagnitude(directionToMove, maxLength);
    }

    IEnumerator attack()
    {
        if (!attackStarted && !delayBeetwenAttacksFlag)
        {
            attackStarted = true;

            playerGameObj.GetComponent<HealthBox>().decreaseHealth();

            StartCoroutine(attackFX());

            yield return new WaitForSeconds(attackTime);

            attackStarted = false;
            delayBeetwenAttacksFlag = true;

            yield return new WaitForSeconds(delayBeetwenAttacks);

            delayBeetwenAttacksFlag = false;
        }
    }

    IEnumerator attackFX()
    {
        for (int i = 0; i < 4; i++)
        {
            characterLocalScale.x = -characterLocalScale.x;
            this.gameObject.transform.localScale = characterLocalScale;

            yield return new WaitForSeconds(0.15f);
        }

    }
}
