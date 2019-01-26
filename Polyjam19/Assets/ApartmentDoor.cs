using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApartmentDoor : BaseDoor
{
    public Apartment apartment;

    public override void Enter(Player player)
    {
        player.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
        player.leftBorder = apartment.leftBorder;
        player.rightBorder = apartment.rightBorder;
    }

    public override void Exit(Player player)
    {
        player.transform.localScale = new Vector3(1f, 1f, 1f);
        player.leftBorder = -1.5f;
        player.rightBorder = 4.5f;
    }
}
