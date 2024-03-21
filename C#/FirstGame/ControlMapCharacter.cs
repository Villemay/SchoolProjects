using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlMapCharacter : MonoBehaviour
{
    public GameObject currentWaypoint;
    private Waypoint wp;
    public GameObject firstWaypoint;
    public bool moving = false;
    public bool movingup = true;
    public bool canMoveUp = true;
    public float movingtime = 3;
    public Text smallCollectablesText;
    public Text largeCollectiblesText;
    public GameObject coinIcon;
    public GameObject nutIcon;
    public GameObject timerIcon;
    public Text timerText;
    public GameObject level2Image;
    public GameObject level3Image;
    public GameObject groundpoundPlatform;
    public Animator anim;
    public Text levelNameText;
    public GameObject climbAbilityIcon;
    public GameObject climbAbilityText;
    public GameObject dashAbilityIcon;
    public GameObject dashAbilityText;
    public GameObject burrowAbilityIcon;
    public GameObject burrowAbilityText;
    public GameObject groundpoundAbilityIcon;
    public GameObject groundpoundAbilityText;

    // Start is called before the first frame update
    void Start()
    {
        loadLevels();
        //moves the player to first maps position
        if (SaveLoad.enteredWaypoint != null)
        {
            transform.position = GameObject.Find(SaveLoad.enteredWaypoint).transform.position;
        }
        else 
        {
            transform.position = firstWaypoint.transform.position;
        }
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Moving", moving);
        if(moving == true)
        {
            hideAbilitys();
        }
        //if the player isn't moving and is stopped it can move
        if (moving == false && wp != null && wp.level==true)
        {
            //moves the player forwards and resets the text fields
            if (Input.GetAxisRaw("Vertical") ==1 && canMoveUp==true)
            {
                smallCollectablesText.text = "";
                largeCollectiblesText.text = "";
                timerText.text = "";
                levelNameText.text = "";
                nutIcon.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                coinIcon.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                timerIcon.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                StartCoroutine(moveTo(this.transform, wp.next.transform.localPosition, movingtime));
                movingup = true;
            }
            //moves the player backwards and resets the text fields
            if (Input.GetAxisRaw("Vertical") == -1)
            {
                smallCollectablesText.text = "";
                largeCollectiblesText.text = "";
                timerText.text = "";
                levelNameText.text = "";
                nutIcon.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                coinIcon.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                timerIcon.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                StartCoroutine(moveTo(this.transform, wp.previous.transform.localPosition, movingtime));
                movingup = false;
                canMoveUp = true;
            }
            if (wp.level == true)
            {
                //if the level has been set 
                if (wp.levelname != "PutLevelNameHere")
                {
                    if (wp.levelname == "Level1" && level2Image.activeInHierarchy == true)
                    {
                        canMoveUp = true;
                    }
                    else if(wp.levelname == "Level2" && level3Image.activeInHierarchy == true)
                    {
                        canMoveUp = true;
                    }
                    else if(wp.levelname == "Level3" && groundpoundPlatform.activeInHierarchy == true)
                    {
                        canMoveUp = true;
                    }
                    else
                    {
                        canMoveUp = false;
                    }
                    //changes the scene to the wanted level
                    if (Input.GetButtonDown("Interact"))
                    {
                        SceneManager.LoadScene(wp.levelname);
                    }
                }
                else
                {
                    if(currentWaypoint.name == "Waypoint 1")
                    {
                        levelNameText.text = "";
                        climbAbilityIcon.SetActive(true);
                        climbAbilityText.SetActive(true);
                    }
                    else if(currentWaypoint.name == "Waypoint 8 (Unlock Dash)")
                    {
                        levelNameText.text = "";
                        dashAbilityIcon.SetActive(true);
                        dashAbilityText.SetActive(true);
                    }
                    else if (currentWaypoint.name == "Waypoint 19 (Unlock Burrow)")
                    {
                        levelNameText.text = "";
                        burrowAbilityIcon.SetActive(true);
                        burrowAbilityText.SetActive(true);
                    }
                    else if(currentWaypoint.name == "Waypoint 39 (Unlock Ground Pound)")
                    {
                        levelNameText.text = "";
                        groundpoundAbilityIcon.SetActive(true);
                        groundpoundAbilityText.SetActive(true);
                    }
                    canMoveUp = true;
                }
            }
        }
    }
    //when the player hits a waypoint
    private void OnTriggerEnter(Collider collision)
    {
        moving = false;
        currentWaypoint = collision.gameObject;
        wp = currentWaypoint.GetComponent<Waypoint>();
        SaveLoad.currentLevel = wp.levelname;
        SaveLoad.enteredWaypoint = wp.name;
        //if the waypoint doesn't contain a level it moves the player to the next waypoint automatically
        if (wp.level == false)
        {
            if (movingup == true)
            {
                StartCoroutine(moveTo(this.transform, wp.next.transform.localPosition, movingtime));
            }
            else
            {
                StartCoroutine(moveTo(this.transform, wp.previous.transform.localPosition, movingtime));
            }
            moving = true;
        }
        //if the waypoint contains a level load the collectibles data of that level
        else
        {
            PlayerData data = SaveLoad.loadPlayer();
            if (data != null)
            {
                if (wp.levelname == "Level1")
                {
                    smallCollectablesText.text = "" + data.smlColAmount + " / 120";
                    nutIcon.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    largeCollectiblesText.text = "" + data.rareCollectableNames.Length + " / 10";
                    coinIcon.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    timerIcon.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    levelNameText.text = "Level 1 - Paw of the Mountain";
                    if (data.cleared == true)
                    {
                        timerText.text = "" + ((int)data.BestTimerTime / 60) + " : " + ((int)data.BestTimerTime % 60) + " : " +
                            (int)(Mathf.Floor((data.BestTimerTime - (((int)data.BestTimerTime % 60) +
                            ((int)data.BestTimerTime / 60) * 60)) * 100));
                        if (data.BestTimerTime == 0)
                        {
                            timerText.text = "0:00:00";
                        }
                    }
                }
                else if (wp.levelname=="Level2")
                {
                    smallCollectablesText.text = "" + data.smlColAmount + " / 120";
                    nutIcon.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    largeCollectiblesText.text = "" + data.rareCollectableNames.Length + " / 10";
                    coinIcon.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    timerIcon.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    levelNameText.text = "Level 2 - Dusty Roads, Gusty Paths";
                    if (data.cleared == true)
                    {
                        timerText.text = "" + ((int)data.BestTimerTime / 60) + " : " + ((int)data.BestTimerTime % 60) + " : " +
                            (int)(Mathf.Floor((data.BestTimerTime - (((int)data.BestTimerTime % 60) +
                            ((int)data.BestTimerTime / 60) * 60)) * 100));
                        if (data.BestTimerTime == 0)
                        {
                            timerText.text = "0:00:00";
                        }
                    }

                }
                else if(wp.levelname == "Level3")
                {
                    smallCollectablesText.text = "" + data.smlColAmount + " / 120";
                    nutIcon.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    largeCollectiblesText.text = "" + data.rareCollectableNames.Length + " / 10";
                    coinIcon.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    timerIcon.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    levelNameText.text = "Level 3 - Nighttime Cliff-Hopping";
                    if (data.cleared == true)
                    {
                        timerText.text = "" + ((int)data.BestTimerTime / 60) + " : " + ((int)data.BestTimerTime % 60) + " : " +
                            (int)(Mathf.Floor((data.BestTimerTime - (((int)data.BestTimerTime % 60) +
                            ((int)data.BestTimerTime / 60) * 60)) * 100));
                        if(data.BestTimerTime == 0)
                        {
                            timerText.text = "0:00:00";
                        }
                    }
                }

            }
            else
            {
                if (wp.levelname == "Level1")
                {
                    smallCollectablesText.text = "0 / 120";
                    nutIcon.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    largeCollectiblesText.text = "0 / 10";
                    coinIcon.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    timerIcon.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    timerText.text = "0:00:00";
                    levelNameText.text = "Level 1 - Paw of the Mountain";
                }
                else if (wp.levelname == "Level2")
                {
                    smallCollectablesText.text = "0 / 120";
                    nutIcon.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    largeCollectiblesText.text = "0 / 10";
                    coinIcon.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    timerIcon.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    timerText.text = "0:00:00";
                    levelNameText.text = "Level 2 - Dusty Roads, Gusty Paths";
                }
                else if (wp.levelname == "Level3")
                {
                    smallCollectablesText.text = "0 / 120";
                    nutIcon.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    largeCollectiblesText.text = "0 / 10";
                    coinIcon.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    timerIcon.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    timerText.text = "0:00:00";
                    levelNameText.text = "Level 3 - Nighttime Cliff-Hopping";
                }
            }
        }
    }
    //moves the player between waypoints
    IEnumerator moveTo(Transform fromPosition,Vector3 toPosition,float duration) 
    {
        if (moving)
        {
            yield break;
        }
        moving = true;
        float counter = 0;
        Vector3 starPosition = fromPosition.position;

        while(counter < duration)
        {
            counter += Time.deltaTime;
            fromPosition.position = Vector3.Lerp(starPosition, toPosition, counter / duration);
            yield return null;
        }
        moving = false;
    }
    public void loadLevels()
    {
        SaveLoad.currentLevel = "Level1";
        PlayerData data = SaveLoad.loadPlayer();
        if(data != null)
        {
            if (data.cleared == true)
            {
                SaveLoad.dash = true;
                level2Image.gameObject.SetActive(true);
                //lockedDashIcon.gameObject.GetComponent<MeshRenderer>().enabled = false;
                //dashIcon.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        SaveLoad.currentLevel = "Level2";
        PlayerData data1 = SaveLoad.loadPlayer();
        if(data1 != null)
        {
            if(data.cleared == true)
            {
                SaveLoad.burrow = true;
                level3Image.gameObject.SetActive(true);
                //lockedBurrowIcon.gameObject.GetComponent<MeshRenderer>().enabled = false;
                //burrowIcon.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        SaveLoad.currentLevel = "Level3";
        PlayerData data2 = SaveLoad.loadPlayer();
        if(data2 != null)
        {
            if(data.cleared == true)
            {
                SaveLoad.groundpound = true;
                groundpoundPlatform.SetActive(true);
                //lockedGroundpoundIcon.gameObject.GetComponent<MeshRenderer>().enabled = false;
                //groundpoundIcon.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
    public void hideAbilitys()
    {
        climbAbilityIcon.SetActive(false);
        climbAbilityText.SetActive(false);
        dashAbilityIcon.SetActive(false);
        dashAbilityText.SetActive(false);
        burrowAbilityIcon.SetActive(false);
        burrowAbilityText.SetActive(false);
        groundpoundAbilityIcon.SetActive(false);
        groundpoundAbilityText.SetActive(false);
    }
}
