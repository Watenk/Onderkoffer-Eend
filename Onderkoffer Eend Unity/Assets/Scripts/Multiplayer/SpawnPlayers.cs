using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    //References
    private RoomManager roomManager;

    public GameObject LiftPlayerPrefab;
    public GameObject PlayerPrefab;

    public Vector3 LiftPlayerSpawn = new(500, 125 ,190);
    public Vector3 PlayerSpawn = new (400, 0, 300);

    private void Start()
    {
        //References
        roomManager = FindObjectOfType<RoomManager>();

        if (roomManager.player1 == true)
        {
            PhotonNetwork.Instantiate(LiftPlayerPrefab.name, LiftPlayerSpawn, Quaternion.identity);
        }

        if (roomManager.player2 == true)
        {
            PhotonNetwork.Instantiate(PlayerPrefab.name, PlayerSpawn, Quaternion.identity);
        }

        if (roomManager.player3 == true)
        {
            PhotonNetwork.Instantiate(PlayerPrefab.name, PlayerSpawn, Quaternion.identity);
        }
    }
}