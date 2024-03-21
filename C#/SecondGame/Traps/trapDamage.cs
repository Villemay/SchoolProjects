using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapDamage : MonoBehaviour
{
    public int damageAmount;
    public GameObject floatingDamage;
    [SerializeField] private bool canTakeDmg = false;

    private void OnTriggerEnter(Collider other)
    {
        if (canTakeDmg == true)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damageAmount);
                GameObject damageText = Instantiate(floatingDamage, other.transform.position, Quaternion.identity);
                damageText.transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = damageAmount.ToString();
                //this.gameObject.SetActive(false);
                canTakeDmg = false;
            }

            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
                //this.gameObject.SetActive(false);
                canTakeDmg = false;
            }
        }
    }


    public void setCanTakeDmg(bool TakeDmg)
    {
        canTakeDmg = TakeDmg;
    }
    public bool getCanTakeDmg()
    {
        return canTakeDmg;
    }
}
