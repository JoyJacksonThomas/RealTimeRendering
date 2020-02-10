using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialShader : ShaderScript
{

    public override Color calcColor(RaycastHit hitInfo, int recursionDepth)
    {
        Material currentMat = hitInfo.transform.GetComponent<Renderer>().sharedMaterial;
        Color fragColor;

        if (currentMat.mainTexture == true)
            fragColor = (currentMat.mainTexture as Texture2D).GetPixelBilinear(hitInfo.textureCoord.x, hitInfo.textureCoord.y);
        else
            fragColor = Color.black;

        return fragColor;
    }
}