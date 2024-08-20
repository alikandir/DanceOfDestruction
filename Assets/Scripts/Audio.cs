using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance { get; private set; }

    private AudioSource audioSource;
    public AudioClip clip;
    void Awake()
    {
        // Check if an instance of MusicPlayer already exists
        if (Instance != null && Instance != this)
        {
            // If another instance exists, destroy this one
            Destroy(gameObject);
            return;
        }

        // If this is the first instance, set it as the singleton instance
        Instance = this;

        // Prevent this object from being destroyed when loading new scenes
        DontDestroyOnLoad(gameObject);

        // Get or add the AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    private void Start()
    {
        audioSource.clip = clip;
        audioSource.loop=true;
        audioSource.Play();
    }
    
}
