using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderScript : MonoBehaviour
{
    virtual public Color calcColor(RaycastHit hitInfo, int recursionDepth) { return Color.black; }
}
