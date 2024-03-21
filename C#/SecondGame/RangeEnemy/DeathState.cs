using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : IRangedEnemyState
{
    private StateRangedEnemy enemy;

    public DeathState(StateRangedEnemy stateEnemy)
    {
        enemy = stateEnemy;
    }

    public void UpdateState()
    {
        enemy.stopEnemyMovement();


    }

    public void ToShootState()
    {

    }
    public void ToIdleState()
    {

    }
    public void ToChaseState()
    {

    }
    public void ToDeathState()
    {

    }



}
