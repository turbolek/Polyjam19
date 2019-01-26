using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDoor : MonoBehaviour
{
    public abstract void Enter(Player player);
    public abstract void Exit(Player player);
}
