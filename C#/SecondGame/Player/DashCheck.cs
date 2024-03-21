using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashCheck : MonoBehaviour
{
    private bool canDash = true;
    //function that lets the script know player cant dash
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            canDash = false;
        }
    }
    //function that return if the player can dash
    public bool CanDash()
    {
        return canDash;
    }
    //function that clets the script know player can dash
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            canDash = true;
        }
    }


}
