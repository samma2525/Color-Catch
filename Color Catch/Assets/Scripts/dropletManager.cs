using UnityEngine;
using UnityEngine.SceneManagement;

public class dropletManager : MonoBehaviour
{
    public static dropletManager Instance;

    public colorTile[] tiles;
    public int dropletsNeeded = 5;

    public GameObject dropletPrefab;
    public GameObject bombPrefab;

    public bool gradualColor = false;
    public float minSpawnDistance = 2f;

    public bool colorChaning = false;
    public bool spawnSecondBomb = false;

    [SerializeField] private string nextSceneName;

    private int collected = 0;
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
        SpawnBombs();
        if (spawnSecondBomb) SpawnBombs();
    }

    public void CollectDroplet()
    {
        scoreScript.Instance.AddScore(1);
        collected++;

        if (colorChaning){
        colorManager.Instance.GenerateNewColor();
        PlayerColorManager.Instance.GenerateNewColorPlayer();
        

        if (gradualColor)
        {
            float progress = Mathf.Clamp01((float)collected / dropletsNeeded);
            foreach (colorTile t in tiles)
                t.RestoreColor(progress);

            if (collected >= dropletsNeeded)
            {
                collected = 0;
                foreach (colorTile tile in tiles)
                    tile.ApplyCurrentColor();
            }
        }
        else
        {
            foreach (colorTile tile in tiles)
                tile.ApplyCurrentColor();
        }
        }
        if (collected >= dropletsNeeded)
        {
            AudioManager.Instance.PlayLevelComplete(nextSceneName);
        }

        SpawnDroplet();
        SpawnBombs();
        if(spawnSecondBomb)SpawnBombs();
    }

    public void CollectBomb()
    {
        scoreScript.Instance.AddScore(-2);
        collected -= 2;

        if(colorChaning){
        colorManager.Instance.GenerateNewColor();
        PlayerColorManager.Instance.GenerateNewColorPlayer();
        

        if (gradualColor)
        {
            float progress = Mathf.Clamp01((float)collected / dropletsNeeded);
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
        SpawnBombs();
        if(spawnSecondBomb) SpawnBombs();
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
        if(spawnFirstBomb){
        if(currentBomb1 != null) Destroy(currentBomb1);
        int attempts = 0;
        Vector3 bombPos = GetRandomPositionAwayFrom(lastDropletPos); 
        currentBomb1 = Instantiate(bombPrefab, bombPos, Quaternion.identity);
        Bomb b = currentBomb1.GetComponent<Bomb>();
        if (b != null) b.SetColor(colorManager.Instance.CurrentColor);
        }
        else
        {
            if (currentBomb2 != null)Destroy(currentBomb2);
            Vector3 bombPos =GetRandomPositionAwayFrom(lastDropletPos);
            currentBomb2 = Instantiate(bombPrefab,bombPos, Quaternion.identity);
            Bomb b = currentBomb2.GetComponent<Bomb>();
            if (b != null) b.SetColor(colorManager.Instance.CurrentColor);
        }
        spawnFirstBomb = !spawnFirstBomb;
}}