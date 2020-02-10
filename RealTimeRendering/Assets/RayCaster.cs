using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour
{
    // Start is called before the first frame update
    public Texture2D canvas;
    public int width = 512;
    public int height = 512;
    private void Awake()
    {
        canvas = new Texture2D(width, height);

    }
    void Start()
    {
        for(int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Ray currentRay = Camera.main.ViewportPointToRay(new Vector3((float)i/width, (float)j /height, Camera.main.nearClipPlane));

                RaycastHit hitInfo;

                bool didHit = Physics.Raycast(currentRay, out hitInfo);

                if(didHit)
                {
                    ShaderScript currentShader = hitInfo.transform.GetComponent<ShaderScript>();
                    canvas.SetPixel(i, j, currentShader.calcColor(hitInfo, 0));
                }
                else
                {
                    canvas.SetPixel(i, j, Color.black);
                }
            }
        }
        canvas.Apply();
    }

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), canvas);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
