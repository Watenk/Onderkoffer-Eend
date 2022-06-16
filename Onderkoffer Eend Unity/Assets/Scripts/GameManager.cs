using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public bool[] playerSpawned = new bool[3];
    public int localPlayerNumber;

    public GameObject loadingScreen;
    public GameObject spawnEnemys;
    public GameObject cabinController;

    private SpawnPlayers spawnPlayers;

    private bool allPlayersSpawned = false;
    private bool loadingDone = false;

    private void Start()
    {
        spawnPlayers = FindObjectOfType<SpawnPlayers>();
    }

    private void Update()
    {
        if (playerSpawned[0] == true && playerSpawned[1] == true && playerSpawned[2] == true)
        {
            allPlayersSpawned = true;
        }

        if (allPlayersSpawned == true && loadingDone == false)
        {
            spawnPlayers.gameObject.SetActive(false);
            if (PhotonNetwork.IsMasterClient)
            {
                spawnEnemys.SetActive(true);
            }
            cabinController.SetActive(true);
            loadingScreen.SetActive(false);
            loadingDone = true;
        }
    }
}