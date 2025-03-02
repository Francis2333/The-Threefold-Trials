using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// using FFTSharp;
// Import your FFT library, e.g. using FFTSharp; (Adjust for your actual FFT library)

[RequireComponent(typeof(AudioSource))]
public class AudioDrivenObstacleGenerator : MonoBehaviour
{
    [Header("Audio / FFT Settings")]
    public AudioSource audioSource;         // The audio source to analyze
    public int fftSize = 1024;             // Must be a power of two
    public float lowFreqCutoff = 500f;     // Everything below this is "low group"
    public float highFreqCutoff = 2000f;   // Everything above this is "high group"
    public float spawnThreshold = 10f;     // How loud a group must be to spawn

    [Header("Obstacle Spawning")]
    public GameObject obstaclePrefab;      // Prefab to spawn
    public float checkInterval = 0.25f;    // How often (seconds) to analyze & spawn
    public float spawnY = 0f;             // The Y position to place spawned obstacles
    public float spawnOffsetZ = 0f;       // Slight offset behind or in front of scene
    private Camera mainCam;

    // Timers and data
    private float timer = 0f;
    private float[] samples;              // Array to hold time-domain samples
    private float[] spectrum;             // Array to hold frequency-domain data (from FFT)

    void Start()
    {
        // Grab references
        if (!audioSource) audioSource = GetComponent<AudioSource>();
        mainCam = Camera.main;

        // Initialize arrays for FFT
        samples = new float[fftSize];
        spectrum = new float[fftSize];

        // Optionally start playing audio if needed:
        // audioSource.Play();
    }

    void Update()
    {
        // Accumulate time
        timer += Time.deltaTime;

        // Every checkInterval seconds, perform an FFT analysis and attempt to spawn obstacles
        if (timer >= checkInterval)
        {
            timer = 0f; // Reset timer
            AnalyzeAudioAndSpawn();
        }
    }

    private void AnalyzeAudioAndSpawn()
    {
        // 1) Get the current audio samples
        //    Unity's GetSpectrumData() is a built-in method that returns frequency magnitudes.
        //    Alternatively, if you use a library like FFTSharp, you'd first do:
        //      audioSource.GetOutputData(samples, 0); 
        //      <perform your own FFT> => fill 'spectrum'
        //    But for simplicity, let's use GetSpectrumData if your project allows it.

        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Hanning);
        //    - spectrum[i] will give the magnitude at a certain frequency bin
        //    - This is already an FFT, courtesy of Unity.

        // 2) Sum the "low" and "high" frequency groups
        float lowGroupValue = 0f;
        float highGroupValue = 0f;

        float sampleRate = AudioSettings.outputSampleRate; // e.g. 44100 by default

        // Unity's GetSpectrumData length = fftSize / 2 for real signals, 
        // or sometimes they fill the entire array. We'll iterate up to spectrum.Length.
        for (int i = 0; i < spectrum.Length; i++)
        {
            // Frequency for this bin = binIndex * (sampleRate / 2) / (spectrum.Length - 1)
            // or simpler approximation:
            float freq = i * (sampleRate / (2f * spectrum.Length));

            if (freq < lowFreqCutoff)
                lowGroupValue += spectrum[i];
            else if (freq > highFreqCutoff)
                highGroupValue += spectrum[i];
        }

        // 3) Check if we exceed some threshold in the low or high group
        //    If so, spawn obstacles to the right side of the camera.
        if (lowGroupValue > spawnThreshold)
        {
            SpawnObstacle("LowFreq");
        }

        if (highGroupValue > spawnThreshold)
        {
            SpawnObstacle("HighFreq");
        }

        // You could also compare lowGroupValue vs. highGroupValue or measure time
        // between spikes, etc. This is a simple threshold-based approach.
    }

    private void SpawnObstacle(string groupTag)
    {
        if (!obstaclePrefab || !mainCam) return;

        // 1) Position to the right side of the camera
        //    We'll convert viewport X=1 => the right edge. 
        //    Y is spawnY, and Z is offset for layering.
        Vector3 spawnPos = mainCam.ViewportToWorldPoint(new Vector3(1f, 0.5f, 0f));
        spawnPos.y = spawnY;
        spawnPos.z = spawnOffsetZ; // Slight offset in front/behind the scene

        // 2) Instantiate the obstacle
        GameObject obs = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);

        // 3) (Optional) If you want to differentiate between low/high obstacles,
        //    you can do so here with groupTag. For example:
        //        - change color, speed, scale, etc.
        //    E.g.:
        if (groupTag == "LowFreq")
        {
            obs.name = "Obstacle_Low";
        }
        else
        {
            obs.name = "Obstacle_High";
            // maybe scale it bigger
            obs.transform.localScale *= 1.25f;
        }

        // The spawned object should have the "ObstacleBehavior" script (formerly BulletBehavior) 
        // so it moves left (negative X) and destroys itself when off-screen.
    }
}
