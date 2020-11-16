using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightEmitter : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;

    public bool lightActive;

    [Range(0, 10)]
    public int soundSelector;

    [Range(0f, 1f)]
    public float volume;

    // Start is called before the first frame update
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/LightAudio");
        instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        //volume = 1;
        lightActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        FMOD.Studio.PLAYBACK_STATE playState;
        instance.getPlaybackState(out playState);
    }

    void OnEnable()
    {
        instance.setParameterByName("Sound Selector", soundSelector);

        instance.setVolume(volume);

        instance.start();
        lightActive = true;
    }

    void OnDisable()
    {
        lightActive = false;

        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
