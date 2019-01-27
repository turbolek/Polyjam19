using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGenerator : MonoBehaviour
{
    [SerializeField]
    private BuildingData floor = null;

    [SerializeField]
    [Range(1, 100)]
    private int floorsNumber = 5;

    [SerializeField]
    private Transform buildingParent = null;

    private List<List<BuildingSegment>> building = null;
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        GenerateBuilding();
        SpawnPlayer();
        SpawnItems();

    }

    void SpawnPlayer()
    {
        GameObject playerGO = Instantiate(gameManager.playerPrefab);
        Player player = playerGO.GetComponent<Player>();
        player.Init();
        BuildingSegment buildingSegment = building[0][1];
        buildingSegment.apartmentRoom.Enter(player);
        player.transform.position = buildingSegment.apartmentRoom.floorTransform.position;
    }

    void SpawnItems()
    {
        for (int i = 0; i < gameManager.itemPrefabs.Length; i++)
        {
            GameObject itemGO = Instantiate(gameManager.itemPrefabs[i]);
            BuildingSegment buildingSegment = building[0][1];
            itemGO.transform.position = buildingSegment.apartmentRoom.floorTransform.position;
            itemGO.transform.Translate(new Vector3((float)2* (1-i), 0f, 0f));
        }
    }

    private void GenerateBuilding()
    {
        if (building == null)
        {
            building = new List<List<BuildingSegment>>();
        }
        building.Clear();
        for (int i = 0; i < floor.Segments.Count; ++i)
        {
            building.Add(GenerateFloor(i));
        }

        BindStairs();
    }

    private List<BuildingSegment> GenerateFloor(int level)
    {
        List<BuildingSegment> newFloor = new List<BuildingSegment>();
        float floorWidth = 0;
        for (int i = 0; i < floor.Segments[level].segments.Count; ++i)
        {
            FloorSegmentType segmentType = floor.Segments[level].segments[i];
            if (level == 0 && segmentType == FloorSegmentType.stairsDown)
            {
                segmentType = FloorSegmentType.empty;
            }
            if (level == floorsNumber - 1 && segmentType == FloorSegmentType.stairsUp)
            {
                segmentType = FloorSegmentType.empty;
            }

            BuildingSegment segment = Instantiate(floor.GetSegmentPrefab(segmentType), buildingParent);
            segment.transform.localPosition = new Vector3(floorWidth + segment.GetRoomWidth() / 2, ((float)level + .5f) * segment.GetSegmentHeight());
            segment.SetApartmentRoomBounds(segment.transform.position.x + segment.GetHallWidth() / 2 - segment.GetSegmentWidth() / 2, segment.transform.position.x + segment.GetHallWidth() / 2 + segment.GetSegmentWidth() / 2);

            newFloor.Add(segment);
            floorWidth += segment.GetSegmentWidth();
        }

        for (int i = 0; i < newFloor.Count; ++i)
        {
            newFloor[i].SetApartmentCorridorBounds(0, floorWidth);
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
                        segment.SetRelatedStairs(FindFreeStairsDown(i + 1));
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

    public Apartment GetUpperNeighbour(Apartment apartment)
    {
        BuildingSegment segment = apartment.GetComponentInParent<BuildingSegment>();
        int floorIndex = GetFloorIndex(segment);
        if (floorIndex < 0)
            return null;
        int apartmentIndex = GetApartmentIndexInFloor(floorIndex, segment);
        if (apartmentIndex < 0)
            return null;

        if (building.Count <= floorIndex + 1)
            return null;

        BuildingSegment upperNeighbourSegment = building[floorIndex + 1][apartmentIndex];
        return upperNeighbourSegment.GetComponentInChildren<Apartment>();
    }

    int GetFloorIndex(BuildingSegment segment)
    {
        for (int i = 0; i < building.Count; i++)
        {
            List<BuildingSegment> floor = building[i];
            if (floor.Contains(segment))
                return i;
        }
        return -1;
    }

    int GetApartmentIndexInFloor(int floorIndex, BuildingSegment segment)
    {
        List<BuildingSegment> floor = building[floorIndex];
        for (int i = 0; i < floor.Count; i++)
        {
            if (floor[i] == segment)
                return i;
        }
        return -1;
    }
}


