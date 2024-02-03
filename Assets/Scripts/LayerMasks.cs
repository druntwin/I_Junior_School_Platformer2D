using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LayerMasks
{
    Ground = 1 << LayerIndexes.Ground,
    Player = 1 << LayerIndexes.Player,
    Enemies = 1 << LayerIndexes.Enemies,
}

public enum LayerIndexes
{
    Ground = 8,
    Player = 9,
    Enemies = 10
}
