using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item Type")]
    public bool isItem;
    public bool isWeapon;
    public bool isArmour;

    [Header("Item Details")]
    public string itemName;
    public string description;
    public int value;
    public Sprite itemSprite;

    [Header("Item Details")]
    public int amountToChange;
    public bool affectMP, affectHP, affectStr;

    [Header("Weapon/Armor Details")]
    public int weaponStrength;
    public int armorStrength;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use(int charToUseOn)
    {
        CharStats selectChar = GameManager.instance.playerStats[charToUseOn];
        if(isItem)
        {
            if(affectHP)
            {
            selectChar.currentHP += amountToChange;
                if(selectChar.currentHP > selectChar.maxHP)
                {
                    selectChar.currentHP = selectChar.maxHP;
                }
            }

            if (affectMP)
            {
                selectChar.currentMP += amountToChange;
                if (selectChar.currentMP > selectChar.maxMP)
                {
                    selectChar.currentMP = selectChar.maxMP;
                }
            }
            if(affectStr)
            {
                selectChar.strength += amountToChange;

            }
        }
        if(isWeapon)
        {
            if(selectChar.equipedWpn != "")
            {
            GameManager.instance.AddItem(selectChar.equipedWpn);
            }
            selectChar.equipedWpn = itemName;
            selectChar.wpnPwr = weaponStrength;
        }
        if(isArmour)
        {
            if (selectChar.equippedArm != "")
            {
                GameManager.instance.AddItem(selectChar.equippedArm);
            }
            selectChar.equippedArm = itemName;
            selectChar.armrPwr = armorStrength;
        }
        GameManager.instance.RemoveItem(itemName);
    }
}
