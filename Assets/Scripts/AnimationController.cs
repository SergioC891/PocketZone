using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private GameObject playerGameObj;
    [SerializeField] private InventoryPopup popup;

    private SpriteRenderer _spriteRenderer;
    private Vector3 defaultLocalScale;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        defaultLocalScale = playerGameObj.GetComponent<Transform>().localScale;
        popup.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float horInput = CnInputManager.GetAxis("Horizontal");
        float vertInput = CnInputManager.GetAxis("Vertical");

        if (horInput != 0 || vertInput != 0)
        {
            if (Mathf.Abs(horInput) > Mathf.Abs(vertInput))
            {
                playerGameObj.GetComponent<Transform>().localScale = new Vector3(Mathf.Sign(horInput) * defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
                _spriteRenderer = playerGameObj.GetComponent<SpriteRenderer>();
            }
            else
            {
                if (vertInput > 0)
                {
                    _spriteRenderer = playerGameObj.GetComponent<SpriteRenderer>();
                }
                else
                {
                    _spriteRenderer = playerGameObj.GetComponent<SpriteRenderer>();
                }
            }
        }

        if (CnInputManager.GetButtonUp("Equipment"))
        {
            if (!popup.gameObject.activeSelf)
            {
                popup.gameObject.SetActive(true);
                popup.Refresh();
                playerGameObj.GetComponent<HealthBox>().showHealthBox(false);
            }
            else
            {
                popup.showDeleteButton(false);
                popup.gameObject.SetActive(false);
                playerGameObj.GetComponent<HealthBox>().showHealthBox(true);
            }
        }
    }
}