using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using Photon.Pun;

public class Enemy : MonoBehaviour
{
    public int enemyNumber = 0;

    //Switch
    public enum State { Patrol, Follow, Kill }
    public State state;

    //Agent
    public float player1ViewDistance = 50f;
    public float player2ViewDistance = 50f;
    private readonly float killDistance = 5f;
    private Transform[] Points = new Transform[9];

    private int Point = 0;
    private NavMeshAgent Agent;
    private FindRoute1 route1;
    private FindRoute2 route2;
    private GameManager gameManager;
    private Animator animator;

    private AudioSource grom;
    private bool gromPlayed = false;

    //Players
    GameObject closestPlayer;
    GameObject player1;
    GameObject player2;
    Player1Script player1Script;
    Player2Script player2Script;
    float DistancePlayer1;
    float DistancePlayer2;
    float closestPlayerDistance;

    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        player1 = FindObjectOfType<Player1Script>().gameObject;
        player2 = FindObjectOfType<Player2Script>().gameObject;
        player1Script = FindObjectOfType<Player1Script>();
        player2Script = FindObjectOfType<Player2Script>();
        route1 = FindObjectOfType<FindRoute1>();
        route2 = FindObjectOfType<FindRoute2>();
        gameManager = FindObjectOfType<GameManager>();
        grom = FindObjectOfType<AudioSource>();
        animator = FindObjectOfType<Animator>();

        if (enemyNumber == 1)
        {
            for (int i = 0; i < Points.Length; i++)
            {
                Points[i] = route1.transform.GetChild(i);
            }
            gameManager.loadingDone = true;
        }

        if (enemyNumber == 2)
        {
            for (int i = 0; i < Points.Length; i++)
            {
                Points[i] = route2.transform.GetChild(i);
            }
            gameManager.loadingDone = true;
        }
    }

    void Update()
    {
        switch (state)
        {
            case State.Patrol:
                Patrol();
                break;

            case State.Follow:
                Follow();
                break;

            case State.Kill:
                Kill();
                break;
        }

        //Update PlayerLocations
        DistancePlayer1 = Vector3.Distance(transform.position, player1.transform.position);
        DistancePlayer2 = Vector3.Distance(transform.position, player2.transform.position);

        if (player1Script.isDead == true)
        {
            DistancePlayer1 = 1000;
        }

        if (player2Script.isDead == true)
        {
            DistancePlayer2 = 1000;
        }

        if (DistancePlayer1 < DistancePlayer2)
        {
            closestPlayer = player1;
        }

        if (DistancePlayer2 < DistancePlayer1)
        {
            closestPlayer = player2;
        }

        closestPlayerDistance = Vector3.Distance(transform.position, closestPlayer.transform.position);


        //Patrol, Follow or Kill
        if (closestPlayerDistance > player1ViewDistance || closestPlayerDistance > player2ViewDistance)
        {
            state = State.Patrol;
        }

        if (closestPlayerDistance < player1ViewDistance || closestPlayerDistance < player2ViewDistance)
        {
            state = State.Follow;
        }

        if (Vector3.Distance(transform.position, closestPlayer.transform.position) < killDistance)
        {
            state = State.Kill;
        }
    }

    void Patrol()
    {
        //Enemy Patrols from point to point

        if (Agent.remainingDistance <= 0.5 && !Agent.pathPending)
        {
            Point += 1;
        }
        
        if (Point >= (Points.Length))
        {
            Point = 0;
        }

        Agent.destination = Points[Point].position;
        gromPlayed = false;
    }

    void Follow()
    {
        if (!grom.isPlaying && gromPlayed == false)
        {
            grom.Play();
            gromPlayed = true;
        }

        //Follows player in viewdistance
        Agent.destination = closestPlayer.transform.position;
    }

    void Kill()
    {
        if (closestPlayer == player1)
        {
            Player1Script player1Script = player1.GetComponent<Player1Script>();
            player1Script.isDead = true;
        }

        if (closestPlayer == player2)
        {
            Player2Script player2Script = player2.GetComponent<Player2Script>();
            player2Script.isDead = true;
        }
    }
}