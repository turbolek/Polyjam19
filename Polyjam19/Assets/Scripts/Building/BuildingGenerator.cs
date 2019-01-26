using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGenerator : MonoBehaviour
{
    [SerializeField]
    private Floor floor = null;

    [SerializeField]
    [Range(1, 100)]
    private int floorsNumber = 5;

    [SerializeField]
    private Transform buildingParent = null;

    private List<List<BuildingSegment>> building = null;

    private void Start()
    {
        GenerateBuilding();
    }

    private void GenerateBuilding()
    {
        if(building == null)
        {
            building = new List<List<BuildingSegment>>();
        }
        building.Clear();
        for (int i = 0; i < floorsNumber; ++i)
        {
            building.Add(GenerateFloor(i));
        }

        BindStairs();
    }

    private List<BuildingSegment> GenerateFloor (int level)
    {
        List<BuildingSegment> newFloor = new List<BuildingSegment>();
        float floorWidth = 0;
        for (int i = 0; i < floor.Segments.Count; ++i)
        {
            FloorSegmentType segmentType = floor.Segments[i];
            if (level == 0 && segmentType == FloorSegmentType.stairsDown)
            {
                segmentType = FloorSegmentType.empty;
            }
            if (level == floorsNumber - 1 && segmentType == FloorSegmentType.stairsUp)
            {
                segmentType = FloorSegmentType.empty;
            }

            BuildingSegment segment = Instantiate(floor.GetSegmentPrefab(segmentType), buildingParent);
            segment.transform.localPosition = new Vector3(floorWidth + segment.SegmentWidth / 2, ((float)level + .5f) * segment.SegmentHeight);
            newFloor.Add(segment);
            floorWidth += segment.SegmentWidth;
        }

        return newFloor;
    }

    private void BindStairs()
    {
        for (int i = 0; i < building.Count; ++i)
        {
            for (int j = 0; j < building[i].Count; ++j)
            {
                if (building[i][j] is StairsBuildingSegment)
                {
                    StairsBuildingSegment segment = (StairsBuildingSegment)building[i][j];
                    if (segment.SegmentType == FloorSegmentType.stairsUp)
                    {
                        segment.SetRelatedStairs(FindFreeStairsDown(i+1));
                    }
                    if (segment.SegmentType == FloorSegmentType.stairsDown)
                    {
                        segment.SetRelatedStairs(FindFreeStairsUp(i - 1));
                    }
                }
            }
        }
    }

    private StaircaseDoor FindFreeStairsUp(int level)
    {
        for (int i = 0; i < building[level].Count; ++i)
        {
            if (building[level][i] is StairsBuildingSegment)
            {
                StairsBuildingSegment segment = (StairsBuildingSegment)building[level][i];
                if (segment.SegmentType == FloorSegmentType.stairsUp && !segment.isTaken)
                {
                    segment.isTaken = true;
                    return segment.Door;
                }
            }
        }
        return null;
    }

    private StaircaseDoor FindFreeStairsDown(int level)
    {
        for (int i = 0; i < building[level].Count; ++i)
        {
            if (building[level][i] is StairsBuildingSegment)
            {
                StairsBuildingSegment segment = (StairsBuildingSegment)building[level][i];
                if (segment.SegmentType == FloorSegmentType.stairsDown && !segment.isTaken)
                {
                    segment.isTaken = true;
                    return segment.Door;
                }
            }
        }
        return null;
    }
}


