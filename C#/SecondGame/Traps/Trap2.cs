using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap2 : MonoBehaviour
{
    [SerializeField] GameObject vfxEffect;
    [SerializeField] GameObject cdTimer;
    [SerializeField] GameObject zapTimer;
    [SerializeField] bool timertriggered = false;
    [SerializeField] bool zappingStarted = false;
    [SerializeField] GameObject dmgTrigger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timertriggered == true)
        {
            if (cdTimer.gameObject.GetComponentInChildren<Timer>().isReady() == false)
            {

            }
            else
            {
                if (zappingStarted == false)
                {
                    if (dmgTrigger.GetComponent<trapDamage>().getCanTakeDmg() == true)
                    {
                        vfxEffect.SetActive(true);
                    }
                    zappingStarted = true;
                    zapTimer.gameObject.GetComponentInChildren<Timer>().TimerStart();
                }
                else if (zapTimer.gameObject.GetComponentInChildren<Timer>().isReady() == false)
                {
                    //this.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
                }
                else
                {
                    zappingStarted = false;
                    timertriggered = false;
                    
                }
            }
        }
        else
        {
            //this.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
            vfxEffect.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (cdTimer.gameObject.GetComponentInChildren<Timer>().isReady() == true)
            {
                cdTimer.gameObject.GetComponentInChildren<Timer>().TimerStart();
                timertriggered = true;
                dmgTrigger.GetComponent<trapDamage>().setCanTakeDmg(true);
            }
        }
    }
}
