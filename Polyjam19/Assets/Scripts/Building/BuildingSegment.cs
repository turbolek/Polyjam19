using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSegment : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer flatRenderer = null;

    [SerializeField]
    private SpriteRenderer corridorRenderer = null;

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
            return flatRenderer.bounds.size.y + corridorRenderer.bounds.size.y;
        }
    }
}
