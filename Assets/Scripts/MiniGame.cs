using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame : MonoBehaviour
{
    public bool miniGameOn = false;  

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public GameObject EKeyButton;

    private void Update()
    {
        if(miniGameOn)
        {
            EKeyButton.SetActive(false);
        }
    }

    public void AbortButton()
    {
        miniGameOn = false;
        gameObject.SetActive(false);
    }
}
