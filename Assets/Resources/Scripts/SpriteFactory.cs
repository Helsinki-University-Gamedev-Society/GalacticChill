using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFactory
{
    public enum SpriteMaterial{
        Normal,
        Diffuse
    }

    private static Material enumToMaterial(SpriteMaterial mat){
        switch (mat)
        {
            case SpriteMaterial.Normal:
                return new Material(Shader.Find("Sprites/Default"));

            case SpriteMaterial.Diffuse:
                return new Material(Shader.Find("Sprites/Diffuse"));

            default:
            return null;
        }
    }

    public static GameObject CreateSprite(string texture, SpriteMaterial material){
        Texture2D tex = ResourceManager.Instance.ImportTexture($"Assets/Resources/Tile/Sprites/{texture}.png");
        
        GameObject gameObject = new GameObject ();
        SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();

        Sprite sprite = Sprite.Create(tex,
                                new Rect(0.0f, 0.0f, tex.width, tex.height),
                                new Vector2(0.5f, 0.5f), 32);
        
        spriteRenderer.sprite=sprite;

        spriteRenderer.material = enumToMaterial(material);
        
        return gameObject;
    }
}
