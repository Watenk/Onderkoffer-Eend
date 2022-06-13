using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinController : MonoBehaviour
{
    //References
    private FindCabin cabin;
    private FindSkiPlayer player;

    public float cabinSpeed;

    void Start()
    {
        //References
        cabin = FindObjectOfType<FindCabin>();
        player = FindObjectOfType<FindSkiPlayer>();
    }

    public void GoUp()
    {
        cabin.transform.position = new Vector3(cabin.transform.position.x , cabin.transform.position.y, cabin.transform.position.z + (cabinSpeed * Time.deltaTime));
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + (cabinSpeed * Time.deltaTime));
    }

    public void GoDown()
    {
        cabin.transform.position = new Vector3(cabin.transform.position.x, cabin.transform.position.y, cabin.transform.position.z - (cabinSpeed * Time.deltaTime));
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - (cabinSpeed * Time.deltaTime));
    }
}
