using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinController : MonoBehaviour
{
    //References
    private FindCabin cabin;
    private Player0Script player0;

    public float cabinSpeed;

    void Start()
    {
        //References
        cabin = FindObjectOfType<FindCabin>();
        player0 = FindObjectOfType<Player0Script>();
    }

    public void GoFoward()
    {
        cabin.transform.position = new Vector3(cabin.transform.position.x , cabin.transform.position.y, cabin.transform.position.z + (cabinSpeed * Time.deltaTime));
        player0.transform.position = new Vector3(player0.transform.position.x, player0.transform.position.y, player0.transform.position.z + (cabinSpeed * Time.deltaTime));
    }

    public void GoBackwards()
    {
        cabin.transform.position = new Vector3(cabin.transform.position.x, cabin.transform.position.y, cabin.transform.position.z - (cabinSpeed * Time.deltaTime));
        player0.transform.position = new Vector3(player0.transform.position.x, player0.transform.position.y, player0.transform.position.z - (cabinSpeed * Time.deltaTime));
    }
}
