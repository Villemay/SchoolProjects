using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    private int hp;
    private int dmg;
    private float critChance;
    private int defence;
    private int speed;
    private bool hpFloppy;
    private bool dmgFloppy;
    private bool critFloppy;
    private bool defFloppy;
    private bool speedFloppy;
    private bool ranged;
    private bool allFloppys;
    private bool tutorialLevelPassed;
    private bool firstDeath;
    private bool firstTimeInGarage;
    private bool deathCheck;


    public PlayerData(PlayerUpgrades player)
    {
        hp = player.GetHp();
        dmg = player.GetDmg();
        critChance = player.GetCritChance();
        defence = player.GetDefence();
        speed = player.GetSpeed();
        hpFloppy = player.GetHpFloppy();
        dmgFloppy = player.GetDmgFloppy();
        critFloppy = player.GetCritFloppy();
        defFloppy = player.GetDefFloppy();
        speedFloppy = player.GetSpeedFloppy();
        ranged = player.GetRanged();
        allFloppys = player.GetFloppyCollection();
        tutorialLevelPassed = player.GetTutorialLevelPassed();
        firstDeath = player.GetFirstDeath();
        firstTimeInGarage = player.GetFirstTimeInGarage();
        deathCheck = player.GetDeathCheck();
    }

    public int GetHp()
    {
        return hp;
    }
    public int GetDmg()
    {
        return dmg;
    }
    public float GetCritChance()
    {
        return critChance;
    }
    public int GetDefence()
    {
        return defence;
    }
    public int GetSpeed()
    {
        return speed;
    }
    public bool GetHpFloppy()
    {
        return hpFloppy;
    }
    public bool GetDmgFloppy()
    {
        return dmgFloppy;
    }
    public bool GetCritFloppy()
    {
        return critFloppy;
    }
    public bool GetDefFloppy()
    {
        return defFloppy;
    }
    public bool GetSpeedFloppy()
    {
        return speedFloppy;
    }
    public bool GetRanged()
    {
        return ranged;
    }
    public bool GetAllFloppys()
    {
        return allFloppys;
    }
    public bool GetTutorialLevelPassed()
    {
        return tutorialLevelPassed;
    }
    public bool GetFirstDeath()
    {
        return firstDeath;
    }
    public bool GetFirstTimeInGarage()
    {
        return firstTimeInGarage;
    }
    public bool GetDeathCheck()
    {
        return deathCheck;
    }

}
