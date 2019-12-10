using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    public string[] lines;
    private bool canActivate;//the playe ris in the activate area


    void Start()
    {
        
    }

    void Update()
    {
        if (canActivate && Input.GetButtonDown("Fire1") && !DialogManager.instance.dialogBox.activeInHierarchy)
        {
            DialogManager.instance.ShowDialog(lines);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
            canActivate = false;
    }
}
