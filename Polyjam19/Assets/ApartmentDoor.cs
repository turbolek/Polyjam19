using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApartmentDoor : BaseDoor
{
    public Apartment apartment;
    public Apartment corridor;
    public Sprite openedSprite;
    SpriteRenderer spriteRenderer;
    Sprite closedSprite;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        closedSprite = spriteRenderer.sprite;
    }

    public override void Enter(Player player)
    {
        apartment.Enter(player);
        spriteRenderer.sprite = openedSprite;
    }

    public override void Exit(Player player)
    {
        corridor.Enter(player);
        spriteRenderer.sprite = closedSprite;
    }
}
