using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class InventoryPopup : MonoBehaviour
{
    [SerializeField] private Image[] itemIcons;
    [SerializeField] private Text[] itemLabels;

    [SerializeField] private Button deleteButton;

    private string _curItem;

    public void Refresh()
    {
        List<string> itemList = Managers.Inventory.GetItemList();

        int len = itemIcons.Length;
        for (int i = 0; i < len; i++)
        {
            if (i < itemList.Count)
            {
                itemIcons[i].gameObject.SetActive(true);
                itemLabels[i].gameObject.SetActive(true);

                string item = itemList[i];

                Sprite sprite = Resources.Load<Sprite>("Icons/" + item);
                itemIcons[i].sprite = sprite;
                itemIcons[i].SetNativeSize();

                int count = Managers.Inventory.GetItemCount(item);
                string message = count > 1 ? "x" + count : "";
                itemLabels[i].text = message;

                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerClick;
                entry.callback.AddListener((BaseEventData data) => { OnItem(item); });
                EventTrigger trigger = itemIcons[i].GetComponent<EventTrigger>();
                trigger.triggers.Clear();
                trigger.triggers.Add(entry);
            }
            else
            {
                itemIcons[i].gameObject.SetActive(false);
                itemLabels[i].gameObject.SetActive(false);
            }
        }

        if (!itemList.Contains(_curItem))
        {
            _curItem = null;
        }

        if (_curItem == null)
        {
            deleteButton.gameObject.SetActive(false);
        }
        else
        {
            deleteButton.gameObject.SetActive(true);
        }
    }

    public void OnItem(string item)
    {
        _curItem = item;
        Refresh();
    }

    public void OnDelete(string item)
    {
        Managers.Inventory.deleteItem(_curItem);
        Refresh();
    }

    public void showDeleteButton(bool value)
    {
        _curItem = value ? _curItem : null;
        deleteButton.gameObject.SetActive(value);
    }
}
