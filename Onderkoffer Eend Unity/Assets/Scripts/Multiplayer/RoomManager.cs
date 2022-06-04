using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public byte maxPlayers = 3;

    public GameObject player1Button;
    public GameObject player2Button;
    public GameObject player3Button;

    public GameObject hostButton;
    public GameObject joinButton;

    public GameObject choosePlayer;
    public GameObject chooseGame;

    PhotonView view;

    public bool player1 = false;
    public bool player2 = false;
    public bool player3 = false;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        view = GetComponent<PhotonView>();
    }

    //Choose Player / Lobby-------------------
    public void onPlayerChosen1()
    {
        player1 = true;
        activateHostAndJoinButtons();
    }

    public void onPlayerChosen2()
    {
        player2 = true;
        activateHostAndJoinButtons();
    }

    public void onPlayerChosen3()
    {
        player3 = true;
        activateHostAndJoinButtons();
    }

    private void activateHostAndJoinButtons()
    {
        hostButton.SetActive(true);
        joinButton.SetActive(true);

        player1Button.SetActive(false);
        player2Button.SetActive(false);
        player3Button.SetActive(false);

        choosePlayer.SetActive(false);
        chooseGame.SetActive(true);
    }

    //Rooms------------------------------------------
    public void CreateMainRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayers;
        PhotonNetwork.CreateRoom("MainRoom", roomOptions);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("MainRoom");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("MainScene");
    }
}
