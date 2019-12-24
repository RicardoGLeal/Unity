using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;
    public CharStats[] playerStats;

    public bool gameMenuOpen;
    public bool dialogActive;
    public bool fadingBetweenAreas;

    //to save the items of the player
    public string[] itemsHeld; //this string array saves all the items that the user has in his inventory.
    public int[] numberOfItems;
    public Item[] referenceItems;//array of all the items that exists in the game.
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        SortItems();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameMenuOpen || dialogActive || fadingBetweenAreas) //si el menu esta abierto || hay un diálogo npc activo || transicion
            PlayerController.instance.canMove = false; //no te puedes mover
        else
            PlayerController.instance.canMove = true;//the player can move

        if(Input.GetKeyDown(KeyCode.Y))//al presionar y
        {
            AddItem("Iron Armor");
            RemoveItem("Health Potion");
            //RemoveItem("bleep");
        }

    }

    public Item GetItemDetails(string itemToWrap)
    {
        for (int i = 0; i < referenceItems.Length; i++) //se busca en la colección de items existentes...
        {
            if(referenceItems[i].itemName == itemToWrap) 
                return referenceItems[i]; //se retorna el item encontrado
        }
        return null;
    }

    public void SortItems()
    {
        bool itemAfterSpace = true;

        while (itemAfterSpace)
        {
            itemAfterSpace = false;
            for (int i = 0; i < itemsHeld.Length - 1; i++)
            {
                if (itemsHeld[i] == "")
                {
                    itemsHeld[i] = itemsHeld[i + 1];
                    itemsHeld[i + 1] = "";

                    numberOfItems[i] = numberOfItems[i + 1];
                    numberOfItems[i + 1] = 0;

                    if (itemsHeld[i] != "")
                        itemAfterSpace = true;
                }
            }
        }
    }

    public void AddItem(string itemToAdd)
    {
        int newItemPosition = 0; //guardara la posicion donde se guardara el item
        bool foundSpace = false; // bandera para saber is ya encontro el espacio
        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if(itemsHeld[i] == "" || itemToAdd == itemsHeld[i] ) //si se llegó a un espacio vació, o si encontró un item igual...
            {
                newItemPosition = i;
                i = itemsHeld.Length;
                foundSpace = true;
            }
        }
        if(foundSpace)
        {
            bool itemExists = false;
            for (int i = 0; i < referenceItems.Length; i++)
            {
                if(referenceItems[i].itemName == itemToAdd) //si se encontró el item en la lista de items de referencia..
                {
                    itemExists = true; //item encontrado (existe)
                    break;
                }
            }
            //jack reacher sin regreso
            if(itemExists)
            {
                itemsHeld[newItemPosition] = itemToAdd; 
                numberOfItems[newItemPosition]++;
            }
            else
            {
                Debug.LogError(itemToAdd + "does not exists!");
            }
        }
        GameMenu.instance.ShowItems();
    }
    public void RemoveItem(string itemToRemove)
    {
        bool foundItem = false;
        int itemPosition = 0;
        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if(itemToRemove == itemsHeld[i])
            {
                foundItem = true;
                itemPosition = i;
                break;
            }
        }

        if (foundItem)
        {
            numberOfItems[itemPosition]--;
            if(numberOfItems[itemPosition] <=0)
            {
                itemsHeld[itemPosition] = "";
            }
            GameMenu.instance.ShowItems();
        }
        else
            Debug.LogError("Could not find the item");
    }
}
