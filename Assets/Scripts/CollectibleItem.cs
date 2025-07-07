using UnityEngine;
using System.Collections;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private string playerGameObjName = "Player";
    [SerializeField] private string bulletsName = "bullets";
    [SerializeField] private int bulletsInPackage = 10;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == playerGameObjName)
        {
            if (itemName == bulletsName)
            {
                for (int i = 0; i < bulletsInPackage; i++)
                {
                    Managers.Inventory.AddItem(itemName);
                }
            }
            else
            {
                Managers.Inventory.AddItem(itemName);
            }

            Managers.Data.SaveGameState();

            Destroy(this.gameObject);
        }
    }
}