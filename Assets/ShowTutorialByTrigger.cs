using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTutorialByTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject info;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            info.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            info.SetActive(false);
        }
    }
}
