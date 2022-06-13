using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class LobbyGameManager : MonoBehaviour
{
    public GameObject startButton;
    public TextMeshProUGUI waitingForHost;
    public PhotonView view;

    private void Start()
    {
        view = FindObjectOfType<PhotonView>();

        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
        }
        else
        {
            waitingForHost.gameObject.SetActive(true);
        }
    }

    public void StartGame()
    {
        view.RPC("LoadMainScene", RpcTarget.All);
    }

    [PunRPC]
    public void LoadMainScene()
    {
        PhotonNetwork.LoadLevel("MainScene");
    }
}
