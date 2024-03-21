using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FloppyDisk : MonoBehaviour
{

    [SerializeField] GameObject floppyDiskMenu;
    [SerializeField] Text floppyDiskDescription;
    [SerializeField] private GameObject interactInfo;
    private bool isMelee = false;
    private bool isRanged = false;
    private GameObject _player;
    private GameObject _getUpgrade;
    private GameObject inputManager;
    private bool inTrigger = false;


    [Header("Sounds")]
    [SerializeField]
    [FMODUnity.EventRef]
    private string aPickUp;

    [SerializeField] private List<string> list;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        floppyDiskMenu = GameObject.Find("FloppyDiskMenu");
        _getUpgrade = GameObject.Find("GetUpgrade");
        inputManager = GameObject.Find("InputManager");
        interactInfo = GameObject.Find("InteractTextMEGAGOOD");
    }

    // Update is called once per frame
    void Update()
    {
        //if the player is in the trigger and presses the interact button take the floppydisk;
        if (inTrigger == true)
        {
            if (Input.GetKeyDown(inputManager.GetComponent<InputManager>().GetInteract()) ||
                Input.GetKeyDown(inputManager.GetComponent<InputManager>().GetSecondaryInteract()))
            {
                if (_player.GetComponent<PlayerUpgrades>().GetRanged() == true)
                    isRanged = true;
                else
                    isMelee = true;

                interactInfo.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                floppyDiskMenu.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                //floppyDiskDescription = GameObject.Find("FloppyText").GetComponent<Text>();

                floppyDiskMenu.GetComponent<FloppyDiskMenuController>().StartCoroutineHideText();


                CheckAvailableUpgrades();
                UnlockFloppyDisk();

                inTrigger = false;
                Destroy(gameObject, 0.5f);

            }
        }
    }

    //if the player is inside the trigger it can intaract with it using the interact button
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            interactInfo.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            inTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            interactInfo.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            inTrigger = false;


        }
    }


    public void CheckAvailableUpgrades()
    {
        //clears the list and adds all the not collected floppydisks to a list
        list.Clear();
        if (_player.GetComponent<PlayerUpgrades>().GetHpFloppy() == false)
        {
            list.Add("HP");
        }
        if (_player.GetComponent<PlayerUpgrades>().GetDmgFloppy() == false)
        {
            list.Add("DMG");
        }
        if (_player.GetComponent<PlayerUpgrades>().GetCritFloppy() == false)
        {
            list.Add("CRIT");
        }
        if (_player.GetComponent<PlayerUpgrades>().GetDefFloppy() == false)
        {
            list.Add("DEF");
        }
        if (_player.GetComponent<PlayerUpgrades>().GetSpeedFloppy() == false)
        {
            list.Add("SPEED");
        }
    }


    private void UnlockFloppyDisk()
    {
        if (list.Count != 0)
        {
            //takes a random item from the list and set the floppydisk to true
            int randomNmb = Random.Range(0, list.Count - 1);
            if (list[randomNmb] == "HP")
            {
                _player.GetComponent<PlayerUpgrades>().SetHpFloppy(true);
                //floppyDiskDescription.text = "HP FLOPPY";
            }
            if (list[randomNmb] == "DMG")
            {
                _player.GetComponent<PlayerUpgrades>().SetDmgFloppy(true);
                //floppyDiskDescription.text = "DMG FLOPPY";
            }
            if (list[randomNmb] == "CRIT")
            {
                _player.GetComponent<PlayerUpgrades>().SetCritFloppy(true);
                //floppyDiskDescription.text = "CRIT FLOPPY";
            }
            if (list[randomNmb] == "DEF")
            {
                _player.GetComponent<PlayerUpgrades>().SetDefFloppy(true);
                //floppyDiskDescription.text = "DEF FLOPPY";
            }
            if (list[randomNmb] == "SPEED")
            {
                _player.GetComponent<PlayerUpgrades>().SetSpeedFloppy(true);
                //floppyDiskDescription.text = "SPEED FLOPPY";
            }


            if(list.Count == 1)
            {
                _player.GetComponent<PlayerUpgrades>().SetFloppyCollection(true);

            }

            //saves the floppy disk
            _player.GetComponent<PlayerUpgrades>().savePlayer();



        }
    }





}
