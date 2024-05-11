using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostPW : MonoBehaviour
{
    public SkinnedMeshRenderer[] MeshR;
    public bool igenpw = false;

    void Update()
{
    if (Input.GetKeyDown(KeyCode.Alpha2) || igenpw)
    {
        StartCoroutine(FadeGhostCoroutine());
    }
}

    private void FadeGhost()
    {
        
        
            Debug.Log("Fading ghost");
            foreach (SkinnedMeshRenderer meshR in MeshR)
            {
                Color color = meshR.material.color;
                color.a = 0.5f;
                meshR.material.color = color;
            }
        
    }
      private IEnumerator FadeGhostCoroutine()
    {
        Debug.Log("Fading ghost");
        gameObject.layer = LayerMask.NameToLayer("Ghost");
        foreach (SkinnedMeshRenderer meshR in MeshR)
        {
            Color color = meshR.sharedMaterial.color;
            color.a = 0.5f;
            meshR.sharedMaterial.color = color;
        }

        yield return new WaitForSeconds(7);

        StartCoroutine(BlinkGhostCoroutine());
    }

    private IEnumerator BlinkGhostCoroutine()
    {
        for (int i = 0; i < 21; i++)
        {
            foreach (SkinnedMeshRenderer meshR in MeshR)
            {
                Color color = meshR.material.color;
                color.a = (color.a == 1f) ? 0.5f : 1f;
                meshR.material.color = color;
            }

            yield return new WaitForSeconds(0.125f);
        }
        Collider[] overlaps = Physics.OverlapSphere(transform.position, 1f, LayerMask.GetMask("Ghost"));
        if (overlaps.Length > 0)
        {
            //Destroy(gameObject);
            Debug.Log("Ghost is dead");
        }
        gameObject.layer = LayerMask.NameToLayer("Default");
    }
    public void FadeGhostCoroutineTestable()
    {
        Debug.Log("Fading ghost");
        gameObject.layer = LayerMask.NameToLayer("Ghost");
        foreach (SkinnedMeshRenderer meshR in MeshR)
        {
            Color color = meshR.sharedMaterial.color;
            color.a = 0.5f;
            meshR.sharedMaterial.color = color;
        }
    }


    public void BlinkGhostImmediate()
    {
        for (int i = 0; i < 21; i++)
        {
            foreach (SkinnedMeshRenderer meshR in MeshR)
            {
                Color color = meshR.sharedMaterial.color;
                color.a = (color.a == 1f) ? 0.5f : 1f;
                meshR.sharedMaterial.color = color;
            }
        }
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

}