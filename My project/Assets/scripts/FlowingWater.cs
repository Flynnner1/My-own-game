using UnityEngine;

// Ensure an AudioSource component exists on the same GameObject
[RequireComponent(typeof(AudioSource))]
public class ProximityMusicPlayer : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The distance within which the music should start playing.")]
    public float proximityDistance = 5f;

    [Tooltip("The audio clip to play when the player is near.")]
    public AudioClip musicClip;

    [Tooltip("The volume level for the music (0.0 to 1.0).")]
    [Range(0f, 1f)] // Adds a slider in the Inspector
    public float targetVolume = 0.05f; // Default to a quieter volume (20%)

    [Header("References")]
    [Tooltip("Optional: Assign the player's Transform directly if known. Otherwise, it will be found by tag 'Player'.")]
    public Transform playerTransform;

    // Private variables
    private AudioSource audioSource;
    private bool isMusicPlaying = false;
    private string logPrefix; // To easily identify logs from this instance

    void Start()
    {
        logPrefix = $"[ProximityMusicPlayer ({gameObject.name})]: ";
        //Debug.Log(logPrefix + "Starting initialization...");

        // Get the required AudioSource component
        audioSource = GetComponent<AudioSource>();
        //Debug.Log(logPrefix + "AudioSource component obtained.");

        // --- Set the Volume ---
        audioSource.volume = targetVolume; // Set the volume based on the Inspector value
        //Debug.Log(logPrefix + $"Setting AudioSource volume to: {targetVolume}");
        // --- End Volume Setting ---

        // Find the player by tag if not assigned in the Inspector
        if (playerTransform == null)
        {
            //Debug.Log(logPrefix + "Player Transform not assigned in Inspector, attempting to find by tag 'Player'.");
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                playerTransform = playerObject.transform;
                //Debug.Log(logPrefix + $"Player found: {playerObject.name}");
            }
            else
            {
                Debug.LogError(logPrefix + "Player object with tag 'Player' not found in the scene! Disabling script.", this);
                this.enabled = false;
                return;
            }
        }
        else
        {
            //Debug.Log(logPrefix + $"Player Transform assigned in Inspector: {playerTransform.name}");
        }

        // Configure the AudioSource
        if (musicClip != null)
        {
            audioSource.clip = musicClip;
            audioSource.playOnAwake = false;
            audioSource.loop = true;
            //Debug.Log(logPrefix + $"Music Clip '{musicClip.name}' assigned and AudioSource configured.");
        }
        else
        {
            Debug.LogError(logPrefix + "Music Clip is not assigned in the Inspector! Disabling script.", this);
            this.enabled = false;
        }

        //Debug.Log(logPrefix + "Initialization complete.");
    }

    void Update()
    {
        if (playerTransform == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= proximityDistance)
        {
            if (!isMusicPlaying)
            {
                Debug.Log(logPrefix + $"Player entered range (Distance: {distanceToPlayer:F2}). Starting music at volume {audioSource.volume}.");
                audioSource.Play();
                isMusicPlaying = true;
            }
        }
        else
        {
            if (isMusicPlaying)
            {
                Debug.Log(logPrefix + $"Player left range (Distance: {distanceToPlayer:F2}). Stopping music.");
                audioSource.Stop();
                isMusicPlaying = false;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, proximityDistance);
    }
}