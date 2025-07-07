using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarnivalCreature : MonoBehaviour
{
    [SerializeField] private string[] carnykItems = new string[5];

    public string bulletName = "Bullet(Clone)";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == bulletName)
        {
            this.gameObject.GetComponent<HealthBox>().decreaseHealth();
        }
    }

    public void destroyProcess()
    {
        putItem();
    }

    private void putItem()
    {
        string itemName = carnykItems[Random.Range(0, 5)];

        GameObject item = (GameObject)Resources.Load(itemName);

        if (item != null)
        {
            GameObject _item = (GameObject)Instantiate(item, new Vector3(transform.position.x, transform.position.y, 0.0f), Quaternion.identity);
        }
    }
}
