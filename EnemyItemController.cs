using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyItemController : MonoBehaviour {

    [SerializeField]
    private Transform m_primaryItemHold;
    [SerializeField]
    private Transform m_secondaryItemHold;

    [SerializeField]
    private float m_itemSwitchRate;

    private int m_inventorySize;
    private bool m_mainItemEquipped;
    private bool m_secondaryItemEquipped;
    private bool m_isPirmaryItemEquipped;
    private bool m_isSecondaryItemEquipped;
    private Enemy m_enemy;
    private EntityInventory m_inventory;
    private Item m_equippedPrimaryItem;
    private Item m_equippedSecondaryItem;

    public float ItemUseTime { get { return m_equippedPrimaryItem.TimeBetweenUse; } }

    private void Awake()
    {
        m_enemy = GetComponent<Enemy>();
        m_inventory = GetComponent<EntityInventory>();
    }

    private void Start()
    {
        m_mainItemEquipped = false;
        m_secondaryItemEquipped = false;
        EquipItem(0);
    }

    public IEnumerator DecideWeapon()
    {
        while(m_enemy.AttackTarget)
        {
            //Has more than one item
            if(m_inventorySize > 1)
            {
                float sqrLengthToTarget = (m_enemy.TargetPos.position - transform.position).sqrMagnitude;
                //Within range of primary weapon
                if (sqrLengthToTarget >= m_enemy.ItemSwitchRange * m_enemy.ItemSwitchRange && !m_mainItemEquipped)
                {
                    Debug.Log("Equip Primary");
                    EquipItem(0);

                    m_mainItemEquipped = true;
                    m_secondaryItemEquipped = false;
                }

                //Within range of secondary weapon
                else if(sqrLengthToTarget < m_enemy.ItemSwitchRange * m_enemy.ItemSwitchRange && !m_secondaryItemEquipped)
                {
                    Debug.Log("Equip Secondary");
                    EquipItem(1);

                    m_mainItemEquipped = false;
                    m_secondaryItemEquipped = true;
                }
            }

            yield return new WaitForSeconds(m_itemSwitchRate);
        }
    }

    public void UsePrimaryItem()
    {
        //if primary item equipped
        if(m_isPirmaryItemEquipped)
        {
            m_equippedPrimaryItem.ComputerUse();
        }
    }

    private void EquipItem(int i)
    {
        //Equip single handed item
        if (m_inventory.GetItem[i].GetHoldSize == Item.HoldSize.TwoHanded)
        {
            EquipTwoHandedItem(m_inventory.GetItem[i]);
        }
        //Equip two handed item
        else if (m_inventory.GetItem[i].GetHoldSize == Item.HoldSize.SingleHanded)
        {
            EquipSingleHandedItem(m_inventory.GetItem[i]);
        }
    }

    private void EquipTwoHandedItem(Item item)
    {
        //If primary item is equipped
        if (m_isPirmaryItemEquipped)
        {
            m_equippedPrimaryItem.Equipped = false;
            m_equippedPrimaryItem.gameObject.SetActive(false);
        }
        //If secondary item is equipped
        if (m_isSecondaryItemEquipped)
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
        if (item.ItemType == Item.Type.Weapon)
        {
            //Unequip existing item
            if (m_isPirmaryItemEquipped)
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
