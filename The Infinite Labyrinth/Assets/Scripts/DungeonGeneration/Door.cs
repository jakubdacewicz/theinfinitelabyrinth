using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DoorType
    {
        top,
        left,
        right,
        bottom
    }

    public DoorType type;
}
