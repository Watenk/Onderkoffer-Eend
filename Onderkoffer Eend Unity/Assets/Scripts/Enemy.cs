using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class Enemy : MonoBehaviour
{
    //Switch
    public enum State { Patrol, Follow, Attack }
    public State state;

    //Agent
    public float ViewDistance = 10f;
    public Transform[] Points;
   
    private int Point = 0;
    private NavMeshAgent Agent;

    private PlayerScript Player;
    private GameManager gameManager;
    private SpawnPlayers spawnPlayers;

    //Players
    GameObject closestPlayer;
    GameObject player1;
    GameObject player2;
    bool assignedPlayers = false;

    void Start()
    {
        spawnPlayers = FindObjectOfType<SpawnPlayers>();
        Player = FindObjectOfType<PlayerScript>();
        gameManager = FindObjectOfType<GameManager>();
        Agent = GetComponent<NavMeshAgent>();
        Agent.destination = Points[Point].position;
    }

    void Update()
    {
        if (spawnPlayers.allPlayersSpawned == true && assignedPlayers == false)
        {
            player1 = FindObjectOfType<FindPlayer1>().gameObject;
            player2 = FindObjectOfType<FindPlayer2>().gameObject;
            assignedPlayers = true;
        }

        switch (state)
        {
            case State.Patrol:
                Patrol();
                break;

            case State.Follow:
                Follow();
                break;

            case State.Attack:
                //Attack();
                break;
        }

        //Update PlayerLocations

        float DistancePlayer1 = Vector3.Distance(transform.position, player1.transform.position);
        float DistancePlayer2 = Vector3.Distance(transform.position, player2.transform.position);

        if (DistancePlayer1 < DistancePlayer2)
        {
            closestPlayer = player1;
        }

        if (DistancePlayer2 < DistancePlayer1)
        {
            closestPlayer = player2;
        }

        if (Vector3.Distance(transform.position, closestPlayer.transform.position) < ViewDistance)
        {
            state = State.Follow;
        }

        if (Vector3.Distance(transform.position, closestPlayer.transform.position) > ViewDistance)
        {
            state = State.Patrol;
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
    }

    void Follow()
    {
        //Follows player in viewdistance

        Agent.destination = closestPlayer.transform.position;
    }
}