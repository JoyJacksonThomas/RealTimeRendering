using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LambertDiffuseShader : ShaderScript
{
    public Texture2D mainTexture;
    public Light mainLight;

    public override Color calcColor(RaycastHit hitInfo, int recursionDepth)
    {
        if (mainLight == null)
        {
            Debug.Log("Light not connected to shader");
            return Color.black;
        }

        Color fragColor;

        if (mainTexture != null)
        {
            fragColor = mainTexture.GetPixelBilinear(hitInfo.textureCoord.x, hitInfo.textureCoord.y);
        }
        else
        {
            fragColor = Color.black;
        }

        Mesh mesh = (hitInfo.collider as MeshCollider).sharedMesh;
        Vector3[] normals = mesh.normals;
        int[] triangles = mesh.triangles;
        Vector3 n0 = normals[triangles[hitInfo.triangleIndex * 3 + 0]];
        Vector3 n1 = normals[triangles[hitInfo.triangleIndex * 3 + 1]];
        Vector3 n2 = normals[triangles[hitInfo.triangleIndex * 3 + 2]];
        Vector3 baryCenter = hitInfo.barycentricCoordinate;
        Vector3 interpolatedNormal = n0 * baryCenter.x + n1 * baryCenter.y + n2 * baryCenter.z;
        interpolatedNormal = interpolatedNormal.normalized;

        float dotProduct = 0.0f;
        float insideRange = 0.0f;
        float insideShadow = 0.0f;
        Vector3 directionToLight = new Vector3(0.0f, 0.0f, 0.0f);

        switch (mainLight.type)
        {
            case LightType.Directional:
                directionToLight = -mainLight.transform.forward;
                dotProduct = Vector3.Dot(directionToLight, interpolatedNormal);
                break;
            case LightType.Point:
                Vector3 distToLight = mainLight.transform.position - hitInfo.point;
                dotProduct = Vector3.Dot(distToLight.normalized, interpolatedNormal);
                if (distToLight.magnitude <= mainLight.range)
                {
                    insideRange = (distToLight.magnitude * distToLight.magnitude) / (mainLight.range * mainLight.range);
                    insideRange = 1.0f - insideRange;
                }
                directionToLight = distToLight.normalized;
                break;
        }


        insideShadow = IsShadowed(hitInfo.point, directionToLight);



        fragColor = fragColor * mainLight.intensity * insideRange * (1.0f - insideShadow) * Mathf.Max(dotProduct, 0.0f); //Make sure we dont go negative
        fragColor.a = 1;
        //Make sure we dont go negative
        return fragColor;
    }

    float IsShadowed(Vector3 point, Vector3 directionToLight)
    {
        if (Physics.Raycast(point, directionToLight))
        {
            return 1.0f;
        }
        else
            return 0.0f;

    }
}
