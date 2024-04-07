using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController Instance;

    [SerializeField] private AudioSource introMusic;
    [SerializeField] private AudioSource loopMusic;

    private bool musicStarted = false;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        introMusic.volume = 1;
        loopMusic.volume = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        introMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (introMusic.isPlaying == false)
        {
            if (musicStarted == false)
            {
                loopMusic.Play();
                musicStarted = true;
            }
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
