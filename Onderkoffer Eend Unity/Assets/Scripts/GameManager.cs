using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool[] playerSpawned = new bool[3];
    public int localPlayerNumber;

    public int ButtonsLeft = 5;
    public GameObject loadingScreen;
    public GameObject spawnEnemys;
    public GameObject cabinController;
    public GameObject gate;
    public float lightTimerAmount = 500;
    public float lightTimer;
    private int lightTimerInt;

    private FindButtonsLeftCounter findButtonsLeftCounter;
    private TextMeshProUGUI buttonsCounter;
    public TextMeshProUGUI lightTimerUI;
    private SpawnPlayers spawnPlayers;

    private bool allPlayersSpawned = false;
    public bool loadingDone = false;

    private void Start()
    {
        spawnPlayers = FindObjectOfType<SpawnPlayers>();
        findButtonsLeftCounter = FindObjectOfType<FindButtonsLeftCounter>();
        buttonsCounter = findButtonsLeftCounter.GetComponent<TextMeshProUGUI>();
        lightTimer = lightTimerAmount;
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
        }

        if (loadingDone == true)
        {
            loadingScreen.SetActive(false);
        }

        buttonsCounter.text = ButtonsLeft.ToString();

        if (ButtonsLeft <= 0)
        {
            gate.SetActive(false);
        }

        if (lightTimer >= 0)
        {
            lightTimer -= Time.deltaTime;
        }
        lightTimerInt = (int) lightTimer;
        lightTimerUI.text = lightTimerInt.ToString();
    }
}