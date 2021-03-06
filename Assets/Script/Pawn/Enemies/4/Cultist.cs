﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cultist : Enemy
{
    public override void DoSkill(Pawn target = null)
    {
        target = target != null ? target : GetCurrentTarget();
        if(target != null)
        {
            target.addBuff(AttributeType.MagicDefense, 3, 1);
        }
    }

    public override void OnActionBegin()
    {
        base.OnActionBegin();
        pawnType = PawnType.Monster;
        gm.hexMap.ProbeAttackTarget(currentCell);
        foreach(HexCell cell in gm.hexMap.GetAttackableTargets())
        {
            if(cell.pawn != null && cell.pawn.pawnType == PawnType.Enemy)
            {
                ((Enemy)cell.pawn).currentTarget = this;
            }
        }
        pawnType = PawnType.Enemy;
    }

    public override void InitPawn()
    {
        skillCounts = 1;
    }
}
