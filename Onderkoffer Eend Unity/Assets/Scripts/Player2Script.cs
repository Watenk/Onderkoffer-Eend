using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player2Script : MonoBehaviour
{
    public bool isDead = false;
    public float speed;

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
        goToGen = FindObjectOfType<FindGoToGen>().gameObject;
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
            if (Input.GetKeyDown("f"))
            {
                if (zaklampGedimt == false)
                {
                    zaklamp.intensity = 10;
                    enemy.player2ViewDistance = 30f;
                    zaklampGedimt = true;
                }
                else
                {
                    zaklamp.intensity = 40;
                    enemy.player2ViewDistance = 30f;
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

            if (healCollider.player2Colliding == true)
            {
                isDead = false;
                zaklamp.intensity = 40;
            }

            if (isDead == true)
            {
                zaklamp.intensity = 0;
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
}
