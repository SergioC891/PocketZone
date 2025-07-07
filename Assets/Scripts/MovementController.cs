using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class MovementController : MonoBehaviour
{
    [SerializeField] private InventoryPopup popup;

    private Rigidbody2D _rigidBody;
    public float moveSpeed = 4.0f;
    public string bulletGameObjName = "Bullet(Clone)";
    public string bulletsName =  "bullets";
    public int bulletsOnStart = 60;

    private bool shootFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();


        for (int i = 0; i < bulletsOnStart; i++)
        {
            if (Managers.Inventory.GetItemCount(bulletsName) < bulletsOnStart)
            {
                Managers.Inventory.AddItem(bulletsName);
            }
            else
            {
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = Vector2.zero;

        float horInput = CnInputManager.GetAxis("Horizontal");
        float vertInput = CnInputManager.GetAxis("Vertical");

        if (horInput != 0 || vertInput != 0)
        {
            movement.x = horInput * moveSpeed;
            movement.y = vertInput * moveSpeed;

            movement = Vector2.ClampMagnitude(movement, moveSpeed);
        }

        movement *= Time.deltaTime;
        _rigidBody.MovePosition(_rigidBody.position + movement);

        if (CnInputManager.GetButtonDown("Fire") && !shootFlag)
        {
            shoot();
        }
    }

    private void shoot()
    {
        shootFlag = true;

        GameObject bullet = ObjectPool.SharedInstance.GetPooledObject(bulletGameObjName);

        if (bullet != null)
        {
            bullet.SetActive(true);
            bullet.SendMessage("shoot", this.transform);
        }

        Managers.Inventory.decItem(bulletsName);

        if (popup.gameObject.activeSelf)
        {
            popup.Refresh();
        }

        StartCoroutine(delayOnShoot());
    }

    IEnumerator delayOnShoot()
    {
        yield return new WaitForSeconds(0.2f);

        shootFlag = false;
    }

    public void destroyProcess()
    {
        Managers.Data.SaveGameState();
    }
}