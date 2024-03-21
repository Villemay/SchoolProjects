using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GODMODE : MonoBehaviour
{
    private bool inTrigger = false;
    private GameObject player;
    [SerializeField] private InputManager iM;
    [SerializeField] private GameObject text;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L) && inTrigger == true)
        {
            if (player.GetComponent<PlayerUpgrades>().GetDefence() == 0)
            {
                player.GetComponent<PlayerUpgrades>().setDef(100);
                text.SetActive(true);

            }
            else
            {
                player.GetComponent<PlayerUpgrades>().setDef(0);
                text.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            inTrigger = true;
            player = collider.gameObject;
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            inTrigger = false;
        }
    }

}
