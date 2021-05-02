using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blit : MonoBehaviour
{
    public Material _transitionMaterial;
    void OnRenderImage(RenderTexture source, RenderTexture dest)
    {
        if (_transitionMaterial != null)
        {
            Graphics.Blit(source, dest, _transitionMaterial);
        }
        
    }
    
}
