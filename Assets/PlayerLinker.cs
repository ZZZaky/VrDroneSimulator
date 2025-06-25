using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLinker : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public GameObject Player { get { return player; } }
}
