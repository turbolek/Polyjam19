using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApartmentDoor : BaseDoor
{
    public Apartment apartment;
    public Apartment corridor;

    public override void Enter(Player player)
    {
        apartment.Enter(player);
    }

    public override void Exit(Player player)
    {
        corridor.Enter(player);

    }
}
