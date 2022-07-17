using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class ClippingController : MonoBehaviour
{
    public float m_MinClippingLevel = 0;
    public float m_MaxClippingLevel = 2.0f;

    [Range(1.0f, 5.0f)]
    public float m_ClippingVelocity = 2.0f;


    private Coroutine m_CurrentCoroutine = null;

    void Update()
    {
        if (m_CurrentCoroutine == null && Input.GetKeyDown(KeyCode.H))
        {
            m_CurrentCoroutine = StartCoroutine(HideModel(m_MinClippingLevel, m_MaxClippingLevel));
        }
        else if (m_CurrentCoroutine == null && Input.GetKeyDown(KeyCode.S)) 
        {
            m_CurrentCoroutine = StartCoroutine(ShowModel(m_MinClippingLevel, m_MaxClippingLevel));
        }
    }



    private IEnumerator HideModel(float minClippingLevel, float maxClippingLevel)
    {
        float clippingLevel = maxClippingLevel;

        while (clippingLevel > minClippingLevel)
        {

            clippingLevel -= Mathf.Abs(maxClippingLevel - minClippingLevel) / m_ClippingVelocity * Time.deltaTime;

            ////For the parent:

            SkinnedMeshRenderer parentSkinnedMeshRenderer = this.GetComponent<SkinnedMeshRenderer>();

            if (parentSkinnedMeshRenderer != null)
            {
                foreach (Material material in parentSkinnedMeshRenderer.materials)
                {
                    material.SetFloat("_ClippingLevel", clippingLevel);
                }
            }

            MeshRenderer parentMeshRenderer = this.GetComponent<MeshRenderer>();

            if (parentMeshRenderer != null)
            {
                foreach (Material material in parentMeshRenderer.materials)
                {
                    material.SetFloat("_ClippingLevel", clippingLevel);
                }
            }


            //For childs:

            //Skinned mesh renderer:
            SkinnedMeshRenderer[] skinnedMeshRenderers = this.GetComponentsInChildren<SkinnedMeshRenderer>();

            if (skinnedMeshRenderers.Length != 0)
            {
                foreach (SkinnedMeshRenderer smrenderer in skinnedMeshRenderers)
                {
                    foreach (Material material in smrenderer.materials)
                    {
                        //print("Skinned Mesh: " + clippingLevel);
                        material.SetFloat("_ClippingLevel", clippingLevel);
                    }
                }
            }

            //Mesh renderer
            MeshRenderer[] meshRenderers = this.GetComponentsInChildren<MeshRenderer>();

            if (meshRenderers.Length != 0)
            {
                foreach (MeshRenderer mrenderer in meshRenderers)
                {
                    foreach (Material material in mrenderer.materials)
                    {
                        //print("Renderer Mesh: " + clippingLevel);
                        material.SetFloat("_ClippingLevel", clippingLevel);
                    }
                }
            }

            yield return 0;
        }

        //print("Hide ended");
        m_CurrentCoroutine = null; 
    }

    private IEnumerator ShowModel(float minClippingLevel, float maxClippingLevel) 
    {
        float clippingLevel = minClippingLevel;

        while (clippingLevel < maxClippingLevel)
        {
            clippingLevel += Mathf.Abs(maxClippingLevel - minClippingLevel) / m_ClippingVelocity * Time.deltaTime;


            ////For the parent:

            SkinnedMeshRenderer parentSkinnedMeshRenderer = this.GetComponent<SkinnedMeshRenderer>();

            if (parentSkinnedMeshRenderer != null)
            {
                foreach (Material material in parentSkinnedMeshRenderer.materials)
                {
                    material.SetFloat("_ClippingLevel", clippingLevel);
                }
            }

            MeshRenderer parentMeshRenderer = this.GetComponent<MeshRenderer>();

            if (parentMeshRenderer != null)
            {
                foreach (Material material in parentMeshRenderer.materials)
                {
                    material.SetFloat("_ClippingLevel", clippingLevel);
                }
            }

            //For the children:

            SkinnedMeshRenderer[] skinnedMeshRenderers = this.GetComponentsInChildren<SkinnedMeshRenderer>();

            if (skinnedMeshRenderers.Length != 0)
            {
                foreach (SkinnedMeshRenderer smrenderer in skinnedMeshRenderers)
                {
                    foreach (Material material in smrenderer.materials)
                    {
                        //print("Skinned Mesh: " + clippingLevel);
                        material.SetFloat("_ClippingLevel", clippingLevel);
                    }
                }
            }

            MeshRenderer[] meshRenderers = this.GetComponentsInChildren<MeshRenderer>();

            if (meshRenderers.Length != 0)
            {
                foreach (MeshRenderer mrenderer in meshRenderers)
                {
                    foreach (Material material in mrenderer.materials)
                    {
                        //print("Renderer Mesh: " + clippingLevel);
                        material.SetFloat("_ClippingLevel", clippingLevel);
                    }
                }
            }

            yield return 0;
        }

        //print("Show ended");
        m_CurrentCoroutine = null;
    }
}
