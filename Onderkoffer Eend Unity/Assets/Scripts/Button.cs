using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Button : MonoBehaviour
{
    PhotonView view;
    public Light pointLight;
    public bool buttonEnabled = false;
    private GameManager gameManager;

    private void Start()
    {
        view = gameObject.GetComponent<PhotonView>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (buttonEnabled == false)
        {
            if (other.CompareTag("Player1") || other.CompareTag("Player2"))
            {
                Debug.Log("Button with player!!");
                view.RPC("ButtonEnabled", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    void ButtonEnabled()
    {
        buttonEnabled = true;
        pointLight.color = Color.green;
        pointLight.intensity = 20;
        gameManager.ButtonsLeft -= 1;
        gameObject.SetActive(false);
    }

    [PunRPC]
    void NotDeath()
    {
    }

    [PunRPC]
    void NotDeath1()
    {
    }
}
