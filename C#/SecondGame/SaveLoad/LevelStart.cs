using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart : MonoBehaviour
{
    [SerializeField] private PlayerUpgrades pU;
    [SerializeField] GameObject playerMelee;
    [SerializeField] GameObject playerRanged;
    [SerializeField] GameObject playerCamera;
    [SerializeField] private GameObject enemySpawner;
    [SerializeField] private List<GameObject> enemies;

    GameObject gameInfo;
    private GameObject _player;
    [SerializeField] GameObject gameTimer;
    GameObject tempUpgrades;

    private void Awake()
    {
        gameInfo = GameObject.Find("GameInfo");
        enemySpawner = GameObject.Find("EnemySpawner");
        gameTimer = GameObject.Find("GameTimer");
        tempUpgrades = GameObject.Find("temporaryIcons");
    }



    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject controls = GameObject.Find("InputManager");
            controls.GetComponent<InputManager>().SetKeys();
            GameObject keybindsUI = GameObject.Find("KeybindGuide");
            keybindsUI.GetComponent<AssingKeybindsToUI>().Setkeys();

            if (SaveLoad.loadPlayer() != null)
            {
                //Start timer

                if (gameTimer != null)
                {
                    gameTimer.GetComponent<GameTimer>().setTimer(true);
                }


                playerMelee.gameObject.GetComponent<PlayerUpgrades>().loadPlayer();
                if (playerMelee.gameObject.GetComponent<PlayerUpgrades>().GetRanged() == false)
                {
                    //MELEE
                    pU = playerMelee.gameObject.GetComponent<PlayerUpgrades>();
                    playerRanged.SetActive(false);
                    _player = playerMelee;
                    playerCamera.GetComponent<CameraController>().SetTarget(playerMelee);
                    GameObject levelEnd = GameObject.Find("LevelEnd");
                    levelEnd.GetComponent<LevelEnd>().SetPU(playerMelee);
                    levelEnd.GetComponent<LevelEnd>().SetPlayer(playerMelee);
                    SetEnemyTargetToMelee();
                    tempUpgrades.GetComponent<settingTempUpgrades>().SetPlayer(playerMelee);
                    tempUpgrades.GetComponent<settingTempUpgrades>().SetRanged(false);
                }
                else
                {
                    //RANGED
                    playerRanged.SetActive(true);
                    pU = playerRanged.gameObject.GetComponent<PlayerUpgrades>();
                    playerMelee.SetActive(false);
                    pU.loadPlayer();
                    _player = playerRanged;
                    playerCamera.GetComponent<CameraController>().SetTarget(playerRanged);
                    GameObject levelEnd = GameObject.Find("LevelEnd");
                    levelEnd.GetComponent<LevelEnd>().SetPU(playerRanged);
                    levelEnd.GetComponent<LevelEnd>().SetPlayer(playerRanged);
                    SetEnemyTargetToRanged();
                    tempUpgrades.GetComponent<settingTempUpgrades>().SetPlayer(playerRanged);
                    tempUpgrades.GetComponent<settingTempUpgrades>().SetRanged(true);
                }
                pU.RefreshPermUpgrades();

            }
            else
            {
                //MELEE
                playerRanged.SetActive(false);
                pU = playerMelee.gameObject.GetComponent<PlayerUpgrades>();
                pU.SetRanged(false);
                GameObject levelEnd = GameObject.Find("LevelEnd");
                levelEnd.GetComponent<LevelEnd>().SetPU(playerMelee);
                levelEnd.GetComponent<LevelEnd>().SetPlayer(playerMelee);
                SetEnemyTargetToMelee();
                tempUpgrades.GetComponent<settingTempUpgrades>().SetPlayer(playerMelee);
                tempUpgrades.GetComponent<settingTempUpgrades>().SetRanged(false);
            }

            this.gameObject.SetActive(false);
        }
    }

    private void SetEnemyTargetToMelee()
    {
        enemies = enemySpawner.gameObject.GetComponent<EnemySpawner>().GetEnemyList();
        for(int i = 0; i < enemies.Count; i++)
        {
            //MELEE ENEMY
            if (enemies[i].gameObject.GetComponent<EnemyHealth>().GetIfRanged() == false)
            {
                enemies[i].GetComponent<MeleeEnemyAI>().SetTarget(playerMelee);
            }
            //RANGED ENEMY
            else if (enemies[i].gameObject.GetComponent<EnemyHealth>().GetIfRanged() == true)
            {
                enemies[i].GetComponent<StateRangedEnemy>().SetTarget(playerMelee);
            }
        }
    }

    private void SetEnemyTargetToRanged()
    {
        enemies = enemySpawner.gameObject.GetComponent<EnemySpawner>().GetEnemyList();
        for (int i = 0; i < enemies.Count; i++)
        {
            //MELEE ENEMY
            if (enemies[i].gameObject.GetComponent<EnemyHealth>().GetIfRanged() == false)
            {
                enemies[i].GetComponent<MeleeEnemyAI>().SetTarget(playerRanged);
            }
            //RANGED ENEMY
            else if (enemies[i].gameObject.GetComponent<EnemyHealth>().GetIfRanged() == true)
            {
                enemies[i].GetComponent<StateRangedEnemy>().SetTarget(playerRanged);
            }
        }
    }

}
