using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CurveController : MonoBehaviour
{
    [Range(-1f, 1f)]
    [SerializeField]
    float curveX = 0;

    [Range(-1f, 1f)]
    [SerializeField]
    float curveY = 0;

    int _CurveXID;
    int _CurveYID;
    private void OnEnable()
    {
        _CurveXID = Shader.PropertyToID("_CurveX");
        _CurveYID = Shader.PropertyToID("_CurveY");
        Shader.SetGlobalFloat(_CurveXID, curveX);
        Shader.SetGlobalFloat(_CurveYID, curveY);
    }

    private void Update()
    {
        if (Application.isEditor)
        {
            Shader.SetGlobalFloat(_CurveXID, curveX);
            Shader.SetGlobalFloat(_CurveYID, curveY);
        }
    }
}
