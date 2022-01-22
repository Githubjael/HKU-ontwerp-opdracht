using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute()]
public class HeightMapSettings : UpdatableData
{
    public NoiseSettings noiseSettings;

    public float heightMultiplier;
    public AnimationCurve heightCurve;

    public float minHeight
    {
        get
        {
            return  heightMultiplier * heightCurve.Evaluate(0);
        }
    }
    public float maxHeight
    {
        get
        {
            return heightMultiplier * heightCurve.Evaluate(1);
        }
    }

#if UNITY_EDITOR

    protected override void OnValidate()
    {
        noiseSettings.ValidateValue();
        base.OnValidate();
    }
#endif
}
