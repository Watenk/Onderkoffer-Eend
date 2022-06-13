using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public byte maxPlayers = 3;

    private void Start()
    {
        if (PhotonNetwork.CountOfRooms <= 0)
        {
            CreateMainRoom();
        }
        else
        {
            JoinRoom();
        }
    }

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
        SceneManager.LoadScene("Lobby");
    }
}
