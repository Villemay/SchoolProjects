using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDash : MonoBehaviour
{
    [SerializeField]
    private Image imageCooldown;
    [SerializeField] GameObject direction;
    [SerializeField] GameObject timer;
    [SerializeField] bool canDoubleDash = false;
    private bool doubleDashed = false;
    public bool isShooting = false;
    [SerializeField] private GameObject dashEffect;

    [SerializeField] private InputManager iM;
    Animator animator;

    [SerializeField]
    [FMODUnity.EventRef]
    private string aDash;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //dash button if pressed and if game is not paused. Prevent using while choosing floppydisk etc or game is paused.
        if ((Input.GetKeyDown(iM.GetDash()) || Input.GetKeyDown(iM.GetSecondaryDash())) && Time.timeScale > 0 && !isShooting) {
            //the dash isn't on cooldown
            if (timer.gameObject.GetComponent<Timer>().isReady() == true)
            {
                //the player can dash forwards
                if (direction.gameObject.GetComponent<DashCheck>().CanDash() == true)
                {
 
                    //resetting the double dash and starting the dash timer and then dashing forwards
                    doubleDashed = false;
                    timer.gameObject.GetComponent<Timer>().TimerStart();
                    Dash();
                }
            }
            //the player can double dash and hasn't double dashed yet
           else if (canDoubleDash == true && doubleDashed == false && !isShooting)
           {
                //the player can dash forwards
                if (direction.gameObject.GetComponent<DashCheck>().CanDash() == true)
                {
                    //setting that the player can't double dash anymore and then dashing forwards

                    doubleDashed = true;
                    Dash();
                }
            }
        }
        ApplyCooldown();
    }

    void ApplyCooldown()
    {
        if (timer.gameObject.GetComponent<Timer>().isReady() == true)
        {
            imageCooldown.fillAmount = 0.0f;
        }
        else
        {
            imageCooldown.fillAmount = 1.0f;
            imageCooldown.fillAmount -= timer.gameObject.GetComponent<Timer>().TimeLeft() / timer.gameObject.GetComponent<Timer>().maxTime();
        }
    }
    //dash the player forwards
    private void Dash()
    {
        animator.SetTrigger("Dash");
        FMODUnity.RuntimeManager.PlayOneShot(aDash);
        Vector3 relativePos = direction.transform.position - transform.position;
        GameObject dashVFX = Instantiate(dashEffect, transform.position, Quaternion.LookRotation(relativePos));
        Destroy(dashVFX, 2f);
        transform.position = direction.transform.position;
    }

    //set double dash for player
    public void setDoubleDash()
    {
        canDoubleDash = true;
    }

}
