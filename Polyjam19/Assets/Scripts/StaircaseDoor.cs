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
    private Transform spawnPoint = null;

    public override void Enter(Player player)
    {
        //tu może animacja a potem akcja
        relatedStairs.Exit(player);
    }

    public override void Exit(Player player)
    {
        player.transform.position = spawnPoint.transform.position;
        //skala itp
    }
}
