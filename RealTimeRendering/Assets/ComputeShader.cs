//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ComputeShader : ShaderScript
//{
//    public ComputeShader Shader;
//    public string kernelName = "CSMain";
//    public RenderTexture RenderTex;

//    // Use this for initialization

//    private int _kernel;
//    private RenderTexture _renderTex;
//    void Awake()
//    {
//        _kernel = Shader.FindKernel(kernelName);

//        //_renderTex = new RenderTexture (512, 512, 24);
//        _renderTex = RenderTex;
//        _renderTex.enableRandomWrite = true;
//        //_renderTex.Create ();

//        ExecComputeShader();
//    }


//    public void ExecComputeShader()
//    {

//        Shader.SetTexture(_kernel, "Result", _renderTex);
//        Shader.SetInt("TimeValue", (int)Time.timeSinceLevelLoad * 100);
//        Shader.Dispatch(_kernel, 512 / 8, 512 / 8, 1);
//    }
//}
