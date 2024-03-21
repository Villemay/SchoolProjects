using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableUpgrades : MonoBehaviour
{
    [SerializeField] private PlayerUpgrades pU;
    private List<string> list;

    public void CheckAvailableUpgrades()
    {
        //clears the list and adds all the not collected floppydisks to a list
        list.Clear();
        if (pU.GetHpFloppy() == false)
        {
            list.Add("HP");
        }
        if (pU.GetDmgFloppy() == false)
        {
            list.Add("DMG");
        }
        if (pU.GetCritFloppy() == false)
        {
            list.Add("CRIT");
        }
        if (pU.GetDefFloppy() == false)
        {
            list.Add("DEF");
        }
        if(pU.GetSpeedFloppy() == false)
        {
            list.Add("SPEED");
        }
    }
    public void SelectRandomFromList()
    {
        //if the lsit is empty is doens't do anything
        if (list.Count != 0)
        {
            //takes a random item from the list and set the floppydisk to true
            int randomNmb = Random.Range(0, list.Count-1);
            if (list[randomNmb] == "HP")
            {
                pU.SetHpFloppy(true);
            }
            if(list[randomNmb] == "DMG")
            {
                pU.SetDmgFloppy(true);
            }
            if(list[randomNmb] == "CRIT")
            {
                pU.SetCritFloppy(true);
            }
            if(list[randomNmb] == "DEF")
            {
                pU.SetDefFloppy(true);
            }
            if(list[randomNmb] == "SPEED")
            {
                pU.SetSpeedFloppy(true);
            }
            //saves the floppy disk
            pU.savePlayer();
        }
    }
}
