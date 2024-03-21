using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int hp;
    [SerializeField] private int dmg;
    [SerializeField] private float critChance;
    [SerializeField] private int defence;
    [SerializeField] private int speed;
    [Header("injections")]
    [SerializeField] private PlayerHealth hpAndDefence;
    [SerializeField] private PlayerShoot shoot;
    [SerializeField] private PlayerMelee melee;
    [SerializeField] private PlayerMovement movevent;
    [Header("Floppydisks")]

    [SerializeField] private bool hpFloppy;
    [SerializeField] private bool dmgFloppy;
    [SerializeField] private bool critFloppy;
    [SerializeField] private bool defFloppy;
    [SerializeField] private bool speedFloppy;
    [SerializeField] private bool allFloppyDisksCollected;


    [Header("Range/melee")]
    [SerializeField] private bool ranged;

    [Header("Tutorial Triggers")]
    [SerializeField] private bool tutorialLevelPassed = false;
    [SerializeField] private bool firstDeath = false;
    [SerializeField] private bool firstTimeInGarage = true;
    [SerializeField] private bool deathCheck = false;





    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Save")
        {
            savePlayer();
        }
        if(collision.tag == "Load")
        {
            loadPlayer();
        }
    }

    public void savePlayer()
    {
        SaveLoad.SavePlayer(this);
    }

    public void loadPlayer()
    {
        PlayerData data = SaveLoad.loadPlayer();
        if (data != null)
        {
            hp = data.GetHp();
            dmg = data.GetDmg();
            critChance = data.GetCritChance();
            defence = data.GetDefence();
            speed = data.GetSpeed();

            hpFloppy = data.GetHpFloppy();
            dmgFloppy = data.GetDmgFloppy();
            critFloppy = data.GetCritFloppy();
            defFloppy = data.GetDefFloppy();
            speedFloppy = data.GetSpeedFloppy();
            allFloppyDisksCollected = data.GetAllFloppys();
            tutorialLevelPassed = data.GetTutorialLevelPassed();
            firstDeath = data.GetFirstDeath();
            firstTimeInGarage = data.GetFirstTimeInGarage();
            deathCheck = data.GetDeathCheck();

            

            ranged = data.GetRanged();
            
        }
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

    public void RefreshPermUpgrades()
    {
        hpAndDefence.addMaxHealth(hp);
        shoot.projectileDamage += dmg;
        melee.playerMeleeDamage += dmg;
        shoot.criticalHitChance += critChance;
        melee.criticalHitChance += critChance;
        hpAndDefence.setBlock(defence);
        movevent.setSpeed(speed);
    }

    public bool GetFloppyCollection()
    {
        return allFloppyDisksCollected;
    }

    public void SetFloppyCollection(bool setter)
    {
        allFloppyDisksCollected = setter;
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
    public void SetRanged(bool setter)
    {
        ranged = setter;
    }
    public void SetHpFloppy(bool setter)
    {
        hpFloppy = setter;
    }
    public void SetDmgFloppy(bool setter)
    {
        dmgFloppy = setter;
    }
    public void SetCritFloppy(bool setter)
    {
        critFloppy = setter;
    }
    public void SetDefFloppy(bool setter)
    {
        defFloppy = setter;
    }
    public void SetSpeedFloppy(bool setter)
    {
        speedFloppy = setter;
    }
    public void setHp(int addedHp)
    {
        hp = addedHp;
    }
    public void setDmg(int addedDmg)
    {
        dmg = addedDmg;
    }
    public void setCrit(float addedCrit)
    {
        critChance = addedCrit;
    }
    public void setDef(int addedDef)
    {
        defence = addedDef;
    }
    public void setSpeed(int addedSpeed)
    {
        speed = addedSpeed;
    }

    public bool GetTutorialLevelPassed()
    {
        return tutorialLevelPassed;
    }
    public bool GetFirstDeath()
    {
        return firstDeath;
    }
    public void SetTutorialLevelPassed(bool setter)
    {
        tutorialLevelPassed = setter;
    }
    public void SetFirstDeath(bool setter)
    {
        firstDeath = setter;
    }
    public bool GetFirstTimeInGarage()
    {
        return firstTimeInGarage;
    }
    public bool GetDeathCheck()
    {
        return deathCheck;
    }
    public void SetFirstTimeInGarage(bool setter)
    {
        firstTimeInGarage = setter;
    }
    public void SetDeathCheck(bool setter)
    {
        deathCheck = setter;
    }



}
