using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Noise
{
    public int seed;
    public abstract float GetNoiseMap(float x, float y, float scale = 1f);
}
