using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplacementControll : MonoBehaviour
{
    public float m_MaxDisplacement = 1.0f;
    [Range(1.0f, 15.0f)]
    public float m_MetallicLerpVelocity = 1.0f;

    [Range(1.0f, 15.0f)]
    public float m_ColorLerpVelocity = 1.0f;

    private float m_DisplacementAmount = 0.0f;

    private float m_ColorLerp = 0.0f;
    private float m_MetallicAmount = 0.0f;

    public ParticleSystem m_ExplosionParticles;
    MeshRenderer m_MeshRenderer;

    private bool m_DisplaceIn = false;

    void Start()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();
    }


    void Update()
    {

        if (m_DisplaceIn)
        {
            MetallicIn();
            DisplaceIn();
        }
        else
        {
            DisplaceOut();
            MetallicOut();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_DisplaceIn = !m_DisplaceIn;

            if (m_ExplosionParticles)
            {
                m_ExplosionParticles.Play();
            }
            
        }
    }

    private void DisplaceIn() 
    {
        m_DisplacementAmount = Mathf.Lerp(m_DisplacementAmount, m_MaxDisplacement, Time.deltaTime);
        m_MeshRenderer.material.SetFloat("_Amount", m_DisplacementAmount);
    }

    private void DisplaceOut()
    {
        m_DisplacementAmount = Mathf.Lerp(m_DisplacementAmount, 0, Time.deltaTime);
        m_MeshRenderer.material.SetFloat("_Amount", m_DisplacementAmount);
    }

    private void MetallicIn() 
    {
        m_MetallicAmount = Mathf.SmoothStep(m_MetallicAmount, 1, Time.deltaTime * m_MetallicLerpVelocity);
        m_MeshRenderer.material.SetFloat("_Metallic", m_MetallicAmount);

    }

    private void MetallicOut()
    {
        m_MetallicAmount = Mathf.SmoothStep(m_MetallicAmount, 0, Time.deltaTime * m_MetallicLerpVelocity);
        m_MeshRenderer.material.SetFloat("_Metallic", m_MetallicAmount);
    }

    private void LerpColorIn()
    {
        m_ColorLerp = Mathf.SmoothStep(m_ColorLerp, 1, Time.deltaTime * m_ColorLerpVelocity);
        m_MeshRenderer.material.SetFloat("_ColorLerp", m_ColorLerp);
    }

    private void LerpColorOut()
    {
        m_ColorLerp = Mathf.SmoothStep(m_ColorLerp, 0, Time.deltaTime * m_ColorLerpVelocity);
        m_MeshRenderer.material.SetFloat("_ColorLerp", m_ColorLerp);
    }

    private void Spazm() 
    {
        m_DisplacementAmount += m_MaxDisplacement;
        m_ColorLerp = 1.0f;
        m_MetallicAmount = 1.0f;
        //Use DisplaceOut/MetallicOut in the update for the effect
    }
}
