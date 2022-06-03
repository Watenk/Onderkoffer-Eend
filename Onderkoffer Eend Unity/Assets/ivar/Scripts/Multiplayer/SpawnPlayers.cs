using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject LiftPlayerPrefab;
    public GameObject PlayerPrefab;

    Vector3 LiftPlayerSpawn = new (400, 0, 300);

    private void Start()
    {
        PhotonNetwork.Instantiate(PlayerPrefab.name, LiftPlayerSpawn, Quaternion.identity);
    }
}