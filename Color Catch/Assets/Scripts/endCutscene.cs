using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class endCutscene : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    private PlayableDirector director;

    void Start()
    {
        director = GetComponent<PlayableDirector>();
        director.stopped += OnTimelineEnd;
    }

    void OnTimelineEnd(PlayableDirector pd)
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
