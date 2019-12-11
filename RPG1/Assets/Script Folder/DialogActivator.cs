using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    public string[] lines;
    private bool canActivate;//the playe ris in the activate area
    public bool isPerson = true; 
    void Start()
    {    
    }

    void Update()
    {
        if (canActivate && Input.GetButtonDown("Fire1") && !DialogManager.instance.dialogBox.activeInHierarchy)//Si el jugador está en la zona de colisión, presiona el click izquierdo y dialogManager no está
            //abierto..
        {
            DialogManager.instance.ShowDialog(lines, isPerson);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            canActivate = true; //si el jugador entra a la zona de activación, canActivate se vuelve true
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
            canActivate = false;
    }
}
