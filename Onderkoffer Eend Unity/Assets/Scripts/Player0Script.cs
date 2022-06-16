using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player0Script : MonoBehaviour
{
    public float speed;

    Rigidbody rb;
    PhotonView view;
    Camera cam;
    GameManager gameManager;
    Enemy enemy;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        view = GetComponent<PhotonView>();
        cam = transform.GetChild(0).GetComponent<Camera>();
        gameManager = FindObjectOfType<GameManager>();
        enemy = FindObjectOfType<Enemy>();

        if (view.IsMine)
        {
            cam.targetDisplay = 0;
        }
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Camera.main.fieldOfView--;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Camera.main.fieldOfView++;
        }
    }

    void FixedUpdate()
    {
        if (view.IsMine && rb.velocity.magnitude <= speed)
        {
            if (Input.GetKey("w"))
            {
                rb.AddForce(transform.forward * speed);
            }
            if (Input.GetKey("d"))
            {
                rb.AddForce(transform.right * speed);
            }
            if (Input.GetKey("s"))
            {
                rb.AddForce((transform.forward * -1) * speed);
            }
            if (Input.GetKey("a"))
            {
                rb.AddForce((transform.right * -1) * speed);
            }
        }
    }
}
