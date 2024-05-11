using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using System.Reflection;


public class GhostPWTests
{
    private GameObject testGameObject;
    private GhostPW ghostScript;
    private SkinnedMeshRenderer[] meshRenderers;

    [SetUp]
    public void Setup()
    {
        testGameObject = new GameObject();
        ghostScript = testGameObject.AddComponent<GhostPW>();
        meshRenderers = new SkinnedMeshRenderer[1];

        var meshRenderer = new GameObject().AddComponent<SkinnedMeshRenderer>();
        meshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));
        meshRenderer.sharedMaterial.color = Color.white;
        meshRenderers[0] = meshRenderer;

        ghostScript.MeshR = meshRenderers;
    }

    [Test]
    public void FadeGhostCoroutine_ChangesAlphaAndLayer()
    {
        ghostScript.igenpw = true;

        // Manually call the coroutine logic for immediate response simulation
        ghostScript.FadeGhostCoroutineTestable();

        // Assertions to validate the changes
        Assert.AreEqual(0.5f, meshRenderers[0].sharedMaterial.color.a, "Alpha is not set correctly.");
        Assert.AreEqual(LayerMask.NameToLayer("Ghost"), testGameObject.layer, "Layer is not set to Ghost.");
    }
    [Test]
    public void FadeGhostCoroutine_ChangesAlphaCorrectly()
    {
        ghostScript.igenpw = true;
        ghostScript.FadeGhostCoroutineTestable(); // Ensure this method is called before the assertion

        Assert.AreEqual(0.5f, meshRenderers[0].sharedMaterial.color.a, "Alpha is not set correctly.");
    }

    [Test]
    public void FadeGhostCoroutine_ChangesLayerCorrectly()
    {
        ghostScript.igenpw = true;
        ghostScript.FadeGhostCoroutineTestable(); // Ensure this method is called before the assertion

        Assert.AreEqual(LayerMask.NameToLayer("Ghost"), testGameObject.layer, "Layer is not set to Ghost initially.");
    }




    [TearDown]
    public void TearDown()
    {
        if (testGameObject != null)
            GameObject.DestroyImmediate(testGameObject);
    }
}
