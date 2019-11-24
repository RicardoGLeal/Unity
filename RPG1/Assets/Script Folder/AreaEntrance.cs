using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    // Start is called before the first frame update
    public string areaTransitionName;
    void Start()
    {
        if (PlayerController.instance.areaTransitionName == areaTransitionName)
            PlayerController.instance.transform.position = transform.position;

        UIFade.instance.FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
