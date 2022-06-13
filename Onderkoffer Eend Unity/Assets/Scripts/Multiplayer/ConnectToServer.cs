using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public GameObject roomManager;

    private void Start()
    {
        //Connect to Photon server
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connected To Photon");
    }

    public override void OnConnectedToMaster()
    {
        //If connected to server join lobby
        PhotonNetwork.JoinLobby();
        Debug.Log("Joined Lobby");
    }

    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("JoinMainRoom");
    }
}