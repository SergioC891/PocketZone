using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour, IGameManager
{
    [SerializeField] private InventoryPopup popup;

    public ManagerStatus status { get; private set; }
    private Dictionary<string, int> _items;

    public void Startup()
    {
        Debug.Log("Inventory Manager starting...");

        UpdateData(new Dictionary<string, int>());

        status = ManagerStatus.Started;
    }

    private void DisplayItems()
    {
        string itemDisplay = "Items: ";
        foreach (KeyValuePair<string, int> item in _items)
        {
            itemDisplay += item.Key + "(" + item.Value + ") ";
        }
    }

    public void AddItem(string name)
    {
        if (_items.ContainsKey(name))
        {
            _items[name] += 1;
        }
        else
        {
            _items[name] = 1;
        }

        if (popup.gameObject.activeSelf)
        {
            popup.Refresh();
        }

        DisplayItems();
    }

    public void decItem(string name)
    {
        if (_items.ContainsKey(name))
        {
            _items[name] -= 1;
        }

        DisplayItems();
    }

    public void deleteItem(string name)
    {

        if (_items.ContainsKey(name))
        {
            _items.Remove(name);
        }

        DisplayItems();
    }

    public void deleteItems()
    {
        _items.Clear();
    }

    public List<string> GetItemList()
    {
        List<string> list = new List<string>();

        if (_items != null)
        {
            list = new List<string>(_items.Keys);
        }

        return list;
    }

    public int GetItemCount(string name)
    {
        if (_items != null && _items.ContainsKey(name))
        {
            return _items[name];
        }
        return 0;
    }

    public void UpdateData(Dictionary<string, int> items)
    {
        _items = items;
    }

    public Dictionary<string, int> GetData()
    {
        return _items;
    }
}
