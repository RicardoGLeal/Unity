using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text dialogText;
    public Text nameText;

    public GameObject dialogBox;
    public GameObject nameBox;
    
    public string[] dialogLines;

    public int currentLine;
    private bool justStarted;

    public static DialogManager instance; //to access from dialog activator
    void Start()
    {
        instance = this;
        // dialogText.text = dialogLines[currentLine];   
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogBox.activeInHierarchy)//Si el dialogBox esta abierto, mostrado.
        {
            if (Input.GetButtonUp("Fire1"))//RELEASED BOTTOM
            {
                if (!justStarted)//si no es la primera vez...
                {
                    currentLine++;//se pasa a la siguiente linea
                    if (currentLine >= dialogLines.Length)//si ya llego a la ultima linea..
                    {
                        dialogBox.SetActive(false);//se desactiva el dialogBox
                        GameManager.instance.dialogActive = false;
                    }
                    else
                    {
                        CheckIfName();
                        dialogText.text = dialogLines[currentLine];//se cambia el dialogText a la linea actual..
                    }
                }
                else
                {
                    justStarted = false;//justStarted = false, ya no es la primera vez.
                }
            }
        }
    }
    public void ShowDialog(string[] newLines, bool isPerson) //the first time
    {
        dialogLines = newLines; //se recibe newLines en dialogLines
        currentLine = 0;

        CheckIfName();

        dialogText.text = dialogLines[currentLine]; //Se imprime la primer línea
        dialogBox.SetActive(true);//Se activa el dialogBox

        justStarted = true;//justStarted = true porque se acaba de iniciar por primera vez.

        nameBox.SetActive(isPerson);
        GameManager.instance.dialogActive = true;
    }

    public void CheckIfName()
    {
        if(dialogLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogLines[currentLine].Replace("n-","");
            currentLine++;

        }
    }
}
