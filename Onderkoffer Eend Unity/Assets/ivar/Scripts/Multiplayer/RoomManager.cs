using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public void CreateMainRoom()
    {
        PhotonNetwork.CreateRoom("MainRoom");
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("MainRoom");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("IvarScene");
    }
}
