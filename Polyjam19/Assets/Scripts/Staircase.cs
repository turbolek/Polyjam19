using UnityEngine;

public class Staircase : MonoBehaviour
{
    [SerializeField]
    private Sprite stairsUpsprite = null;
    [SerializeField]
    private Sprite stairsDownSprite = null;

    [SerializeField]
    private Staircase relatedStairs = null;

    [SerializeField]
    private Transform spawnPoint = null;

    public void OnEnter(GameObject target)
    {
        //może jakaś animka
        relatedStairs.OnExit(target);
    }

    public void OnExit(GameObject target)
    {
        target.transform.position = spawnPoint.position;
    }
}
