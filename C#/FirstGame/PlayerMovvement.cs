using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovvement : MonoBehaviour
{
    public List<string> collectables;
    public List<string> checkpoints;
    public List<string> rareCollectables;
    public GameObject timer;
    public bool cleared = false;
    public int maxSmalColAmount = 0;
    public float bestTimerTime = float.MaxValue;
    public Text collectableText;
    public Text rareCollectableText;
    public Text timerText;
    public GameObject hud;
    public Text hudCollectableText;
    public Text hudRareCollectableText;
    public Text hudTimerText;
    public GameObject hudTimerIcon;
    public GameObject hat;
    public bool lvl3Cleared = false;
    public GameObject menu;
    public GameObject orbA;
    public GameObject orbB;
    public GameObject teleporter;
    public int health = 3;
    public GameObject healthFull1;
    public GameObject healthEmpty1;
    public GameObject healthFull2;
    public GameObject healthEmpty2;
    public GameObject healthFull3;
    public GameObject healthEmpty3;
    public GameObject hudHealthFull1;
    public GameObject hudHealthEmpty1;
    public GameObject hudHealthFull2;
    public GameObject hudHealthEmpty2;
    public GameObject hudHealthFull3;
    public GameObject hudHealthEmpty3;
    public AudioClip CollectibleSound;
    public AudioClip CheckpointSound;
    public AudioClip WizcoinSound;
    public AudioSource playerAudioSource;

    Player player;
    float damageCooldown;

    void Start()
    {
        player = GetComponent<Player>();
        damageCooldown = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (timer != null && menu.activeInHierarchy == true)
        {
            timerText.text = timer.GetComponent<Timer>().timerMinutes + " : " + timer.GetComponent<Timer>().timerSeconds + " : " +
                timer.GetComponent<Timer>().timerSeconds100;
        }
        if (timer != null && hud.activeInHierarchy == true)
        {
            hudTimerText.text = timer.GetComponent<Timer>().timerMinutes + " : " + timer.GetComponent<Timer>().timerSeconds + " : " +
                timer.GetComponent<Timer>().timerSeconds100;
        }
        if (hud.activeInHierarchy == false)
        {
            if (Input.GetButtonDown("Hud"))
            {
                StartCoroutine(showHud());
            }
        }


    }
    public void OnTriggerEnter(Collider collision)
    {
        //if player hits collectable it will be added to list and it's component will be turned off
        if (collision.tag == "Collectable")
        {
            collectables.Add(collision.gameObject.name);
            
            collision.gameObject.GetComponent<MeshRenderer>().enabled = false;
            collision.gameObject.GetComponent<MeshCollider>().enabled = false;
            collision.gameObject.GetComponentInChildren<ParticleSystem>().Stop();
            collectableText.text = collectables.Count.ToString() + " / " + (int)GameObject.FindGameObjectsWithTag("Collectable").Length;
            hudCollectableText.text = collectables.Count.ToString() + " / " + (int)GameObject.FindGameObjectsWithTag("Collectable").Length;
            StartCoroutine(showHud());
            playerAudioSource.PlayOneShot(CollectibleSound);
        }
        //if player hits a rare collectable it will be added to list and it's collider will be turned off
        if(collision.tag == "RareCollectible")
        {
            rareCollectables.Add(collision.gameObject.name);
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
            collision.gameObject.GetComponent<MeshRenderer>().enabled = false;
            rareCollectableText.text = rareCollectables.Count.ToString() + " / " + 
                (int)GameObject.FindGameObjectsWithTag("RareCollectible").Length;
            hudRareCollectableText.text = rareCollectables.Count.ToString() + " / " +
                (int)GameObject.FindGameObjectsWithTag("RareCollectible").Length;
            StartCoroutine(showHud());
            playerAudioSource.PlayOneShot(WizcoinSound);
        }
        //if the player hits a checkpoint the checkpoint will be added to a list, the checkpoints component will be turned off and the 
        //player data will be saved
        if(collision.tag == "Checkpoint")
        {
            checkpoints.Add(collision.gameObject.name);
            collision.gameObject.GetComponent<MeshRenderer>().enabled = false;
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
            collision.transform.GetChild(1).gameObject.SetActive(false);
            collision.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            collision.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(true);
            collision.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.SetActive(true);
            savePlayer();
            StartCoroutine(showHud());
            playerAudioSource.PlayOneShot(CheckpointSound);
        }
        //if the player hits a deathwall reset the maps collectables and load the last checkpoint
        //if the player is on a timetrial stop and reset the timer, reset the map 
        if(collision.tag == "DeathWall")
        {
            die();
        }
        //if player hits the finishline load the overworld
        //if the player is on timetrial stop and post the time 
        //save the progres into a file
        if(collision.tag == "Finish")
        {
            if(timer != null)
            {
                timer.GetComponent<Timer>().TimerStop();
                if (timer.GetComponent<Timer>().timerTime < bestTimerTime)
                {
                    bestTimerTime = timer.GetComponent<Timer>().timerTime;
                }
            }
            if (bestTimerTime == float.MaxValue)
            {
                bestTimerTime = 0;
            }
            cleared = true;
            if (SaveLoad.currentLevel == "Level3")
            {
                lvl3Cleared = true;
            }
            savePlayer();
            SceneManager.LoadScene("OverWorld");
        }
        //if player hits timetrial start block, turn timetrial start blocks components off and start the time
        if (collision.tag == "TimerStart")
        {
            timer = collision.gameObject;
            timer.GetComponent<MeshRenderer>().enabled = false;
            timer.GetComponent<BoxCollider>().enabled = false;
            timer.GetComponent<Timer>().TimerStart();
            hideCollectablesAndCheckpoints();
            int timerHideObjectSize = GameObject.FindGameObjectsWithTag("TimerHide").Length;
            for (int i = 0; i < timerHideObjectSize; i++)
            {
                GameObject hidetimer;
                if (i == 0)
                {
                    hidetimer = GameObject.Find("TimerHide");
                }
                else
                {
                    hidetimer = GameObject.Find("TimerHide (" + i + ")");
                }
                hidetimer.GetComponent<BoxCollider>().enabled = false;
            }
        }
        //if player hits the timerhide trigger hide the timetrial start block so player can't cheat
        if (collision.tag == "TimerHide")
        {
            hideTimer();
        }
        //when the level starts load the rare collectables if there are any collected and hide them from the map
        //change the collider off so player doesn't trigger it multiple times
        if(collision.tag == "LevelStart")
        {
            loadRareCollectables();
            collision.GetComponent<BoxCollider>().enabled = false;
            collectableText.text = collectables.Count.ToString() + " / " + (int)GameObject.FindGameObjectsWithTag("Collectable").Length;
            hudCollectableText.text = collectables.Count.ToString() + " / " + (int)GameObject.FindGameObjectsWithTag("Collectable").Length;
            savePlayer();
        }
        if(collision.tag == "KeyCardA")
        {
            collision.gameObject.SetActive(false);
            orbA.gameObject.SetActive(true);
            if (orbB.activeInHierarchy == true)
            {
                teleporter.transform.GetChild(1).gameObject.SetActive(true);
                teleporter.transform.GetChild(4).gameObject.SetActive(true);
            }
        }
        if(collision.tag == "KeyCardB")
        {
            collision.gameObject.SetActive(false);
            orbB.gameObject.SetActive(true);
            if (orbA.activeInHierarchy == true)
            {
                teleporter.transform.GetChild(1).gameObject.SetActive(true);
                teleporter.transform.GetChild(4).gameObject.SetActive(true);
            }
        }
        if(collision.tag == "Teleport")
        {
            this.GetComponent<CharacterController>().enabled = false;
            this.gameObject.transform.position = collision.transform.GetChild(0).transform.position;
            this.GetComponent<CharacterController>().enabled = true;
        }
        if(collision.tag == "Enemy" && Time.time > damageCooldown)
        {
            damageCooldown = Time.time + 2f;
            Vector3 enemy = new Vector3(collision.gameObject.transform.position.x, transform.position.y, collision.gameObject.transform.position.z);
            player.TakeDamage(1,enemy);
            if(health == 1)
            {
                die();
            }
            else if (health == 2)
            {
                healthFull2.SetActive(false);
                healthEmpty2.SetActive(true);
                hudHealthFull2.SetActive(false);
                hudHealthEmpty2.SetActive(true);
                health = 1;
                StartCoroutine(showHud());
            }
            else if (health == 3)
            {
                healthFull3.SetActive(false);
                healthEmpty3.SetActive(true);
                hudHealthFull3.SetActive(false);
                hudHealthEmpty3.SetActive(true);
                health = 2;
                StartCoroutine(showHud());
            }
        }
    }
    //loads collected rare collectables and hides them from the scene
    public void loadRareCollectables()
    {
        //if the save doesn't exist (will give a error in the log still, but it comes from SaveLoad and it is intended)
        if (SaveLoad.loadPlayer() != null)
        {
            //load the player data from the file
            PlayerData data = SaveLoad.loadPlayer();
            //adds the rare collectables from the file and adds them to the scenes list
            for (int i = 0; i < data.rareCollectableNames.Length; i++)
            {
                if (rareCollectables.Contains(data.rareCollectableNames[i]) == false)
                {
                    rareCollectables.Add(data.rareCollectableNames[i]);
                }
            }
            rareCollectableText.text = rareCollectables.Count.ToString() + " / " +
                (int)GameObject.FindGameObjectsWithTag("RareCollectible").Length;
            hudRareCollectableText.text = rareCollectables.Count.ToString() + " / " +
                (int)GameObject.FindGameObjectsWithTag("RareCollectible").Length;
            //if the level is not cleared hide the timetrial start
            if (data.cleared != true)
            {
                hideTimer();
            }
            else
            {
                cleared = true;
            }
            //hide the rare collectables that have been added to the scenes list
            hideRareCollectables();
            //max collectables collected
            maxSmalColAmount = data.smlColAmount;
            bestTimerTime = data.BestTimerTime;
            if (bestTimerTime <= 0)
            {
                bestTimerTime = float.MaxValue;
            }
            lvl3Cleared = data.level3Cleared;
            if(lvl3Cleared == true)
            {
                hat.SetActive(true);
            }
            else
            {
                hat.SetActive(false);
            }
        }
        else
        {
            hideTimer();
            rareCollectableText.text = rareCollectables.Count.ToString() + " / " +
                (int)GameObject.FindGameObjectsWithTag("RareCollectible").Length;
            hudRareCollectableText.text = rareCollectables.Count.ToString() + " / " +
                (int)GameObject.FindGameObjectsWithTag("RareCollectible").Length;
        }
    }
    //function that loads the player data
    public void loadPlayer()
    {
        //empty the collectables list if there is items collected
        for(int i =collectables.Count-1;i>-1;i--)
        {
            collectables.RemoveAt(i);
        }
        //empty the rare collectables list if there is items collected
        for(int i = rareCollectables.Count - 1; i > -1; i--)
        {
            rareCollectables.RemoveAt(i);
        }
        //load the saved data
        PlayerData data = SaveLoad.loadPlayer();
        //change the players position to the saved one
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        //cc need to be turned off to move the player
        this.GetComponent<CharacterController>().enabled = false;
        this.gameObject.transform.position = position;
        this.GetComponent<CharacterController>().enabled = true;
        //add collectables from the save to the list
        for (int i = 0; i < data.collectableNames.Length; i++)
        {
            if (collectables.Contains(data.collectableNames[i])==false)
            {
                collectables.Add(data.collectableNames[i]);
            }
        }
        //add checkpoints from the save to the list
        for(int i = 0; i < data.checkpointNames.Length; i++)
        {
            if (checkpoints.Contains(data.checkpointNames[i]) == false)
            {
                checkpoints.Add(data.checkpointNames[i]);
            }
        }
        //add rare collectables from the save to the list
        for(int i = 0; i < data.rareCollectableNames.Length; i++)
        {
            if (rareCollectables.Contains(data.rareCollectableNames[i]) == false)
            {
                rareCollectables.Add(data.rareCollectableNames[i]);
            }
        }
        //hide the collected checkpoints, collectables and rare collectables
        hideCheckpoints();
        hideCollectables();
        hideRareCollectables();


    }
    //function that saves the player data
    public void savePlayer()
    {
        SaveLoad.SavePlayer(this);
    }
    //function that hides collected checkpoints
    public void hideCheckpoints()
    {
        foreach(string checkpoint in checkpoints)
        {
            GameObject checkp = GameObject.Find(checkpoint);
            checkp.GetComponent<MeshRenderer>().enabled = false;
            checkp.GetComponent<Collider>().enabled = false;
        }
    }
    //function that hides collected rare collectables
    public void hideRareCollectables()
    {
        foreach(string rareCollectable in rareCollectables)
        {
            GameObject collec = GameObject.Find(rareCollectable);
            collec.GetComponent<BoxCollider>().enabled = false;
            collec.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    //function that hides collected collectables
    public void hideCollectables()
    {
        foreach (string collectable in collectables)
        {
            GameObject collec = GameObject.Find(collectable);
            
            collec.GetComponent<MeshRenderer>().enabled = false;
            collec.GetComponent<MeshCollider>().enabled = false;
            collec.gameObject.GetComponentInChildren<ParticleSystem>().Stop();
        }
    }
    //function that turns all the collected collectables back on
    public void resetCollectables()
    {
        foreach(string collectable in collectables)
        {
            GameObject collec = GameObject.Find(collectable);
            
            collec.GetComponent<MeshRenderer>().enabled = true;
            collec.GetComponent<MeshCollider>().enabled = true;
            collec.gameObject.GetComponentInChildren<ParticleSystem>().Play();
        }
    }
    //function that turns all the collected rare collectable back on
    public void resetRareCollectables()
    {
        foreach(string rareCollectable in rareCollectables)
        {
            GameObject collec = GameObject.Find(rareCollectable);
            collec.GetComponent<BoxCollider>().enabled = true;
            collec.GetComponent<MeshRenderer>().enabled = true;
        }
    }
    //for the timetrial hides all the collectables and checkpoint in the level
    public void hideCollectablesAndCheckpoints()
    {
        int collectablesSize = GameObject.FindGameObjectsWithTag("Collectable").Length;
        for (int i = 0; i < collectablesSize; i++)
        {
            GameObject collec;
            if (i == 0)
            {
                collec = GameObject.Find("Nut_Collectible");
            }
            else
            {
                collec = GameObject.Find("Nut_Collectible (" + i + ")");
            }
            
            collec.GetComponent<MeshRenderer>().enabled = false;
            collec.GetComponent<MeshCollider>().enabled = false;
            collec.gameObject.GetComponentInChildren<ParticleSystem>().Stop();
        }

        int checkpointSize = GameObject.FindGameObjectsWithTag("Checkpoint").Length;
        for(int i = 0;i < checkpointSize; i++)
        {
            GameObject checkp;
            if (i == 0)
            {
                checkp = GameObject.Find("PlaceholderCheckpoint");
            }
            else
            {
                checkp = GameObject.Find("PlaceholderCheckpoint (" + i + ")");
            }
            checkp.GetComponent<MeshRenderer>().enabled = false;
            checkp.GetComponent<BoxCollider>().enabled = false;
        }
        int rareCollectableSize = GameObject.FindGameObjectsWithTag("RareCollectible").Length;
        for(int i = 0; i < rareCollectableSize; i++)
        {
            GameObject collec;
            if (i == 0)
            {
                collec = GameObject.Find("RareCollectible");
            }
            else
            {
                collec = GameObject.Find("RareCollectible (" + i + ")");
            }
            collec.GetComponent<MeshRenderer>().enabled = false;
            collec.GetComponent<BoxCollider>().enabled = false;
        }
    }
    //for the timetrial resets all the checkpoints and collectables to be visible again
    public void resetCollectablesAndCheckpoints()
    {
        int collectablesSize = GameObject.FindGameObjectsWithTag("Collectable").Length;
        for (int i = 0; i < collectablesSize; i++)
        {
            GameObject collec;
            if (i == 0)
            {
                collec = GameObject.Find("Nut_Collectible");
            }
            else
            {
                collec = GameObject.Find("Nut_Collectible (" + i + ")");
            }
            
            collec.GetComponent<MeshRenderer>().enabled = true;
            collec.GetComponent<MeshCollider>().enabled = true;
            collec.gameObject.GetComponentInChildren<ParticleSystem>().Play();
        }

        int checkpointSize = GameObject.FindGameObjectsWithTag("Checkpoint").Length;
        for (int i = 0; i < checkpointSize; i++)
        {
            GameObject checkp;
            if (i == 0)
            {
                checkp = GameObject.Find("PlaceholderCheckpoint");
            }
            else
            {
                checkp = GameObject.Find("PlaceholderCheckpoint (" + i + ")");
            }
            checkp.GetComponent<MeshRenderer>().enabled = true;
            checkp.GetComponent<BoxCollider>().enabled = true;
        }
        int rareCollectableSize = GameObject.FindGameObjectsWithTag("RareCollectible").Length;
        for (int i = 0; i < rareCollectableSize; i++)
        {
            GameObject collec;
            if (i == 0)
            {
                collec = GameObject.Find("RareCollectible");
            }
            else
            {
                collec = GameObject.Find("RareCollectible (" + i + ")");
            }
            collec.GetComponent<MeshRenderer>().enabled = true;
            collec.GetComponent<BoxCollider>().enabled = true;
        }
    }
    //hides the timerstarter and the timerhider
    public void hideTimer()
    {
        int timerHideObjectSize = GameObject.FindGameObjectsWithTag("TimerHide").Length;
        for(int i = 0; i < timerHideObjectSize; i++)
        {
            GameObject hidetimer;
            if (i == 0)
            {
                hidetimer = GameObject.Find("TimerHide");
            }
            else
            {
                hidetimer = GameObject.Find("TimerHide (" + i + ")");
            }
            hidetimer.GetComponent<BoxCollider>().enabled = false;
        }
        GameObject timerStartObject = GameObject.FindGameObjectWithTag("TimerStart");
        timerStartObject.GetComponent<BoxCollider>().enabled = false;
        timerStartObject.GetComponent<MeshRenderer>().enabled = false;
        hudTimerIcon.SetActive(false);
        hudTimerText.text = "";
    }
    public void die()
    {
        if (timer != null)
        {
            timer.GetComponent<Timer>().TimerStop();
            timer.GetComponent<Timer>().TimerReset();
            resetCollectablesAndCheckpoints();
        }
        if (cleared == true)
        {
            int timerHideObjectSize = GameObject.FindGameObjectsWithTag("TimerHide").Length;
            for (int i = 0; i < timerHideObjectSize; i++)
            {
                GameObject hidetimer;
                if (i == 0)
                {
                    hidetimer = GameObject.Find("TimerHide");
                }
                else
                {
                    hidetimer = GameObject.Find("TimerHide (" + i + ")");
                }
                hidetimer.GetComponent<BoxCollider>().enabled = false;
            }
            GameObject timerStartObject = GameObject.FindGameObjectWithTag("TimerStart");
            timerStartObject.GetComponent<BoxCollider>().enabled = true;
            timerStartObject.GetComponent<MeshRenderer>().enabled = true;
            hudTimerIcon.SetActive(true);
            hudTimerText.text = "0:00:00";
        }
        resetCollectables();
        resetRareCollectables();
        loadPlayer();
        collectableText.text = collectables.Count.ToString() + " / " + (int)GameObject.FindGameObjectsWithTag("Collectable").Length;
        hudCollectableText.text = collectables.Count.ToString() + " / " + (int)GameObject.FindGameObjectsWithTag("Collectable").Length;
        rareCollectableText.text = rareCollectables.Count.ToString() + " / " +
            (int)GameObject.FindGameObjectsWithTag("RareCollectible").Length;
        hudRareCollectableText.text = rareCollectables.Count.ToString() + " / " +
            (int)GameObject.FindGameObjectsWithTag("RareCollectible").Length;
        resetHealth();
        StartCoroutine(showHud());
    }
    public void resetHealth()
    {
        healthFull1.SetActive(true);
        healthFull2.SetActive(true);
        healthFull3.SetActive(true);
        healthEmpty1.SetActive(false);
        healthEmpty2.SetActive(false);
        healthEmpty3.SetActive(false);
        health = 3;
        player.health = 3;
        player.UnDie();
        hudHealthFull1.SetActive(true);
        hudHealthEmpty1.SetActive(false);
        hudHealthFull2.SetActive(true);
        hudHealthEmpty2.SetActive(false);
        hudHealthFull3.SetActive(true);
        hudHealthEmpty3.SetActive(false);
    }
    IEnumerator showHud()
    {
        hud.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        hud.SetActive(false);
    }
}
