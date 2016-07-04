using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntityInventory : MonoBehaviour {

    //https://www.reddit.com/r/Unity3D/comments/2fxsd9/tutorial_inventory_and_crafting_system_with_the/

    [SerializeField]
    private List<Item> m_items;
    [SerializeField]
    private int m_inventorySize;

    public List<Item> GetItem { get { return m_items; } }
    public int CurrentSize { get { return m_items.Count; } }
    public float InventorySize { get { return m_inventorySize; } }


    public void AddItem(Item item)
    {
        if(m_items.Count < m_inventorySize)
        {
            m_items.Add(item);
        }
        else
        {
            return;
        }
    }

    public void RemoveItem(Item item)
    {
        m_items.Remove(item);
    }

    public bool IsInventoryFull()
    {
        if(m_items.Count < m_inventorySize)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
