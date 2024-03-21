using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] GameObject cdTimer;
    [SerializeField] GameObject zapTimer;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cdTimer.gameObject.GetComponentInChildren<Timer>().isReady() == true)
        {
            zapTimer.gameObject.GetComponentInChildren<Timer>().TimerStart();
            cdTimer.gameObject.GetComponentInChildren<Timer>().TimerStart();
            this.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        else if(zapTimer.gameObject.GetComponentInChildren<Timer>().isReady() == true)
        {
            this.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }
}
