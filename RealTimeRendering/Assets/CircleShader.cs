using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleShader : ShaderScript
{

    public override Color calcColor(RaycastHit hitInfo, int recursionDepth)
    {
        Color fragColor;
        Vector2 circlePos = new Vector2(.5f, .5f);
        float radius = .25f;
        
        if((hitInfo.textureCoord - circlePos).magnitude < radius)
        {
            fragColor = Color.red;
        }
        else
        {
            fragColor = hitInfo.transform.GetComponent<Renderer>().sharedMaterial.color;
        }
        return fragColor;
    }
}
