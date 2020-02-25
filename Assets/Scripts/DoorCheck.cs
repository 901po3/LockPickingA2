using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorCheck : MonoBehaviour
{
    public GameObject LockPickDisplayPanel;
    public GameObject EKeyImage;
    public GameObject Panel;
    public GameObject Player;
    public GameObject PlayerPack;

    public float maxRotDetect;
    public float range;
    public int timer;

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (!Panel.GetComponent<MiniGame>().miniGameOn)
            {
                EKeyImage.SetActive(true);
                if (Input.GetKey(KeyCode.E) && Player.GetComponent<CharacterControl>().NumberOfLockpicks > 0)
                {
                    MiniGame miniGame = Panel.GetComponent<MiniGame>();
                    miniGame.miniGameOn = true;
                    miniGame.maxRotDetect = maxRotDetect;
                    miniGame.range = range;
                    miniGame.timer = timer;
                    miniGame.unlockingAngle = Random.Range(5, 176);
                    miniGame.door = gameObject;
                    Player.GetComponent<CharacterControl>().isLockPicking = true;
                    LockPickDisplayPanel.SetActive(false);
                    Panel.SetActive(true);
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
