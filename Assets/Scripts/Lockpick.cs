using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lockpick : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(Vector3.up, -100 * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            collision.gameObject.GetComponent<CharacterControl>().NumberOfLockpicks += 1;
            Destroy(gameObject);
        }
    }
}
