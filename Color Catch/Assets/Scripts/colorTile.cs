using UnityEngine;
using UnityEngine.Tilemaps;

public class colorTile : MonoBehaviour
{
    private Tilemap tilemap;
    private Color originalColor;
    private Color grayColor;
    void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        originalColor = tilemap.color;

        float g = originalColor.grayscale;
        grayColor = new Color(g, g, g);

        tilemap.color = grayColor;
    }

    public void RestoreColor(float t)
    {
        tilemap.color = Color.Lerp(grayColor, colorManager.Instance.CurrentColor ,t);
    }

    public void ApplyCurrentColor()
    {
        tilemap.color = colorManager.Instance.CurrentColor;
    }
}
