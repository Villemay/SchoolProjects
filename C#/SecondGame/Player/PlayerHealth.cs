using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth;
    [SerializeField] HealthBar healthBar;
    [Header("Heal %")]
    [SerializeField] float healAmount;
    [Header("Block Chance %")]
    [SerializeField] int blockChance;
    [SerializeField] GameObject floatingBlockText;

    GameObject gameInfo;
    Animator animator;
    [SerializeField] GameObject gameTimer;

    [Header("Sounds")]
    [SerializeField]
    [FMODUnity.EventRef]
    private string deathJingle;




    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        gameInfo = GameObject.Find("GameInfo");
        gameTimer = GameObject.Find("GameTimer");
        healthBar.SetMaxHealth(maxHealth);

        StartCoroutine(lateStart());
    }

    public void TakeDamage(int damageAmount)
    {
        int blockNum = Random.Range(1, 100);
        if (blockNum <= blockChance)
        {
            GameObject blockText = Instantiate(floatingBlockText, transform.position, Quaternion.identity);

        }
        else
        {
            currentHealth -= damageAmount;
            healthBar.SetHealth(currentHealth);
            gameInfo.GetComponent<GameInfo>().hp(currentHealth);

            //Die
            if (currentHealth <= 0)
            {
                animator.SetTrigger("Death");

                GameObject.Find("BackgroundMusic").GetComponent<BackgroundMusic>().StopASoundWithFade();

                DeactiveComponents();

                Destroy(gameTimer);

                FMODUnity.RuntimeManager.PlayOneShot(deathJingle);


                currentHealth = 0;

                Destroy(gameObject, 4f);
            }
        }
    }

    public void Heal()
    {
        float heal = maxHealth * healAmount;

        currentHealth = gameInfo.GetComponent<GameInfo>().getHp() + (int)heal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
        }
        else
        {
            healthBar.SetHealth(currentHealth);
        }

        setCurrentHealth(gameInfo.GetComponent<GameInfo>().getHp() + (int)heal);
        gameInfo.GetComponent<GameInfo>().hp(currentHealth);
    }

    private void DeactiveComponents()
    {
        if (GetComponent<PlayerUpgrades>().GetRanged())
        {
            GetComponent<PlayerShoot>().enabled = false;
        }
        else if (!GetComponent<PlayerUpgrades>().GetRanged())
        {
            GetComponent<PlayerMelee>().enabled = false;
        }

        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerDash>().enabled = false;

        //Block melee enemy from hitting player. (Normally melee enemy attacks check if they hit gameobject that is on layer "Player". Now change it to default.
        gameObject.layer = 0;
        gameObject.tag = "Untagged";
    }

    public void setBlock(int add)
    {
        blockChance += add;
    }

    public void addMaxHealth(int add)
    {
        maxHealth += add;
        healthBar.SetMaxHealth(maxHealth);

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            currentHealth += add;
        }
        healthBar.SetHealth(currentHealth);
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }
    public void setCurrentHealth(int setHealth)
    {
        if (setHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth = setHealth;
        }
            
        healthBar.SetHealth(currentHealth);
    }

    IEnumerator lateStart()
    {
        yield return new WaitForSeconds(0.5f);

        setCurrentHealth(gameInfo.GetComponent<GameInfo>().getHp());
    }
}
