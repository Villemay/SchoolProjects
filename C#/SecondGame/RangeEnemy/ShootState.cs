using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : IRangedEnemyState
{

    private StateRangedEnemy enemy;

    public ShootState(StateRangedEnemy stateEnemy)
    {
        enemy = stateEnemy;
    }

    public void UpdateState()
    {
        shoot();
    }

    public void ToChaseState()
    {
        enemy.setStateToChase();
    }

    public void ToIdleState()
    {
        enemy.setStateToIdle();
    }
    public void ToShootState()
    {
        //do nothing we are already here
    }
    public void ToDeathState()
    {
        enemy.setStateToDeath();
    }

    public void shoot()
    {
        enemy.stopEnemyMovement();
        Vector3 targetPosition = new Vector3(enemy.getTargetPositionX(), enemy.getEnemyPositionY(), enemy.getTargetPositionZ());
        enemy.lookAtTarget(targetPosition);

        if(enemy.getNextShootTime() < Time.time)
        {
            enemy.getAnimator().SetTrigger("isAttack");
        }


        if (enemy.getTargetDistance() > enemy.getShootingRange())
        {
            ToChaseState();
        }
    }

}
