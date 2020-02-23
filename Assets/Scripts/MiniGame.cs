﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame : MonoBehaviour
{
    public GameObject Player;
    public bool miniGameOn = false;
    public GameObject lockPick;
    public GameObject lockFrame;
    Vector3 lockPickPos;

    public float maxRotDetect;
    public float range;
    private float unlockingAngle;

    AudioSource audio;
    public bool isStuck = true;
    public AudioClip LockpickingStuck;
    [Range(5, 175)]
    public AudioClip LockpickingTurn;

    private void Awake()
    {
        gameObject.SetActive(false);
        audio = GetComponent<AudioSource>();
        lockPickPos = lockPick.transform.localPosition;
        unlockingAngle = Random.Range(5, 176);
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

        if(lockPick.transform.eulerAngles.z >= unlockingAngle - range && lockPick.transform.eulerAngles.z <= unlockingAngle + range)
        {
            isStuck = false;
        }
        else
        {
            isStuck = true;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isStuck)
            {
                audio.clip = LockpickingStuck;
                audio.Play();
            }
            else if(!isStuck)
            {
                audio.clip = LockpickingTurn;
                audio.Play();
            }
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            lockPick.transform.localPosition = lockPickPos;
            lockFrame.transform.localRotation = Quaternion.Euler(0, 0, 0);
            if (audio.isPlaying)
            {
                audio.Stop();
            }
        }
        if(Input.GetKey(KeyCode.E))
        {
            if(!isStuck)
            {
                lockFrame.transform.Rotate(Vector3.forward * 30 * Time.deltaTime, Space.World);
            }
            if(isStuck && audio.clip == LockpickingTurn)
            {
                audio.Stop();
                audio.clip = LockpickingStuck;
                audio.Play();
            }
            if (!isStuck && audio.clip == LockpickingStuck)
            {
                audio.Stop();
                audio.clip = LockpickingTurn;
                audio.Play();
            }
            Vibration();
        }
    }

    private void Vibration()
    {
        Vector3 temp = new Vector3(lockPickPos.x + Random.Range(-5, 5), lockPickPos.y + Random.Range(-5, 5), lockPickPos.z);
        lockPick.transform.localPosition = Vector3.Lerp(lockPick.transform.localPosition, temp, Time.deltaTime * 50);
    }


    public void AbortButton()
    {
        miniGameOn = false;
        gameObject.SetActive(false);
        Player.GetComponent<CharacterControl>().isLockPicking = false;
    }
}
