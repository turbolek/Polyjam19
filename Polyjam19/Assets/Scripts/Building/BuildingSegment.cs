using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSegment : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer roomRenderer = null;

    [SerializeField]
    private SpriteRenderer corridorRenderer = null;


    [SerializeField]
    private Apartment apartmentRoom = null;
    [SerializeField]
    private Apartment apartmentCorridor = null;

    public float SegmentWidth
    {
        get
        {
            return corridorRenderer.bounds.size.x;
        }
    }

    public float SegmentHeight
    {
        get
        {
            return roomRenderer.bounds.size.y + corridorRenderer.bounds.size.y;
        }
    }

    public void SetApartmentRoomBounds(float min, float max)
    {
        if (apartmentRoom ==null)
        {
            return;
        }
        apartmentRoom.leftBorder = min;
        apartmentRoom.rightBorder = max;
    }

    public void SetApartmentCorridorBounds(float min, float max)
    {
        apartmentCorridor.leftBorder = min;
        apartmentCorridor.rightBorder = max;
    }
}
