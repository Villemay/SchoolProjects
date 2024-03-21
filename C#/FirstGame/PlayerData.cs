using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] position;
    public string[] collectableNames;
    public string[] checkpointNames;
    public string[] rareCollectableNames;
    public bool cleared = false;
    public int smlColAmount;
    public float BestTimerTime;
    public bool level3Cleared = false;


    public PlayerData(PlayerMovvement player)
    {
        //players position
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y + 6;
        position[2] = player.transform.position.z;
        //players collected collectables
        collectableNames = new string[player.collectables.Count];
        for (int i = 0; i < player.collectables.Count;i++)
        {
            collectableNames[i] = player.collectables[i];
        }
        //players collected checkpoints
        checkpointNames = new string[player.checkpoints.Count];
        for(int i = 0; i < player.checkpoints.Count; i++)
        {
            checkpointNames[i] = player.checkpoints[i];
        }
        //players collected rare collectables
        rareCollectableNames = new string[player.rareCollectables.Count];
        for(int i = 0; i < player.rareCollectables.Count; i++)
        {
            rareCollectableNames[i] = player.rareCollectables[i];
        }
        //if the player has cleared the level
        cleared = player.cleared;
        //players most collected small collectables 
        if (player.maxSmalColAmount > player.collectables.Count)
        {
            smlColAmount = player.maxSmalColAmount;
        }
        else
        {
            smlColAmount = player.collectables.Count;
        }
        //save the player best time
        BestTimerTime = player.bestTimerTime;
        level3Cleared = player.lvl3Cleared;
    }


}
