using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherEnemy : EnemyController
{    // Update is called once per frame
    void Update()
    {
        base.Update();
        DestroyEnemyAndSpawnMana();
    }
}
