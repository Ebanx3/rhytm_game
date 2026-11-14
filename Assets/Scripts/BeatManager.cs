using System;
using UnityEngine;

public class BeatManager : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    [SerializeField] private float bpm = 120f;
    [SerializeField] private int ticksPerBeat = 4;

    public event Action<int> OnTick;

    private double songStartTime;
    private double tickInterval;
    private int lastTick = -1;

    void Start()
    {
        tickInterval = 60.0 / (bpm * ticksPerBeat);

        songStartTime = AudioSettings.dspTime + 1.0;

        music.PlayScheduled(songStartTime);
    }

    void Update()
    {
        if (AudioSettings.dspTime < songStartTime)
            return;

        int currentTick = (int)(
            (AudioSettings.dspTime - songStartTime) / tickInterval
        );

        while (lastTick < currentTick)
        {
            lastTick++;
            OnTick?.Invoke(lastTick);
        }
    }
}
