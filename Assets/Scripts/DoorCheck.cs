using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorCheck : MonoBehaviour
{
    public GameObject EKeyImage;
    public GameObject Panel;
    public GameObject Player;

    private void OnTriggerStay(Collider other)
    {
        {
            if (other.transform.tag == "Player")
            {
                if(!Panel.GetComponent<MiniGame>().miniGameOn)
                {
                    EKeyImage.SetActive(true);
                    if (Input.GetKey(KeyCode.E))
                    {
                        Panel.GetComponent<MiniGame>().miniGameOn = true;
                        Panel.SetActive(true);
                    }

                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            EKeyImage.SetActive(false);
        }
    }

}
