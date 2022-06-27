using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player1Script : MonoBehaviour
{
    public float speed;
    public bool isDead = false;

    private bool zaklampGedimt = false;
    private bool enemyFound = false;
    public int lastPressedKey = 1;

    private Rigidbody rb;
    private PhotonView view;
    private Camera cam;
    private Light zaklamp;
    private Enemy enemy;
    private GameManager gameManager;
    private TerrainDetector terrainDetector;
    private GameObject goToGen;
    private HealCollider healCollider;
    private Animator animator;
    private AudioSource loopSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        view = GetComponent<PhotonView>();
        cam = transform.GetChild(0).GetComponent<Camera>();
        zaklamp = gameObject.transform.GetChild(1).GetComponent<Light>();
        gameManager = FindObjectOfType<GameManager>();
        terrainDetector = new TerrainDetector();
        goToGen = GameObject.Find("GoToGen");
        healCollider = FindObjectOfType<HealCollider>();
        animator = FindObjectOfType<Animator>();
        loopSound = FindObjectOfType<AudioSource>();

        if (view.IsMine)
        {
            goToGen.SetActive(false);
            cam.targetDisplay = 0;
        }
    }

    private void Update()
    {
        if (view.IsMine)
        {
            //Find Enemy
            if (gameManager.loadingDone == true && enemyFound == false)
            {
                enemy = FindObjectOfType<Enemy>();
                enemyFound = true;
            }

            //FlashLight
            if (Input.GetKeyDown("f") && isDead == false)
            {
                if (zaklampGedimt == false)
                {
                    zaklamp.intensity = 10;
                    enemy.player1ViewDistance = 30;
                    zaklampGedimt = true;
                }
                else
                {
                    zaklamp.intensity = 40;
                    enemy.player1ViewDistance = 50;
                    zaklampGedimt = false;
                }
            }

            //Terrain
            int terrain = terrainDetector.GetTerrainTexture(transform.position);

            switch (terrain)
            {
                case 0: //Gravel
                    speed = 15;
                    break;

                case 1: //Grass
                    speed = 10;
                    break;
            }

            //Health
            if (healCollider.player1Colliding == true)
            {
                zaklamp.intensity = 40;
                view.RPC("NotDeath1", RpcTarget.All);
            }

            if (isDead == true)
            {
                zaklamp.intensity = 5;
                goToGen.SetActive(true);
            }

            if (isDead == false)
            {
                goToGen.SetActive(false);
            }

            //Animator
            animator.SetInteger("LastPressedKey", lastPressedKey);

            if (Input.GetKey("w"))
            {
                animator.SetBool("w", true);
                if (!loopSound.isPlaying)
                {
                    loopSound.Play();
                }
            }
            else
            {
                animator.SetBool("w", false);
            }

            if (Input.GetKey("d"))
            {
                animator.SetBool("d", true);
                if (!loopSound.isPlaying)
                {
                    loopSound.Play();
                }
            }
            else
            {
                animator.SetBool("d", false);
            }

            if (Input.GetKey("s"))
            {
                animator.SetBool("s", true);
                if (!loopSound.isPlaying)
                {
                    loopSound.Play();
                }
            }
            else
            {
                animator.SetBool("s", false);
            }

            if (Input.GetKey("a"))
            {
                animator.SetBool("a", true);
                if (!loopSound.isPlaying)
                {
                    loopSound.Play();
                }
            }
            else
            {
                animator.SetBool("a", false);
            }
        }
    }

    void FixedUpdate()
    {
        if (view.IsMine && rb.velocity.magnitude <= speed)
        {
            if (Input.GetKey("w"))
            {
                rb.AddForce(transform.forward * speed, ForceMode.Impulse);
                lastPressedKey = 1;
            }
            if (Input.GetKey("d"))
            {
                rb.AddForce(transform.right * speed, ForceMode.Impulse);
            }
            if (Input.GetKey("s"))
            {
                rb.AddForce((transform.forward * -1) * speed, ForceMode.Impulse);
                lastPressedKey = 2;
            }
            if (Input.GetKey("a"))
            {
                rb.AddForce((transform.right * -1) * speed, ForceMode.Impulse);
            }
        }
    }

    [PunRPC]
    void ButtonEnabled()
    {
    }

    [PunRPC]
    void NotDeath1()
    {
        isDead = false;
    }

    [PunRPC]
    void NotDeath()
    {
    }

}
