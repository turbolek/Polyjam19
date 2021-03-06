﻿using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingData", menuName = "ScriptableObjects/BuildingData")]
public class BuildingData : ScriptableObject
{
    [SerializeField]
    private List<FloorData> floors = null;

    public List<FloorData> Segments 
    {
        get { return floors; }
    }

    [SerializeField]
    private BuildingSegment apartmentSalonSegment = null;
    public BuildingSegment ApartmentSalonSegment
    {
        get { return apartmentSalonSegment; }
    }

    [SerializeField]
    private BuildingSegment apartmentKitchenSegment = null;
    public BuildingSegment ApartmentKitchenSegment
    {
        get { return apartmentKitchenSegment; }
    }

    [SerializeField]
    private BuildingSegment apartmentBathroomSegment = null;
    public BuildingSegment ApartmentBathroomSegment
    {
        get { return apartmentBathroomSegment; }
    }

    [SerializeField]
    private BuildingSegment caretakerRoomSegment = null;
    public BuildingSegment CaretakerRoomSegment
    {
        get { return caretakerRoomSegment; }
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
    private BuildingSegment roofLeft = null;
    public BuildingSegment RoofLeft
    {
        get { return roofLeft; }
    }

    [SerializeField]
    private BuildingSegment roofRight = null;
    public BuildingSegment RoofRight
    {
        get { return roofRight; }
    }

    [SerializeField]
    private BuildingSegment roofCenterWide = null;
    public BuildingSegment RoofCenterWide
    {
        get { return roofCenterWide; }
    }

    [SerializeField]
    private BuildingSegment roofCenterNarrow = null;
    public BuildingSegment RoofCenterNarrow
    {
        get { return roofCenterNarrow; }
    }

    [SerializeField]
    private BuildingSegment emptySegment = null;
    public BuildingSegment EmptySegment
    {
        get { return emptySegment; }
    }

    public BuildingSegment GetSegmentPrefab(FloorSegmentType segmentType)
    {
        switch (segmentType)
        {
            case FloorSegmentType.empty:
                return EmptySegment;
            case FloorSegmentType.apartment:
                return GetRandomApartment();
            case FloorSegmentType.stairsUp:
                return stairsUpSegment;
            case FloorSegmentType.stairsDown:
                return stairsDownSegment;
            case FloorSegmentType.caretakerRoom:
                return caretakerRoomSegment;
            case FloorSegmentType.roofLeft:
                return RoofLeft;
            case FloorSegmentType.roofRight:
                return RoofRight;
            case FloorSegmentType.roofCenterWide:
                return RoofCenterWide;
            case FloorSegmentType.roofCenterNarrow:
                return RoofCenterNarrow;
            default:
                return null;
        }
    }

    List<ApartmentType> availableApartments = new List<ApartmentType>();

    private void RefreshAvailableApartments()
    {
        int typesCount = Enum.GetNames(typeof(ApartmentType)).Length;
        for (int i = 0; i < typesCount; ++i)
        {
            availableApartments.Add((ApartmentType)(i));
        }
    }
    
    private BuildingSegment GetRandomApartment()
    {
        if (availableApartments.Count == 0)
        {
            RefreshAvailableApartments();
        }

        int randomId = UnityEngine.Random.Range(0, availableApartments.Count);
        ApartmentType type = availableApartments[randomId];
        availableApartments.RemoveAt(randomId);

        switch (type)
        {
            case ApartmentType.salon:
                return ApartmentSalonSegment;
            case ApartmentType.kitchen:
                return ApartmentKitchenSegment;
            case ApartmentType.bathroom:
                return ApartmentBathroomSegment;
            default:
                return null;
        }
    }


}

[System.Serializable]
public struct FloorData
{
    public List<FloorSegmentType> segments;
}


public enum FloorSegmentType
{
    empty = 0,
    apartment = 1,
    stairsUp = 2,
    stairsDown = 3,
    caretakerRoom = 4,
    roofLeft = 5,
    roofRight = 6,
    roofCenterWide = 7,
    roofCenterNarrow = 8,
}

public enum ApartmentType
{
    salon = 0,
    kitchen = 1,
    bathroom =2,
}


