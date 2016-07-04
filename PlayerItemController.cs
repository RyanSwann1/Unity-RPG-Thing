using UnityEngine;
using System.Collections;

public class PlayerItemController : MonoBehaviour {

    private KeyCode[] keyCode =
    {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
        KeyCode.Alpha9,
        KeyCode.Alpha0
    };
    
    [SerializeField]
    private Transform m_primaryItemHold;
    [SerializeField]
    private Transform m_secondaryItemHold;

    private Player m_player;
    private EntityInventory m_inventory;
    private Item m_equippedPrimaryItem;
    private Item m_equippedSecondaryItem;

    private bool m_isPirmaryItemEquipped;
    private bool m_isSecondaryItemEquipped;

    private void Awake()
    {
        m_player = GetComponent<Player>();
        m_inventory = GetComponent<EntityInventory>();
    } 

    private void Start()
    {
        EquipItem(0);
    }
    
    private void Update()
    {
        for (int i = 0; i < keyCode.Length; i++)
        {
            if (Input.GetKeyDown(keyCode[i]) && i < m_inventory.CurrentSize)
            {
                EquipItem(i);
            }
        }
    }

    public void EquipItem(int i)
    {
        //Equip single handed item
        if(m_inventory.GetItem[i].GetHoldSize == Item.HoldSize.TwoHanded)
        {
            EquipTwoHandedItem(m_inventory.GetItem[i]);
        }
        //Equip two handed item
        else if(m_inventory.GetItem[i].GetHoldSize == Item.HoldSize.SingleHanded)
        {
            EquipSingleHandedItem(m_inventory.GetItem[i]);
        }
    }

    public void AddItem(Item item)
    {
        if (!m_inventory.IsInventoryFull())
        {
            //Add a two handed item or weapon
            if (item.GetHoldSize == Item.HoldSize.TwoHanded || item.ItemType == Item.Type.Weapon)
            {
                Item newItem = Instantiate(item, m_primaryItemHold.position, m_primaryItemHold.rotation) as Item;
                Destroy(newItem.GetComponent<Collider>());
                newItem.Equipped = false;
                newItem.transform.parent = m_primaryItemHold;
                newItem.gameObject.SetActive(false);
                m_inventory.AddItem(newItem);
            }
            else
            {
                Item newItem = Instantiate(item, m_secondaryItemHold.position, m_secondaryItemHold.rotation) as Item;
                Destroy(newItem.GetComponent<Collider>());
                newItem.Equipped = false;
                newItem.transform.parent = m_secondaryItemHold;
                newItem.gameObject.SetActive(false);
                m_inventory.AddItem(newItem);
            }
        }
    }

    public void UsePrimaryItem()
    {
        if(m_isPirmaryItemEquipped)
        {
            m_equippedPrimaryItem.PlayerUse();
        }
        
    }

    private void EquipTwoHandedItem(Item item)
    {
        //If primary item is equipped
        if(m_isPirmaryItemEquipped)
        {
            m_equippedPrimaryItem.Equipped = false;
            m_equippedPrimaryItem.gameObject.SetActive(false);
        }
        //If secondary item is equipped
        if(m_isSecondaryItemEquipped)
        {
            m_equippedSecondaryItem.Equipped = false;
            m_equippedSecondaryItem.gameObject.SetActive(false);
        }

        //Equip new Two Handed Item
        m_equippedPrimaryItem = item;
        m_equippedPrimaryItem.Equipped = true;
        m_equippedPrimaryItem.gameObject.SetActive(true);
        m_isPirmaryItemEquipped = true;

    }

    private void EquipSingleHandedItem(Item item)
    {
        //If this item is a weapon
        if(item.ItemType == Item.Type.Weapon)
        {
            //Unequip existing item
            if(m_isPirmaryItemEquipped)
            {
                m_equippedPrimaryItem.Equipped = false;
                m_equippedPrimaryItem.gameObject.SetActive(false);
            }

            //Equip new item
            m_equippedPrimaryItem = item;
            m_equippedPrimaryItem.Equipped = true;
            m_equippedPrimaryItem.gameObject.SetActive(true);
            m_isPirmaryItemEquipped = true;
        }
        else
        {
            //Unequip existing item
            if (m_isSecondaryItemEquipped)
            {
                m_equippedSecondaryItem.Equipped = false;
                m_equippedSecondaryItem.gameObject.SetActive(false);
            }

            m_equippedSecondaryItem = item;
            m_equippedSecondaryItem.Equipped = true;
            m_equippedSecondaryItem.gameObject.SetActive(true);
            m_isSecondaryItemEquipped = true;
        }
    }
}
