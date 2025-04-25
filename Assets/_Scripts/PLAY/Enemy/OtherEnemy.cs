using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherEnemy : EnemyController
{    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        DestroyEnemyAndSpawnMana();
    }
}
