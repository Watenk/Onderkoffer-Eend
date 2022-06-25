using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnEnemys : MonoBehaviour
{
    public Vector3 witfLofSpawn1 = new(400, 0, 500);
    public Vector3 witfLofSpawn2 = new(450, 0, 450);

    public GameObject enemy1;
    public GameObject enemy2;

    private void Start()
    {
        PhotonNetwork.Instantiate(enemy1.name, witfLofSpawn1, Quaternion.identity);
        PhotonNetwork.Instantiate(enemy2.name, witfLofSpawn2, Quaternion.identity);
    }
}
