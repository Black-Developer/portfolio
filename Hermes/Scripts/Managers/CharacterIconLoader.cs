using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
public class CharacterIconLoader : MonoBehaviour
{
    public int slot;
    public Image image;
    public Texture2D sprite;

    public void Start()
    {
        image = GetComponent<Image>();
        sprite = null;
        LoadIcon(slot);
    }
    public void Update()
    {
        if (image.sprite == null)
        {
            LoadIcon(slot);
        }
    }
    public void LoadIcon(int slot)
    {
        string path = Application.persistentDataPath + "/StreamingAssets" + slot + ".png";
        var rawData = System.IO.File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(rawData);
        //if (sprite == null)
        //{
        //    return;
        //}
        Rect rect = new Rect(0, 0, texture.width, texture.height);
        image.sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
    }
}
