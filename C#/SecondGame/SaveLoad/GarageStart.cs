using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageStart : MonoBehaviour
{
    [SerializeField] private PlayerUpgrades pU;
    [SerializeField] private GarageUpgradesUI garageUI;
    [SerializeField] private GameObject godMode;
    [SerializeField] private GameObject broBot;
    [SerializeField] private GameObject levelEnd;
    [SerializeField] private GameObject controls;
    [SerializeField] private GameObject keybindsUI;


    private void Start()
    {
        levelEnd = GameObject.Find("LevelEnd");
        controls = GameObject.Find("InputManager");
    }



    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {

            if (SaveLoad.loadPlayer() != null)
            {
                pU.loadPlayer();
                SetUIButtons();
                if (pU.GetDefence() == 100)
                {
                    godMode.SetActive(true);
                }
                else
                {
                    godMode.SetActive(false);
                }

                if (pU.GetFirstDeath()==false && pU.GetDeathCheck() == false)
                {
                    pU.SetFirstDeath(true);
                    pU.SetDeathCheck(true);
                    broBot.GetComponent<Brobot>().StartCoroutineFirstDeathLine();
                }
            }
            else
            {
                garageUI.ResetImages();
                broBot.GetComponent<Brobot>().StartCoroutineFirstLine();
            }


            levelEnd.gameObject.GetComponent<LevelEnd>().SetPlayer(collision.gameObject);
            levelEnd.gameObject.GetComponent<LevelEnd>().SetPU(collision.gameObject);
            controls.GetComponent<InputManager>().SetKeys();
            keybindsUI.GetComponent<AssingKeybindsToUI>().Setkeys();
            this.gameObject.SetActive(false);
        }
    }


    private void SetUIButtons()
    {
        if (pU.GetHpFloppy() == true)
        {
            garageUI.SetHpCollected(true);
        }
        if(pU.GetDmgFloppy() == true)
        {
            garageUI.SetDmgCollected(true);
        }
        if (pU.GetCritFloppy() == true)
        {
            garageUI.SetCritCollected(true);
        }
        if (pU.GetDefFloppy() == true)
        {
            garageUI.SetDefCollected(true);
        }
        if (pU.GetSpeedFloppy() == true)
        {
            garageUI.SetSpeedCollected(true);
        }
        garageUI.ResetImages();
        SetLastUsedDisks();
    } 


    private void SetLastUsedDisks()
    {
        if(pU.GetHp() != 0)
        {
            garageUI.HpFloppy();
        }
        if(pU.GetDmg() != 0)
        {
            garageUI.DmgFloppy();
        }
        if(pU.GetCritChance() != 0)
        {
            garageUI.CritFloppy();
        }
        if (pU.GetDefence() == 100)
        {
            //GODMODE
        }
        else if(pU.GetDefence() != 0)
        {
            garageUI.DefFloppy();
        }
        if(pU.GetSpeed() != 0)
        {
            garageUI.SpeedFloppy();
        }
    }

}
