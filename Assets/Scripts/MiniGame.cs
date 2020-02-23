using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame : MonoBehaviour
{
    public Text LockPickNumberText;
    public GameObject LockPickDisplayPanel;
    public GameObject Player;
    public bool miniGameOn = false;
    public GameObject lockPick;
    public GameObject lockFrame;
    public Text timerText;
    Vector3 lockPickPos;

    public int timer;
    private bool timerDelayOn = true;
    public float maxRotDetect;
    public float range;
    public float unlockingAngle;

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
    }

    public GameObject EKeyButton;

    private void Update()
    {
        if(miniGameOn)
        {
            EKeyButton.SetActive(false);
            LockPicking();
            Timer();
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

        if (Input.GetKeyDown(KeyCode.R))
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
        else if (Input.GetKeyUp(KeyCode.R))
        {
            lockPick.transform.localPosition = lockPickPos;
            lockFrame.transform.localRotation = Quaternion.Euler(0, 0, 0);
            lockPick.transform.localRotation = Quaternion.Euler(0, 0, 0);
            if (audio.isPlaying)
            {
                audio.Stop();
            }
        }
        if(Input.GetKey(KeyCode.R))
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

    private void Timer()
    {
        if (timerDelayOn)
        {
            timerDelayOn = false;
            StartCoroutine(ReduceTimer());
        }
        timerText.text = timer.ToString() + " Second(s)";
        if(timer == 0)
        {
            StartCoroutine(Exit());
        }
    }

    IEnumerator Exit()
    {
        yield return new WaitForSeconds(1.0f);
        Player.GetComponent<CharacterControl>().NumberOfLockpicks -= 1;
        if (Player.GetComponent<CharacterControl>().NumberOfLockpicks < 0)
            Player.GetComponent<CharacterControl>().NumberOfLockpicks = 0;
        LockPickNumberText.text = "X " + Player.GetComponent<CharacterControl>().NumberOfLockpicks.ToString();
        AbortButton();
    }

    IEnumerator ReduceTimer()
    {
        yield return new WaitForSeconds(1f);
        timerDelayOn = true;
        timer -= 1;
        if(timer < 0)
        {
            timer = 0;
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
        LockPickDisplayPanel.SetActive(true);
        timerDelayOn = true;
        audio.Stop();
        audio.clip = null;
    }

}
