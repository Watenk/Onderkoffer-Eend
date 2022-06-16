using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class Enemy : MonoBehaviour
{
    //Switch
    public enum State { Patrol, Follow, Kill }
    public State state;

    //Agent
    public float viewDistance = 50f;
    private readonly float killDistance = 2f;
    private Transform[] Points = new Transform[9];
   
    private int Point = 0;
    private NavMeshAgent Agent;
    private FindRoute1 route1;

    //Players
    GameObject closestPlayer;
    GameObject player1;
    GameObject player2;
    float DistancePlayer1;
    float DistancePlayer2;

    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        player1 = FindObjectOfType<Player1Script>().gameObject;
        player2 = FindObjectOfType<Player2Script>().gameObject;
        route1 = FindObjectOfType<FindRoute1>();

        for (int i = 0; i < Points.Length; i++)
        {
            Points[i] = route1.transform.GetChild(i);
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

        if (DistancePlayer1 < DistancePlayer2)
        {
            closestPlayer = player1;
        }

        if (DistancePlayer2 < DistancePlayer1)
        {
            closestPlayer = player2;
        }

        //Patrol Or Follow

        if (Vector3.Distance(transform.position, closestPlayer.transform.position) < viewDistance)
        {
            state = State.Follow;
        }

        if (Vector3.Distance(transform.position, closestPlayer.transform.position) > viewDistance)
        {
            state = State.Patrol;
        }

        //Kill
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
    }

    void Follow()
    {
        //Follows player in viewdistance

        Agent.destination = closestPlayer.transform.position;
    }

    void Kill()
    {
        closestPlayer.transform.position = new Vector3(2, 0, 2);
    }
}