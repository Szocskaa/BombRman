using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostPU : MonoBehaviour
{
    public SkinnedMeshRenderer[] MeshR;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerObject"))
        {
            StartCoroutine(FadeGhostCoroutine(other.gameObject));
            foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = false;
            }
        }
    }

    private IEnumerator FadeGhostCoroutine(GameObject player)
    {
        Debug.Log("Fading ghost");
        player.layer = LayerMask.NameToLayer("Ghost");
        foreach (SkinnedMeshRenderer meshR in MeshR)
        {
            Color color = meshR.sharedMaterial.color;
            color.a = 0.5f;
            meshR.sharedMaterial.color = color;
        }

        yield return new WaitForSeconds(7);

        StartCoroutine(BlinkGhostCoroutine(player));
    }

    private IEnumerator BlinkGhostCoroutine(GameObject player)
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
        Collider[] overlaps = Physics.OverlapSphere(player.transform.position, 0f, LayerMask.GetMask("Ghost"));
        if (overlaps.Length > 0)
        {
            Destroy(player);
            Debug.Log("Ghost is dead");
        }
        foreach (SkinnedMeshRenderer meshR in MeshR)
        {
            Color color = meshR.sharedMaterial.color;
            color.a = 1.0f;
            meshR.sharedMaterial.color = color;
        }
        player.layer = LayerMask.NameToLayer("Default");
        Destroy(gameObject);
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