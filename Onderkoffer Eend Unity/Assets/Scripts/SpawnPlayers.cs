using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject player0Prefab;
    public GameObject player1Prefab;
    public GameObject player2Prefab;
    public GameObject liftLight;

    public Vector3 player0Spawn = new(500, 125 ,190);
    public Vector3 player1Spawn = new(400, 0, 300);
    public Vector3 player2Spawn = new(400, 0, 320);

    private bool spawnedPlayerLocal = false;

    private GameManager gameManager;
    private PhotonView view;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        view = this.gameObject.GetComponent<PhotonView>();

        if (PhotonNetwork.LocalPlayer == PhotonNetwork.PlayerList[0] && spawnedPlayerLocal == false)
        {
            PhotonNetwork.Instantiate(player0Prefab.name, player0Spawn, Quaternion.identity);
            liftLight.SetActive(true);
            spawnedPlayerLocal = true;
            gameManager.localPlayerNumber = 0;
            view.RPC("PlayerSpawned", RpcTarget.All, 0);
        }

        if (PhotonNetwork.LocalPlayer == PhotonNetwork.PlayerList[1] && spawnedPlayerLocal == false)
        {
            PhotonNetwork.Instantiate(player1Prefab.name, player1Spawn, Quaternion.identity);
            spawnedPlayerLocal = true;
            gameManager.localPlayerNumber = 1;
            view.RPC("PlayerSpawned", RpcTarget.All, 1);
        }

        if (PhotonNetwork.LocalPlayer == PhotonNetwork.PlayerList[2] && spawnedPlayerLocal == false)
        {
            PhotonNetwork.Instantiate(player2Prefab.name, player2Spawn, Quaternion.identity);
            spawnedPlayerLocal = true;
            gameManager.localPlayerNumber = 2;
            view.RPC("PlayerSpawned", RpcTarget.All, 2);
        }
    }

    [PunRPC]
    void PlayerSpawned(int playerNumber)
    {
        gameManager.playerSpawned[playerNumber] = true;
    }
}