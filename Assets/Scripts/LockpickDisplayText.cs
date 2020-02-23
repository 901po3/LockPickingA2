using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockpickDisplayText : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        GetComponent<Text>().text = "X " + player.GetComponent<CharacterControl>().NumberOfLockpicks.ToString();
    }
}
