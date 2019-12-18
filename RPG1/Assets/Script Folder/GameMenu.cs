using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameMenu : MonoBehaviour
{
    public GameObject theMenu;
    public GameObject[] windows;

    private CharStats[] playerStats;

    public Text[] nameText, hpText, mpText, lvlText, expText;
    public Slider[] expSlider;
    public Image[] charImage; 
    public GameObject[] charStatHolder;

    //the following variables are used in status window.
    public Text statusName, statusHP, statusMP, statusStrength, statusDefence, statusWpnEqpd, statusWpnPwr, statusArmrEqpd, statusArmorPwr, statusExp;
    public Image statusImage;

    public GameObject[] statusButtons;
    //the following variables are used in inventory
    public ItemButton[] itemButtons;


    public string selectedItem;
    public Item activeItem;
    public Text itemName, itemDescription, useButtonText;


    public static GameMenu instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2")) //Click derecho del mouse
        {
        if(theMenu.activeInHierarchy) //Si el menú está abierto..
                CloseMenu(); //Se cierra
        else
            {
                theMenu.SetActive(true);//Se abre el menú
                UpdateMainStats();//Se actualizan las estadísticas
                GameManager.instance.gameMenuOpen = true; //En el GameManager se establece que el menú ya está abierto
            }
        }
        
    }
    public void UpdateMainStats()
    {
        playerStats = GameManager.instance.playerStats;

        for (int i = 0; i < playerStats.Length; i++)
        {
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                charStatHolder[i].SetActive(true);

                nameText[i].text = playerStats[i].charName;
                hpText[i].text = "HP: " + playerStats[i].currentHP + "/" + playerStats[i].maxHP;
                mpText[i].text = "MP: " + playerStats[i].currentMP + "/" + playerStats[i].maxMP;
                lvlText[i].text = "LVL: "+ playerStats[i].playerLevel;
                expText[i].text = ""+playerStats[i].currentEXP + "/" + playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                expSlider[i].maxValue = playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                expSlider[i].value = playerStats[i].currentEXP;
                charImage[i].sprite = playerStats[i].charImage;



            }
            else
            {
                charStatHolder[i].SetActive(false);
            }
        }
    }

    public void ToogleWindow(int windowNumber)//para abrir la ventana correspondiente del menú. 
    {
        UpdateMainStats();
        for (int i = 0; i < windows.Length; i++)
        {
            if (i == windowNumber)
                windows[i].SetActive(!windows[i].activeInHierarchy);//activa la ventana correspondiente
            else
                windows[i].SetActive(false);//se van desactivando las que no son
        }
    }

    public void CloseMenu()
    {
        for (int i = 0; i < windows.Length; i++)
            windows[i].SetActive(false);//se desactivan todas las ventanas
        theMenu.SetActive(false);//se desactiva el menu
        GameManager.instance.gameMenuOpen = false;//en el gameManager se establece que el menu no está abierto.
    }

    public void OpenStatus()
    {
        UpdateMainStats();
        StatusChar(0);
        //update the information that is shown
        for (int i = 0; i < statusButtons.Length; i++)
        {
            statusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
            statusButtons[i].GetComponentInChildren<Text>().text = playerStats[i].charName;
        }
    }

    public void StatusChar(int selected)
    {
        statusName.text = playerStats[selected].charName;
        statusHP.text = "" + playerStats[selected].currentHP + "/" + playerStats[selected].maxHP;
        statusMP.text = "" + playerStats[selected].currentMP + "/" + playerStats[selected].maxMP;
        statusStrength.text = "" + playerStats[selected].strength;
        statusDefence.text = "" + playerStats[selected].defence;
        if (playerStats[selected].equipedWpn != "")
        {
            statusWpnEqpd.text = playerStats[selected].equipedWpn;
        }
        statusWpnPwr.text = playerStats[selected].wpnPwr.ToString();
        if (playerStats[selected].equippedArm != "")
        {
            statusArmrEqpd.text = playerStats[selected].equippedArm;
        }
        statusArmorPwr.text = playerStats[selected].armrPwr.ToString();
        statusExp.text = (playerStats[selected].expToNextLevel[playerStats[selected].playerLevel] - playerStats[selected].currentEXP).ToString();
        statusImage.sprite = playerStats[selected].charImage;

    }

    public void ShowItems()
    {
        GameManager.instance.SortItems(); //se acomodan todos los items para no dejar espacios vacios
        for (int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].buttonValue = i; 
            if(GameManager.instance.itemsHeld[i] != "") //si hay un item
            {
                itemButtons[i].buttonImage.gameObject.SetActive(true); //se deja visible
                itemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite; //se le coloca el sprite
                itemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();//se le asigna la cantidad
            }
            else
            {
                itemButtons[i].buttonImage.gameObject.SetActive(false); //se desactiva el boton
                itemButtons[i].amountText.text = ""; //se pone null la cantidad
            }
        }
    }

    public void SelectItem(Item newItem)
    {
        activeItem = newItem; 
        if (activeItem.isItem) //si es un item
            useButtonText.text = "Use";
        else
            if (activeItem.isWeapon || activeItem.isArmour)//si es armor o weapon..
            useButtonText.text = "Equip";
        itemName.text = activeItem.itemName;
        itemDescription.text = activeItem.description;
    }

    public void DiscardItem()
    {
        if(activeItem!=null)
        {
            GameManager.instance.RemoveItem(activeItem.itemName);
        }
    }
}

