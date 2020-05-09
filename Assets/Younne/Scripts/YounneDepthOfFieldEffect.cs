using UnityEngine;
using System;

//[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class YounneDepthOfFieldEffect : MonoBehaviour
{
    public Shader dofShader;

    [NonSerialized]
    Material dofMaterial;

    [Range(0.1f, 100f)]
    public float focusDistance = 10f;

    [Range(0.1f, 10f)]
    public float focusRange = 3f;

    const int circleOfConfusionPass = 0;

    private void Awake()
    {
        var camera = GetComponent<Camera>();
        if (camera != null)
        {
            camera.depthTextureMode = DepthTextureMode.Depth;
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (dofMaterial == null)
        {
            dofMaterial = new Material(dofShader);
            dofMaterial.hideFlags = HideFlags.HideAndDontSave;
        }

        dofMaterial.SetFloat("_FocusDistance", focusDistance);
        dofMaterial.SetFloat("_FocusRange", focusRange);

        Graphics.Blit(source, destination, dofMaterial, circleOfConfusionPass);
    }
}