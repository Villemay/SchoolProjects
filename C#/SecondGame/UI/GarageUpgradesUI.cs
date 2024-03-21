using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarageUpgradesUI : MonoBehaviour
{
    [SerializeField] private GameObject uI;
    [SerializeField] private PlayerUpgrades pU;
    [SerializeField] private GameObject interact;
    private bool interactable = false;
    [SerializeField] private Animator animator;
    private GameObject firstButton;
    private string secondString;
    private GameObject secondButton;
    private bool opening = false;
    private bool esced = false;

    [Header("Button Images")]
    [SerializeField] private Sprite imgCollected;
    [SerializeField] private Sprite imgActive;

    [Header("Buttons")]
    [SerializeField] private GameObject hpButton;
    [SerializeField] private GameObject dmgButton;
    [SerializeField] private GameObject critButton;
    [SerializeField] private GameObject defButton;
    [SerializeField] private GameObject speedButton;

    [Header("Collected Floppydisks")]
    [SerializeField] private bool hpCollected = false;
    [SerializeField] private bool dmgCollected = false;
    [SerializeField] private bool critCollected = false;
    [SerializeField] private bool defCollected = false;
    [SerializeField] private bool speedCollected = false;


    [Header("Floppydisk Amounts")]
    [SerializeField] private int hpAmount;
    [SerializeField] private int dmgAmount;
    [SerializeField] private float critAmount;
    [SerializeField] private int defAmount;
    [SerializeField] private int speedAmount;

    [Header("Button Amount Texts")]
    [SerializeField] private GameObject hpText;
    [SerializeField] private GameObject dmgText;
    [SerializeField] private GameObject critText;
    [SerializeField] private GameObject defText;
    [SerializeField] private GameObject speedText;
    [SerializeField] private GameObject godmode;

    [Header("Button Images")]
    [SerializeField] private GameObject hpImage;
    [SerializeField] private GameObject dmgImage;
    [SerializeField] private GameObject critImage;
    [SerializeField] private GameObject defImage;
    [SerializeField] private GameObject speedImage;

    [Header("Button Texts")]
    [SerializeField] private GameObject hpButtonText;
    [SerializeField] private GameObject dmgButtonText;
    [SerializeField] private GameObject critButtonText;
    [SerializeField] private GameObject defButtonText;
    [SerializeField] private GameObject speedButtonText;

    [Header("Sounds")]
    [SerializeField]
    [FMODUnity.EventRef]
    private string aMenu;

    [SerializeField]
    [FMODUnity.EventRef]
    private string aOpen;

    [SerializeField]
    [FMODUnity.EventRef]
    private string aClose;

    [SerializeField] private InputManager iM;
    // Start is called before the first frame update
    void Start()
    {
        hpText.GetComponent<Text>().text = "+"+ hpAmount.ToString() + " Health";
        dmgText.GetComponent<Text>().text = "+" + dmgAmount.ToString() + " Damage";
        critText.GetComponent<Text>().text = "+" + (critAmount * 100).ToString() + "%";
        defText.GetComponent<Text>().text = "Increase Defense";
        speedText.GetComponent<Text>().text = "Increase Speed";
    }

    // Update is called once per frame
    void Update()
    {
        if(uI.activeSelf==false && interactable == true && opening==false &&
            (Input.GetKeyDown(iM.GetInteract()) || Input.GetKeyDown(iM.GetSecondaryInteract())))
        {
            ActivateUI();
        }
        if((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7)) && uI.activeSelf == true)
        {
            esced = true;
            DeActivateUI();
        }
        if (esced == true)
        {
            pU.gameObject.GetComponent<GarageMovement>().enabled = false;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            interact.SetActive(true);
            interactable = true;
        }
    }


    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player")
        {
            interact.SetActive(false);
            interactable = false;
        }
    }
    public void ActivateUI()
    {
        StartCoroutine(CloseBooth());

    }
    IEnumerator CloseBooth()
    {
        animator.SetTrigger("CloseUpgradeBooth");
        pU.gameObject.GetComponent<GarageMovement>().enabled = false;
        pU.gameObject.GetComponent<Animator>().SetBool("Run", false);
        opening = true;
        FMODUnity.RuntimeManager.PlayOneShot(aClose);
        yield return new WaitForSeconds(1.5f);

        uI.gameObject.SetActive(true);

    }
    IEnumerator OpenBooth()
    {
        animator.SetTrigger("OpenUpgradeBooth");
        uI.gameObject.SetActive(false);
        FMODUnity.RuntimeManager.PlayOneShot(aOpen);
        yield return new WaitForSeconds(1.8f);

        pU.gameObject.GetComponent<GarageMovement>().enabled = true;
        opening = false;
        esced = false;
    }
    public void DeActivateUI()
    {
        StartCoroutine(OpenBooth());
    }


    public void HpFloppy()
    {
        if (hpCollected == true && hpButton.GetComponent<Image>().sprite != imgActive)
        {
            ResetUpgrades();
            pU.setHp(hpAmount);
            TextResetToFalse();
            hpText.SetActive(true);
            SetButtons(hpButton);
            secondString = "HP";
            ResetImages();
        }

    }
    public void DmgFloppy()
    {
        if (dmgCollected == true && dmgButton.GetComponent<Image>().sprite != imgActive)
        {
            ResetUpgrades();
            pU.setDmg(dmgAmount);
            TextResetToFalse();
            dmgText.SetActive(true);
            SetButtons(dmgButton);
            secondString = "DMG";
            ResetImages();
        }

    }
    public void CritFloppy()
    {
        if (critCollected == true && critButton.GetComponent<Image>().sprite != imgActive)
        {
            ResetUpgrades();
            pU.setCrit(critAmount);
            TextResetToFalse();
            critText.SetActive(true);
            SetButtons(critButton);
            secondString = "CRIT";
            ResetImages();
        }

    }
    public void DefFloppy()
    {
        if (defCollected == true && defButton.GetComponent<Image>().sprite != imgActive)
        {
            ResetUpgrades();
            pU.setDef(defAmount);
            TextResetToFalse();
            defText.SetActive(true);
            SetButtons(defButton);
            secondString = "DEF";
            ResetImages();
        }

    }
    public void SpeedFloppy()
    {
        if (speedCollected == true && speedButton.GetComponent<Image>().sprite != imgActive)
        {
            ResetUpgrades();
            pU.setSpeed(speedAmount);
            TextResetToFalse();
            speedText.SetActive(true);
            SetButtons(speedButton);
            secondString = "SPEED";
            ResetImages();
        }
    }

    private void TextResetToFalse()
    {
        hpText.SetActive(false);
        dmgText.SetActive(false);
        critText.SetActive(false);
        defText.SetActive(false);
        speedText.SetActive(false);
    }

    private void SetButtons(GameObject currentButton)
    {
        if(firstButton == null)
        {
            firstButton = currentButton;
        }
        else if(secondButton == null)
        {
            secondButton = currentButton;
            if (firstButton == hpButton)
            {
                pU.setHp(hpAmount);
                hpText.SetActive(true);
            }
            else if(firstButton == dmgButton)
            {
                pU.setDmg(dmgAmount);
                dmgText.SetActive(true);
            }
            else if(firstButton == critButton)
            {
                pU.setCrit(critAmount);
                critText.SetActive(true);
            }
            else if(firstButton == defButton)
            {
                pU.setDef(defAmount);
                defText.SetActive(true);
            }
            else if(firstButton == speedButton)
            {
                pU.setSpeed(speedAmount);
                speedText.SetActive(true);
            }
        }
        else
        {
            if (secondString == "HP")
            {
                firstButton = hpButton;
                hpText.SetActive(true);
                pU.setHp(hpAmount);
            }
            else if(secondString == "DMG")
            {
                firstButton = dmgButton;
                dmgText.SetActive(true);
                pU.setDmg(dmgAmount);
            }
            else if(secondString == "CRIT")
            {
                firstButton = critButton;
                critText.SetActive(true);
                pU.setCrit(critAmount);
            }
            else if(secondString == "DEF")
            {
                firstButton = defButton;
                defText.SetActive(true);
                pU.setDef(defAmount);
            }
            else if(secondString == "SPEED")
            {
                firstButton = speedButton;
                speedText.SetActive(true);
                pU.setSpeed(speedAmount);
            }
            secondButton = currentButton;
        }
    }

    private void ResetUpgrades()
    {
        pU.setHp(0);
        pU.setDmg(0);
        pU.setCrit(0f);
        pU.setDef(0);
        pU.setSpeed(0);
    }
    public void ResetImages()
    {
        if (hpCollected == true)
        {
            hpButton.GetComponent<Image>().sprite = imgCollected;
        }
        else if(hpCollected == false)
        {
            hpButton.GetComponent<Button>().interactable = false;
            hpImage.SetActive(false);
            hpButtonText.GetComponent<Text>().color = Color.gray;
        }

        if(dmgCollected == true)
        {
            dmgButton.GetComponent<Image>().sprite = imgCollected;
        }
        else if(dmgCollected == false)
        {
            dmgButton.GetComponent<Button>().interactable = false;
            dmgImage.SetActive(false);
            dmgButtonText.GetComponent<Text>().color = Color.gray;
        }

        if (critCollected == true)
        {
            critButton.GetComponent<Image>().sprite = imgCollected;
        }
        else if (critCollected == false)
        {
            critButton.GetComponent<Button>().interactable = false;
            critImage.SetActive(false);
            critButtonText.GetComponent<Text>().color = Color.gray;
        }

        if (defCollected == true)
        {
            defButton.GetComponent<Image>().sprite = imgCollected;
        }
        else if (defCollected==false)
        {
            defButton.GetComponent<Button>().interactable = false;
            defImage.SetActive(false);
            defButtonText.GetComponent<Text>().color = Color.gray;
        }

        if (speedCollected == true)
        {
            speedButton.GetComponent<Image>().sprite = imgCollected;
        }
        else if (speedCollected == false)
        {
            speedButton.GetComponent<Button>().interactable = false;
            speedImage.SetActive(false);
            speedButtonText.GetComponent<Text>().color = Color.gray;
        }

        if(firstButton != null)
        {
            firstButton.GetComponent<Image>().sprite = imgActive;
        }

        if(secondButton != null)
        {
            secondButton.GetComponent<Image>().sprite = imgActive;
        }

    }

    public void SetHpCollected(bool collected)
    {
        hpCollected = collected;
    }
    public void SetDmgCollected(bool collected)
    {
        dmgCollected = collected;
    }
    public void SetCritCollected(bool collected)
    {
        critCollected = collected;
    }
    public void SetDefCollected(bool collected)
    {
        defCollected = collected;
    }
    public void SetSpeedCollected(bool collected)
    {
        speedCollected = collected;
    }

    public void ResetButton()
    {
        firstButton = null;
        secondButton = null;
        ResetUpgrades();
        ResetImages();
        hpText.SetActive(false);
        dmgText.SetActive(false);
        critText.SetActive(false);
        defText.SetActive(false);
        speedText.SetActive(false);
        godmode.SetActive(false);
    }

    public void soundOnClick()
    {
        FMODUnity.RuntimeManager.PlayOneShot(aMenu);
    }
}
