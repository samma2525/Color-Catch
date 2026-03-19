using UnityEngine;

public class PlayerColorManager : MonoBehaviour
{
    public static PlayerColorManager Instance;
    public Color CurrentColor { get; private set; }

    [Range(0.3f, 1f)]
    public float minColorDistance = 0.5f;

    void Awake()
    {
        Instance = this;
        CurrentColor = Color.black;
    }

    public void GenerateNewColorPlayer()
    {
        Color bg = colorManager.Instance.CurrentColor;
        Color candidate;
        int attempts = 0;

        do
        {
            candidate = new Color(Random.value, Random.value, Random.value);
            attempts++;
        }
        while (ColorDistance(candidate, bg) < minColorDistance && attempts < 100);

        CurrentColor = candidate;
    }

    private float ColorDistance(Color a, Color b)
    {
        float dr = a.r - b.r;
        float dg = a.g - b.g;
        float db = a.b - b.b;
        return Mathf.Sqrt(dr * dr + dg * dg + db * db);
    }
}
