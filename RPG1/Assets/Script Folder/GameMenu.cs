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

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
        if(theMenu.activeInHierarchy)
            {
                CloseMenu();
            }
        else
            {
                theMenu.SetActive(true);
                UpdateMainStats();
                GameManager.instance.gameMenuOpen = true;
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

    public void ToogleWindow(int windowNumber)
    {
        UpdateMainStats();
        for (int i = 0; i < windows.Length; i++)
        {
            if (i == windowNumber)
            {
                windows[i].SetActive(!windows[i].activeInHierarchy);
            }
            else
                windows[i].SetActive(false);
        }
    }

    public void CloseMenu()
    {
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);

        }
        theMenu.SetActive(false);
        GameManager.instance.gameMenuOpen = false;
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
}

