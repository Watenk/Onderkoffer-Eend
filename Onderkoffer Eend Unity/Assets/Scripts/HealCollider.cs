using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCollider : MonoBehaviour
{
    public bool player1Colliding = false;
    public bool player2Colliding = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            player1Colliding = true;
        }

        if (other.CompareTag("Player2"))
        {
            player2Colliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            player1Colliding = false;
        }

        if (other.CompareTag("Player2"))
        {
            player2Colliding = false;
        }
    }
}