using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaircaseDoor : BaseDoor
{
    [SerializeField]
    private Sprite stairsUpsprite = null;
    [SerializeField]
    private Sprite stairsDownSprite = null;

    [SerializeField]
    private StaircaseDoor relatedStairs = null;

    [SerializeField]
    private Apartment corridor = null;

    public override void Enter(Player player)
    {
        if (relatedStairs == null)
        {
            return;
        }
        relatedStairs.Exit(player);
    }

    public override void Exit(Player player)
    {
        corridor.Enter(player);
    }

    public void SetRelatedDoor(StaircaseDoor door)
    {
        relatedStairs = door;
    }
}
