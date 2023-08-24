using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GroundType
{
    dirt,
    grass,
    water
}

public class Ground : MonoBehaviour
{
    public GameObject placedGround = null;
    public GroundType type;
}
