using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FloorData", menuName = "ScriptableObjects/FloorData")]
public class Floor : ScriptableObject
{
    [SerializeField]
    private List<FloorSegmentType> segments = null;
    public List<FloorSegmentType> Segments 
    {
        get { return segments; }
    }

    [SerializeField]
    private BuildingSegment apartmentSegment = null;
    public BuildingSegment ApartmentSegment
    {
        get { return apartmentSegment; }
    }

    [SerializeField]
    private BuildingSegment stairsUpSegment = null;
    public BuildingSegment StairsUpSegment
    {
        get { return stairsUpSegment; }
    }

    [SerializeField]
    private BuildingSegment stairsDownSegment = null;
    public BuildingSegment StairsDownSegment
    {
        get { return stairsDownSegment; }
    }

    [SerializeField]
    private BuildingSegment emptysegment = null;
    public BuildingSegment EmptySegment
    {
        get { return emptysegment; }
    }

    public BuildingSegment GetSegmentPrefab(FloorSegmentType segmentType)
    {
        switch (segmentType)
        {
            case FloorSegmentType.empty:
                return EmptySegment;
            case FloorSegmentType.apartment:
                return apartmentSegment;
            case FloorSegmentType.stairsUp:
                return stairsUpSegment;
            case FloorSegmentType.stairsDown:
                return stairsDownSegment;
            default:
                return null;
        }
    }
}




public enum FloorSegmentType
{
    empty = 0,
    apartment = 1,
    stairsUp = 2,
    stairsDown = 3,
}


