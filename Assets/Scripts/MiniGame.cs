using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame : MonoBehaviour
{
    public GameObject Player;
    public bool miniGameOn = false;
    public GameObject lockPick;
    Vector3 lockPickPos;

    public float maxRotDetect;
    public float range;

    AudioSource audio;
    public bool isStuck = true;
    public AudioClip LockpickingStuck;
    public AudioClip LockpickingTurn;

    private void Awake()
    {
        gameObject.SetActive(false);
        audio = GetComponent<AudioSource>();
        lockPickPos = lockPick.transform.position;
    }

    public GameObject EKeyButton;

    private void Update()
    {
        if(miniGameOn)
        {
            EKeyButton.SetActive(false);
            LockPicking();
        }
    }

    private void LockPicking()
    {
        lockPick.transform.eulerAngles = Vector3.forward * 180 * Mathf.Clamp((Input.mousePosition.x / Screen.width), 0.01f, 0.99f);
        lockPick.transform.eulerAngles = Vector3.forward * Mathf.Clamp(lockPick.transform.eulerAngles.z, 0, 180);

        if(isStuck)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (audio.clip != LockpickingStuck)
                {
                    audio.clip = LockpickingStuck;
                    audio.Play();
                }
            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                if (audio.clip == LockpickingStuck && audio.isPlaying)
                {
                    audio.Stop();
                    lockPick.transform.position = lockPickPos;
                    audio.clip = null;
                }
            }
            else if(Input.GetKey(KeyCode.E))
            {
                Vibration();
            }
        }
    }

    private void Vibration()
    {
        Vector3 temp = new Vector3(lockPickPos.x + Random.Range(-5, 5), lockPickPos.y + Random.Range(-5, 5), lockPickPos.z);
        lockPick.transform.position = Vector3.Lerp(lockPick.transform.position, temp, Time.deltaTime * 50);
    }


    public void AbortButton()
    {
        miniGameOn = false;
        gameObject.SetActive(false);
        Player.GetComponent<CharacterControl>().isLockPicking = false;
    }
}
