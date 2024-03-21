using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRangedEnemyState
{
    void UpdateState();

    void ToIdleState();

    void ToChaseState();

    void ToShootState();

    void ToDeathState();

}
