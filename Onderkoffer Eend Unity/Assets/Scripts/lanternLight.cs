using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lanternLight : MonoBehaviour
{
    private GameManager gameManager;
    private Light pointLight;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        pointLight = gameObject.transform.GetChild(2).GetComponent<Light>();
    }

    void Update()
    {
        if (gameManager.lightTimer <= 0)
        {
            pointLight.intensity = 0.5f;
        }
    }
}
