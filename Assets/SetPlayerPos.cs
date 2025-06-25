using FDS.FlightScenarios.ScenarioActions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SetPlayerPos : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private Transform spawn;

    [Inject]
    private void Construct(PlayerLinker playerLink)
    {
        player = playerLink.Player;
    }

    private void OnEnable()
    {
        player.transform.position = spawn.position;
        player.transform.rotation = spawn.rotation;
    }
}
