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
    private SpriteRenderer hallCorridorRenderer = null;

    
    public Apartment apartmentRoom = null;
    public Apartment apartmentCorridor = null;

    public float SegmentWidth;
    
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

    public float GetSegmentWidth()
    {
        return GetRoomWidth() + GetHallWidth();
    }

    public float GetRoomWidth()
    {
        if (roomRenderer == null)
        {
            return 0;
        }
        return roomRenderer.bounds.size.x;
    }

    public float GetHallWidth()
    {
        if (hallCorridorRenderer == null)
        {
            return 0;
        }
        return hallCorridorRenderer.bounds.size.x;
    }

    public float GetSegmentHeight()
    {
        return roomRenderer.bounds.size.y + corridorRenderer.bounds.size.y;
    }
}
