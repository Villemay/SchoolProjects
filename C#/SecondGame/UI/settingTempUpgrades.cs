using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingTempUpgrades : MonoBehaviour
{
    GameObject gameInfo;
    private GameObject _player;

    bool isCrit = false;
    bool isDmg = false;
    bool isSpeed = false;
    bool isBlock = false;
    bool isDash = false;

    [SerializeField]
    GameObject crit;
    [SerializeField]
    GameObject dmg;
    [SerializeField]
    GameObject speed;
    [SerializeField]
    GameObject block;
    [SerializeField]
    GameObject dash;
    [SerializeField]
    GameObject ranged;
    [SerializeField]
    GameObject melee;
    [SerializeField]
    GameObject rangedUlti;
    [SerializeField]
    GameObject meleeUlti;

    [Header("Upgrade values float")]
    [SerializeField]
    float _addCritChance;
    [SerializeField]
    float _addSpeed;
    [Header("Upgrade values int")]
    [SerializeField]
    int _addBlockChance;
    [SerializeField]
    int _addDamage;

    private bool isMelee = false;
    private bool isRanged = false;

    // Start is called before the first frame update
    void Start()
    {
        gameInfo = GameObject.Find("GameInfo");

        getIcons(0.5f);
    }

    public void getIcons(float aika)
    {

        StartCoroutine(LateStart(aika));
    }
    
    IEnumerator LateStart(float aika)
    {
        yield return new WaitForSeconds(aika);

        int i = 0;
        if (gameInfo.GetComponent<GameInfo>().getRangedUlti() == true)
        {
            upgradeRangedUlti();
            rangedUlti.SetActive(true);
        }
        else if (gameInfo.GetComponent<GameInfo>().getMeleeUlti() == true)
        {
            upgradeMeleeUlti();
            meleeUlti.SetActive(true);
        }

        if (_player.GetComponent<PlayerUpgrades>().GetRanged() == true)
        {
            ranged.SetActive(true);
        }
        else if (_player.GetComponent<PlayerUpgrades>().GetRanged() == false)
        {
            melee.SetActive(true);
        }


        if (gameInfo.GetComponent<GameInfo>().getCrit() == true)
        {
            crit = Instantiate(crit, new Vector3(0, 0, 0), Quaternion.identity);
            crit.transform.SetParent(this.transform.GetChild(i), false);
            
            if(isCrit == false)
                upgradeCrit();
            isCrit = true;
            i++;
        }
        if (gameInfo.GetComponent<GameInfo>().getDmg() == true)
        {
            dmg = Instantiate(dmg, new Vector3(0, 0, 0), Quaternion.identity);
            dmg.transform.SetParent(this.transform.GetChild(i), false);

            if (isDmg == false)
                upgradeDamage();
            isDmg = true;
            i++;
        }
        if (gameInfo.GetComponent<GameInfo>().getSpeed() == true)
        {
            speed = Instantiate(speed, new Vector3(0, 0, 0), Quaternion.identity);
            speed.transform.SetParent(this.transform.GetChild(i), false);

            if (isSpeed == false)
                upgradeSpeed();
            isSpeed = true;
            i++;
        }
        if (gameInfo.GetComponent<GameInfo>().getBlock() == true)
        {
            block = Instantiate(block, new Vector3(0, 0, 0), Quaternion.identity);
            block.transform.SetParent(this.transform.GetChild(i), false);

            if (isBlock == false)
                upgradeBlock();
            isBlock = true;
            i++;
        }
        if (gameInfo.GetComponent<GameInfo>().getDash() == true)
        {
            dash = Instantiate(dash, new Vector3(0, 0, 0), Quaternion.identity);
            dash.transform.SetParent(this.transform.GetChild(i), false);

            if (isDash == false)
                upgradeDash();
            isDash = true;
            i++;
        }
    }

    private void upgradeCrit()
    {
        for(int i = 0; i< gameInfo.GetComponent<GameInfo>().getCritCount(); i++)
        {
            if (isRanged == true)
            {
                _player.GetComponent<PlayerShoot>().setRangedCrit(_addCritChance);
            }
            else
            {
                _player.GetComponent<PlayerMelee>().setMeleeCrit(_addCritChance);
            }
        }
    }

    private void upgradeDamage()
    {
        for (int i = 0; i < gameInfo.GetComponent<GameInfo>().getDmgCount(); i++)
        {
            if (isRanged == true)
            {
                _player.GetComponent<PlayerShoot>().setRangeDamage(_addDamage);
            }
            else
            {
                _player.GetComponent<PlayerMelee>().setDamage(_addDamage);
            }
        }
    }

    private void upgradeSpeed()
    {
        for (int i = 1; i < gameInfo.GetComponent<GameInfo>().getSpeedCount(); i++)
        {
            _addSpeed += _addSpeed;
        }
        Debug.Log(_player.GetComponent<PlayerMovement>().moveSpeed);

        _player.GetComponent<PlayerMovement>().setSpeed(_addSpeed);

        if (isRanged == true)
        {
            _player.GetComponent<PlayerShoot>().setSpeed(_addSpeed);
        }
        else
        {
            _player.GetComponent<PlayerMelee>().setSpeed(_addSpeed);
        }
    }

    private void upgradeBlock()
    {
        for (int i = 0; i < gameInfo.GetComponent<GameInfo>().getBlockCount(); i++)
        {
            _player.GetComponent<PlayerHealth>().setBlock(_addBlockChance);
        }
    }

    private void upgradeDash()
    {
        _player.GetComponent<PlayerDash>().setDoubleDash();

    }

    private void upgradeMeleeUlti()
    {
        _player.GetComponent<PlayerMelee>().setMeleeUlti(true);
        GameObject.FindGameObjectWithTag("UltimateTimer").SetActive(true);

    }

    private void upgradeRangedUlti()
    {
        _player.GetComponent<PlayerShoot>().setRangedUlti(true);
        GameObject.FindGameObjectWithTag("UltimateTimer").SetActive(true);

    }

    public void SetPlayer(GameObject player)
    {
        _player = player;
    }
    public void SetRanged(bool ranged)
    {
        if (ranged == true)
        {
            isRanged = true;
        }
        else
        {
            isRanged = false;
        }
    }

}
