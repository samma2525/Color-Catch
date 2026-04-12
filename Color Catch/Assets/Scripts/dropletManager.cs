using UnityEngine;
using UnityEngine.SceneManagement;

public class dropletManager : MonoBehaviour
{
    public static dropletManager Instance;

    public colorTile[] tiles;
    public int dropletsToWin = 5;
    public int dropletsToChangeColor = 0;

    public GameObject dropletPrefab;
    public GameObject bombPrefab;

    public bool gradualColor = false;
    public float minSpawnDistance = 2f;

    public bool colorChaning = false;
    public bool spawnBombs = true;
    public bool spawnSecondBomb = false;

    [SerializeField] private string nextSceneName;

    private int collectedForColor = 0;
    private int collectedForLevel = 0;
    private Vector3 lastDropletPos;

    private GameObject currentDroplet;
    private GameObject currentBomb1;
    private GameObject currentBomb2;
    private bool spawnFirstBomb = true;

    private Transform playerTransform;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        SpawnDroplet();
        if (spawnBombs) SpawnBombs();
    }

    public void CollectDroplet()
    {
        collectedForColor++;
        collectedForLevel++;
        scoreScript.Instance.AddScore(1);

        //allowing color chaning for some levels
        if (colorChaning){
        colorManager.Instance.GenerateNewColor();
        PlayerColorManager.Instance.GenerateNewColorPlayer();
        

        //Gradient black to color logic
        if (gradualColor)
        {
            float progress = Mathf.Clamp01((float)collectedForColor / dropletsToChangeColor);
            foreach (colorTile t in tiles)
                t.RestoreColor(progress);

            if (collectedForColor >= dropletsToChangeColor)
            {
                collectedForColor = 0;
                foreach (colorTile tile in tiles)
                    tile.ApplyCurrentColor();

                colorManager.Instance.GenerateNewColor();
                PlayerColorManager.Instance.GenerateNewColorPlayer();
            }
        }
        else
        {
            foreach (colorTile tile in tiles)
                tile.ApplyCurrentColor();
        }
        }
        //Win statement
        if (collectedForLevel >= dropletsToWin)
        {
            AudioManager.Instance.PlayLevelComplete(nextSceneName);
        }

        SpawnDroplet();
        if(spawnBombs)SpawnBombs();
    }

    public void CollectBomb()
    {
        scoreScript.Instance.AddScore(-2);
        collectedForLevel -= 2;
        collectedForColor -=2;

        if(colorChaning){
        colorManager.Instance.GenerateNewColor();
        PlayerColorManager.Instance.GenerateNewColorPlayer();
        

        if (gradualColor)
        {
            float progress = Mathf.Clamp01((float)collectedForColor / dropletsToChangeColor);
            foreach (colorTile t in tiles)
                t.RestoreColor(progress);
        }
        else
        {
            foreach (colorTile tile in tiles)
                tile.ApplyCurrentColor();
        }
        }

        SpawnDroplet();
        if(spawnBombs)SpawnBombs();
    }

    private Vector3 GetRandomPositionAwayFrom(Vector3 otherPos)
{
    Vector3 candidate;
    int attempts = 0;

    do
    {
        float x = Random.Range(-4.17f, 4.025f);
        float y = Random.Range(-4.282f, 4.216f);
        candidate = new Vector3(x, y, 0);
        attempts++;
    }
    while ((Vector3.Distance(candidate, otherPos) < minSpawnDistance ||
            Vector3.Distance(candidate, playerTransform.position) < minSpawnDistance)
            && attempts < 100);

    return candidate;
}

    public void SpawnDroplet()
    {
        if (dropletPrefab == null) return;

        if(currentDroplet != null)
        {Destroy(currentDroplet);}

        lastDropletPos = GetRandomPositionAwayFrom(playerTransform.position);
        currentDroplet = Instantiate(dropletPrefab, lastDropletPos, Quaternion.identity);
        Droplet d = currentDroplet.GetComponent<Droplet>();
        if (d != null)
            d.SetColor(colorManager.Instance.CurrentColor);
    }

    public void SpawnBombs()
    {
        if (bombPrefab == null) return;

        if (currentBomb1 != null) Destroy(currentBomb1);
        if (currentBomb2 != null) Destroy(currentBomb2);

        Vector3 bombPos = GetRandomPositionAwayFrom(lastDropletPos);
        currentBomb1 = Instantiate(bombPrefab, bombPos, Quaternion.identity);
        Bomb b1 = currentBomb1.GetComponent<Bomb>();
        if (b1 != null) b1.SetColor(colorManager.Instance.CurrentColor);

        if (spawnSecondBomb)
        {
            Vector3 bombPos2 = GetRandomPositionAwayFrom(lastDropletPos);
            currentBomb2 = Instantiate(bombPrefab, bombPos2, Quaternion.identity);
            Bomb b2 = currentBomb2.GetComponent<Bomb>();
            if (b2 != null) b2.SetColor(colorManager.Instance.CurrentColor);
        }
}}