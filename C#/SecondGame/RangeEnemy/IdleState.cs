using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IRangedEnemyState
{
    private StateRangedEnemy enemy;

    public IdleState(StateRangedEnemy stateEnemy)
    {
        enemy = stateEnemy;
    }

    public void UpdateState()
    {
        idle();
    }

    public void ToChaseState()
    {
        enemy.ResetEnemyMovementSpeed();
        enemy.setStateToChase();
    }

    public void ToShootState()
    {
        enemy.setStateToShoot();
    }

    public void ToIdleState()
    {
        //do nothing we are already here
    }
    public void ToDeathState()
    {
        enemy.setStateToDeath();
    }
    public void idle()
    {
        enemy.stopEnemyMovement();
        enemy.getAnimator().SetBool("isMoving", false);
        if(enemy.getAgroRange() >= enemy.getTargetDistance())
        {
            enemy.getAnimator().SetBool("isAgro", true);
            
        }
    }

}
