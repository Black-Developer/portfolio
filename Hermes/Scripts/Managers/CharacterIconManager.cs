using UnityEngine;
using UnityEditor;
public class CharacterIconManager : MonoBehaviour
{
    private static CharacterIconManager instance;
    private Camera camera;
    public Camera mainCamera;
    private bool takeScreenShotOnNextFrame;
    private int slot;

    private void Awake()
    {
        instance = this;
        mainCamera = Camera.main;
        camera = gameObject.GetComponent<Camera>();
        camera.enabled = false;
        mainCamera.enabled = true;
        takeScreenShotOnNextFrame = false;
    }

    private void OnPostRender()
    {
        if (takeScreenShotOnNextFrame)
        {
            takeScreenShotOnNextFrame = false;
            RenderTexture renderTexture = camera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.persistentDataPath + "/StreamingAssets" + slot + ".png", byteArray);
            Debug.Log("Save Image" + "/Resources/Sprites/Character/" + slot + ".png");

            //AssetDatabase.Refresh();

            RenderTexture.ReleaseTemporary(renderTexture);
            camera.targetTexture = null;
            camera.enabled = false;
            mainCamera.enabled = true;
        }
    }
    private void TakeCharacterScreenShot(int width, int height,int _slot)
    {
        mainCamera.enabled = false;
        camera.enabled = true;
        camera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        slot = _slot;
        takeScreenShotOnNextFrame = true;
    }
    public static void TakeCharacterScreenShot_Static(int slot)
    {
        instance.TakeCharacterScreenShot(500, 500, slot);
    }
}
