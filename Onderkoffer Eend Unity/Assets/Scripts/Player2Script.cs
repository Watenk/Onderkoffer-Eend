using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player2Script : MonoBehaviour
{
    public float speed;

    private bool zaklampGedimt = false;

    Rigidbody rb;
    PhotonView view;
    Camera cam;
    Light zaklamp;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        view = GetComponent<PhotonView>();
        cam = transform.GetChild(0).GetComponent<Camera>();
        zaklamp = gameObject.transform.GetChild(2).GetComponent<Light>();

        print(zaklamp);

        if (view.IsMine)
        {
            cam.targetDisplay = 0;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            if (zaklampGedimt == false)
            {
                zaklamp.intensity = 10;
                zaklampGedimt = true;
            }
            else
            {
                zaklamp.intensity = 40;
                zaklampGedimt = false;
            }
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
