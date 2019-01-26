using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsBuildingSegment : BuildingSegment
{
    [SerializeField]
    private FloorSegmentType segmentType = FloorSegmentType.stairsUp;
    public FloorSegmentType SegmentType
    {
        get { return segmentType; }
    }

    public bool isTaken = false;

    [SerializeField]
    private StaircaseDoor door = null;
    public StaircaseDoor Door
    {
        get { return door; }
    }

    public void SetRelatedStairs(StaircaseDoor relatedDoor)
    {
        Door.SetRelatedDoor(relatedDoor);
    }
}

