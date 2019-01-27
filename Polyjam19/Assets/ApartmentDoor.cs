using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApartmentDoor : BaseDoor
{
    public Apartment apartment;
    public Apartment corridor;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Enter(Player player)
    {
        apartment.Enter(player);
        spriteRenderer.enabled = false;
    }

    public override void Exit(Player player)
    {
        corridor.Enter(player);
        spriteRenderer.enabled = true;
    }
}
