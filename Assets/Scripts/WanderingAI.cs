using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    [SerializeField] private string playerGameObjName = "Player";
    [SerializeField] private string bulletGameObjName = "Bullet(Clone)";

    public float speed = 0.3f;
    public float obstacleRange = 1.0f;

    private bool attackStarted = false;

    private GameObject playerGameObj;
    private Vector3 characterLocalScale;

    void Start()
    {
        playerGameObj = GameObject.Find(playerGameObjName);

        characterLocalScale = this.gameObject.transform.localScale;

        speed = Mathf.Sign(characterLocalScale.x) * speed;
    }


    // Update is called once per frame
    void Update()
    {
        if (!attackStarted)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == playerGameObjName && !attackStarted)
        {
            StartCoroutine(attack());
        }
        else if (collision.name != bulletGameObjName)
        {
            characterLocalScale.x = -characterLocalScale.x;
            this.gameObject.transform.localScale = characterLocalScale;
            speed = -speed;
        }
    }

    IEnumerator attack()
    {
        attackStarted = true;

        playerGameObj.GetComponent<HealthBox>().decreaseHealth();

        StartCoroutine(attackFX());

        yield return new WaitForSeconds(.15f);

        attackStarted = false;
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
