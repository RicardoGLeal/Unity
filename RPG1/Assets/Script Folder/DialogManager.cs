using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text dialogText;
    public Text dialogName;

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
        if (dialogBox.activeInHierarchy)
        {
            if (Input.GetButtonUp("Fire1"))//RELEASED BOTTOM
            {
                if (!justStarted)
                {
                    currentLine++;
                    if (currentLine >= dialogLines.Length)
                        dialogBox.SetActive(false);
                    else
                        dialogText.text = dialogLines[currentLine];
                }
                else
                {
                    justStarted = false;
                }
            }
        }
    }
    public void ShowDialog(string[] newLines)
    {
        dialogLines = newLines;
        currentLine = 0;
        dialogText.text = dialogLines[0];
        dialogBox.SetActive(true);

        justStarted = true;
    }
}
