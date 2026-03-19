using UnityEngine;

public class colorManager : MonoBehaviour
{
    public static colorManager Instance;
    public Color CurrentColor {get; private set;}

    void Awake()
    {
        Instance = this;
        CurrentColor = Color.white;
    }
    public void GenerateNewColor()
    {
        CurrentColor = new Color(
            Random.value,
            Random.value,
            Random.value);
    }
}
