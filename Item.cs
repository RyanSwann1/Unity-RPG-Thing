using UnityEngine;
using System.Collections;


public class Item : MonoBehaviour
{
    [System.Serializable]
    public enum Type
    {
        Weapon,
        Quest,
        Consumable,
        Tool
    }

    public enum HoldSize
    {
        SingleHanded,
        TwoHanded
    }


    [System.Serializable]
    public struct ItemDetails
    {
        public string m_itemName;
        public int m_itemID;
        public string m_itemDescription;
        public Texture2D m_itemIcon;
        //public bool m_isEquipped;
    }
   

    public bool m_equipped;
    public bool m_twoHanded;
    public float m_timeBetweenUse;

    [SerializeField]
    protected Type m_itemType;
    [SerializeField]
    protected HoldSize m_holdSize;

    private void Start()
    {
        m_equipped = false;
    }

    public virtual void PlayerUse()
    {

    }

    public virtual void ComputerUse()
    {

    }
    
    public float TimeBetweenUse { get { return m_timeBetweenUse; } set { m_timeBetweenUse = value; } }
    public bool Equipped { get { return m_equipped; } set { m_equipped = value; } }
    public bool TwoHanded { get { return m_twoHanded; } set { m_twoHanded = value; } }
    public Type ItemType { get { return m_itemType; } set { m_itemType = value; } }
    public HoldSize GetHoldSize { get { return m_holdSize; } }
}
