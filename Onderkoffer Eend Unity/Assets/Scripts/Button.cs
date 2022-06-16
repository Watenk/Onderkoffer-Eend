using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

public class Button : MonoBehaviour
{
    public UnityEvent unityEvent = new UnityEvent();

    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Input.GetMouseButton(0))
            {
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
                {
                    unityEvent.Invoke();
                }
            }
        }
    }
}
