using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject LiftPlayerPrefab;
    public GameObject Player1Prefab;
    public GameObject Player2Prefab;
    public GameObject LiftLight;
    public GameObject goggles;

    private GameManager gameManager;
    private PhotonView view;

    public Vector3 LiftPlayerSpawn = new(500, 125 ,190);
    public Vector3 PlayerSpawn = new(400, 0, 300);

    private bool spawnedPlayerLocal = false;
    public bool allPlayersSpawned = false;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        view = FindObjectOfType<PhotonView>();

        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(LiftPlayerPrefab.name, LiftPlayerSpawn, Quaternion.identity);
            int playerAmount = 0;
            view.RPC("AddPlayer", RpcTarget.All, playerAmount);
            LiftLight.SetActive(true);
            goggles.SetActive(true);
            spawnedPlayerLocal = true;
        }

        if (gameManager.playerArray[1] == false && gameManager.playerArray[0] == true && spawnedPlayerLocal == false)
        {
            PhotonNetwork.Instantiate(Player1Prefab.name, PlayerSpawn, Quaternion.identity);
            int playerAmount = 1;
            view.RPC("AddPlayer", RpcTarget.All, playerAmount);
            spawnedPlayerLocal = true;
        }

        if (gameManager.playerArray[2] == false && gameManager.playerArray[0] == true && gameManager.playerArray[1] == true && spawnedPlayerLocal == false)
        {
            PhotonNetwork.Instantiate(Player2Prefab.name, PlayerSpawn, Quaternion.identity);
            int playerAmount = 2;
            view.RPC("AddPlayer", RpcTarget.All, playerAmount);
            spawnedPlayerLocal = true;
            allPlayersSpawned = true;
        }
    }

    [PunRPC]
    public void AddPlayer(int player)
    {
        gameManager.playerArray[player] = true;
        SpawnPlayer();
    }
}