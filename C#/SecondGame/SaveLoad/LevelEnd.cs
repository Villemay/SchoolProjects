using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private PlayerUpgrades pU;
    GameObject gameInfo;
    private GameObject _player;
    [SerializeField] GameObject gameTimer;

    private void Start()
    {
        gameInfo = GameObject.Find("GameInfo");
        _player = GameObject.FindGameObjectWithTag("Player");

        gameTimer = GameObject.Find("GameTimer");

    }



    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (GameObject.Find("GameTimer") != null) 
            { 
                gameTimer.GetComponent<GameTimer>().setTimer(false); 
            }
            if (pU.GetFirstTimeInGarage() == true)
            {
                pU.SetFirstTimeInGarage(false);
            }
            //gameInfo.GetComponent<GameInfo>().hp(_player.GetComponent<PlayerHealth>().getCurrentHealth());
            pU.savePlayer();

            if (GameObject.Find("BackgroundMusic"))
            {
                GameObject.Find("BackgroundMusic").GetComponent<BackgroundMusic>().StopASoundWithFade();
            }


            this.gameObject.SetActive(false);
        }

        
    }
    public void SetPU(GameObject player)
    {
        pU = player.gameObject.GetComponent<PlayerUpgrades>();
    }
    public void SetPlayer(GameObject player)
    {
        _player = player;
    }

}
