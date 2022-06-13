using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject LiftPlayerPrefab;
    public GameObject PlayerPrefab;
    public GameObject LiftLight;

    public Vector3 LiftPlayerSpawn = new(500, 125 ,190);
    public Vector3 PlayerSpawn = new(400, 0, 300);

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(LiftPlayerPrefab.name, LiftPlayerSpawn, Quaternion.identity);
            LiftLight.SetActive(true);
        }
        else
        {
        PhotonNetwork.Instantiate(PlayerPrefab.name, PlayerSpawn, Quaternion.identity);
        }
    }
}