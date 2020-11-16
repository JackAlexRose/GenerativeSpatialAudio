using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LightTypes;

public class SpotlightController : MonoBehaviour
{
    [Range(0.05f, 5f)]
    public float SpotlightTimerInterval;
    private float SpotlightTargetTime;

    [Range(0.0f, 1.0f)]
    public float SpotlightSkipChance = 0f;

    private float delayInterval = 0.1f;

    public GameObject[] LightObjects;

    private List<ScrollingLight> _activeLights = new List<ScrollingLight>();

    // Start is called before the first frame update
    void Start()
    {
        SpotlightTargetTime = Time.deltaTime + SpotlightTimerInterval;
    }

    void OnEnable()
    {
        SpotlightTargetTime = Time.deltaTime + SpotlightTimerInterval;

        _activeLights.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        // delayTimer -= Time.deltaTime;
        SpotlightTargetTime -= Time.deltaTime;

        if (SpotlightTargetTime <= 0f)
        {
            SpotlightTimerEnded();
        }

        // if (delayTimer <= 0f)
        // {
        //     DelayTimerEnded();
        //     delayTimer = 0.5f;
        // }

        for (int i = _activeLights.Count - 1; i > -1; i--)
        {
            if (!_activeLights[i].Light.activeSelf)
            {
                _activeLights.RemoveAt(i);
            }
        }
    }

    private void FixedUpdate()
    {
        var currentAmount = _activeLights.Count;
        for (int i = 0; i < currentAmount; i++)
        {
            _activeLights[i].Material.SetTextureOffset("_MainTex", new Vector2(0, _activeLights[i].CurrentOffset));
            _activeLights[i].CurrentOffset += Time.deltaTime * _activeLights[i].ScrollSpeed;
            _activeLights[i].delayTimer -= Time.deltaTime;

            if (_activeLights[i].delayTimer <= 0f)
            {
                DelayTimerEnded(_activeLights[i]);
                _activeLights[i].Light.SetActive(false);
            }
        }
    }

    private void SpotlightTimerEnded()
    {
        if (SpotlightSkipRoll())
        {
            SpotlightTargetTime = Time.deltaTime + SpotlightTimerInterval;
            return;
        }

        var numLights = Random.Range(1, 4);
        numLights = 1;

        for (int i = 0; i < numLights; i++)
        {
            var index = Random.Range(0, LightObjects.Length);
            var volume = 1f;
            var soundSelector = Random.Range(0, 2);

            SwitchOnLight(index, volume, soundSelector);
        }

        SpotlightTargetTime = Time.deltaTime + SpotlightTimerInterval;
    }

    private void DelayTimerEnded(ScrollingLight light)
    {
        light.delayTimer = delayInterval;
        if (light.Emitter.volume > 0.4f)
        {
            var index = light.Index;
            var delayVolume = light.Emitter.volume * Random.Range(0.5f, 0.8f);

            ActivateDelayLights(index, delayVolume, light.Emitter.soundSelector);
        }
    }

    private void SwitchOnLight(int index, float volume, int soundSelector)
    {
        var newLight = new ScrollingLight(index, LightObjects[index], Random.Range(0.0f, 15.0f), Random.Range(0, 10), LightObjects[index].transform.GetChild(1).GetComponent<Renderer>().material, LightObjects[index].GetComponent<SpotlightEmitter>());
        newLight.Emitter.volume = volume;
        newLight.delayTimer = delayInterval;
        newLight.Emitter.soundSelector = soundSelector;
        newLight.Light.SetActive(true);

        _activeLights.Add(newLight);
    }

    private bool SpotlightSkipRoll()
    {
        float fRand = Random.Range(0.0f, 1.0f);

        return fRand <= SpotlightSkipChance;
    }

    private Vector2 IndexToCoordinates(int index)
    {
        Vector2 coordinates = new Vector2();

        coordinates.x = (int)(index / 6);
        coordinates.y = index % 6;
        return coordinates;
    }

    private int CoordinatesToIndex(Vector2 coordinates)
    {
        int index = (int)(coordinates.x * 6 + coordinates.y);

        return index;
    }

    private bool LightChance()
    {
        float fRand = Random.Range(0.0f, 1.0f);

        return fRand <= 0.5f;
    }

    private void ActivateDelayLights(int index, float delayVolume, int soundSelector)
    {
        var coords = IndexToCoordinates(index);

        var left = LightChance();
        var right = LightChance();
        var up = LightChance();
        var down = LightChance();

        if (left)
        {
            var leftCoords = new Vector2(coords.x, coords.y - 1);
            if (leftCoords.y > -1)
            {
                SwitchOnLight(CoordinatesToIndex(leftCoords), delayVolume, soundSelector);
            }
        }

        if (right)
        {
            var rightCoords = new Vector2(coords.x, coords.y + 1);
            if (rightCoords.y < 5)
            {
                SwitchOnLight(CoordinatesToIndex(rightCoords), delayVolume, soundSelector);
            }
        }

        if (up)
        {
            var upCoords = new Vector2(coords.x - 1, coords.y);
            if (upCoords.x > -1)
            {
                SwitchOnLight(CoordinatesToIndex(upCoords), delayVolume, soundSelector);
            }
        }

        if (down)
        {
            var downCoords = new Vector2(coords.x + 1, coords.y);
            if (downCoords.x < 5)
            {
                SwitchOnLight(CoordinatesToIndex(downCoords), delayVolume, soundSelector);
            }
        }
    }
}
