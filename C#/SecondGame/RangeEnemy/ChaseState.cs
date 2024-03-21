using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IRangedEnemyState
{

    private StateRangedEnemy enemy;

    public ChaseState(StateRangedEnemy stateEnemy)
    {
        enemy = stateEnemy;
    }

    public void UpdateState()
    {
        chase();
    }
    public void ToShootState()
    {
        enemy.setStateToShoot();
    }

    public void ToIdleState()
    {
        enemy.setStateToIdle();
    }
    public void ToChaseState()
    {
        //do nothing we are already here
    }

    public void ToDeathState()
    {
        enemy.setStateToDeath();
    }

    private void chase()
    {
        enemy.getAnimator().SetBool("isAgro", false);
        if (enemy.getTargetDistance() <= enemy.getShootingRange())
        {
            ToShootState();
        }
        else if(enemy.getTargetDistance() > enemy.getLoseAgroDistance())
        {
            ToIdleState();
        }
        if (enemy.getWait() == false)
        {
            enemy.setNavMeshToMove();
        }
    }

}
