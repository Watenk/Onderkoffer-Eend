using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnEnemys : MonoBehaviour
{
    public GameObject witLoof;
    public Vector3 witfLofSpawn = new(400, 0, 500);

    private void Start()
    {
        PhotonNetwork.Instantiate(witLoof.name, witfLofSpawn, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
