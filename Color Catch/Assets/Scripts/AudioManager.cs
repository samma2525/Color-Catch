using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;

    public AudioSource sfxSource;
    public AudioSource musicSource;

    public AudioClip bombSFX;
    public AudioClip dropletSFX;
    public AudioClip levelCompleteSFX;

    public GameObject buttonSFX;
    public GameObject buttonMusic;

    public Sprite SFXmuteIcon;
    public Sprite SFXunmuteIcon;

    public Sprite MusicMuteIcon;
    public Sprite MusicUnmuteIcon;

    private bool isMutedSFX = false;
    private bool isMutedMusic = false;
    private Image buttonImageSFX;
    private Image buttonImageMusic;
    void Awake()
    {
        Instance = this;
        buttonImageSFX = buttonSFX.GetComponent<Image>();
        buttonImageMusic = buttonMusic.GetComponent<Image>();
    }

    public void PlayBombSFX()
    {
        sfxSource.PlayOneShot(bombSFX);
    }

    public void PlayDropletSFX()
    {
        sfxSource.PlayOneShot(dropletSFX);
    }

    public void ToggleSFX()
    {
        isMutedSFX = !isMutedSFX;
        sfxSource.mute = isMutedSFX;
        buttonImageSFX.sprite = isMutedSFX ? SFXmuteIcon : SFXunmuteIcon;
    }

    public void ToggleMusic()
    {
        isMutedMusic = !isMutedMusic;
        musicSource.mute = isMutedMusic;
        buttonImageMusic.sprite = isMutedMusic ? MusicMuteIcon : MusicUnmuteIcon;
    }

    public void PlayLevelComplete(string sceneName)
    {
        StartCoroutine(PlayThenLoadScene(sceneName));
    }

    private IEnumerator PlayThenLoadScene(string sceneName)
    {
        sfxSource.PlayOneShot(levelCompleteSFX);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneName);
    }
}
