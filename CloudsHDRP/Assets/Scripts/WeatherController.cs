using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class WeatherController : MonoBehaviour
{
    // A private reference to the Volume component on this GameObject.
    private Volume _globalVolume;

    // A private reference to the Volumetric Clouds settings within the Volume Profile.
    private VolumetricClouds _clouds;

    void Start()
    {
        _globalVolume = GetComponent<Volume>();
        if (_globalVolume == null)
        {
            Debug.LogError("WeatherController requires a Volume component on the same object!");
            this.enabled = false;
            return;
        }
        if (!_globalVolume.profile.TryGet(out _clouds))
        {
            Debug.LogError("Could not find VolumetricClouds override in the Volume profile. Make sure it is added.");
            this.enabled = false;
            return;
        }

        SetNoClouds();
        Debug.Log("Weather Controller Initialized. Press Keypad 0-9 to change weather.");
    }

    void Update()
    {
        HandleWeatherInput();
    }

    private void HandleWeatherInput()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0)) SetNoClouds();
        if (Input.GetKeyDown(KeyCode.Keypad1)) SetFewPuffyClouds();
        if (Input.GetKeyDown(KeyCode.Keypad2)) SetScatteredPuffyClouds();
        if (Input.GetKeyDown(KeyCode.Keypad3)) SetManyPuffyClouds();
        if (Input.GetKeyDown(KeyCode.Keypad4)) SetFewGreyClouds();
        if (Input.GetKeyDown(KeyCode.Keypad5)) SetManyGreyCloudsHigh();
        if (Input.GetKeyDown(KeyCode.Keypad6)) SetManyGreyCloudsLow();
    }
    

    #region Presets 0-6
    private void SetBasePuffyCloudSettings()
    {
        _clouds.active = true;
        _clouds.erosionFactor.value = 0.2f;
        _clouds.scatteringTint.value = Color.white;
        _clouds.multiScattering.value = 0.9f;
        _clouds.powderEffectIntensity.value = 0.6f;
        _clouds.sunLightDimmer.value = 1.0f;
        _clouds.ambientLightProbeDimmer.value = 1.0f;
    }
    private void SetBaseRainyCloudSettings()
    {
        _clouds.active = true;
        _clouds.scatteringTint.value = new Color(0.55f, 0.55f, 0.55f);
        _clouds.multiScattering.value = 0.2f;
        _clouds.powderEffectIntensity.value = 0.1f;
        _clouds.erosionFactor.value = 0.7f;
        _clouds.sunLightDimmer.value = 0.4f;
    }
    private void SetNoClouds() { _clouds.active = false; }
    private void SetFewPuffyClouds()
    {
        SetBasePuffyCloudSettings();
        _clouds.shapeFactor.value = 0.95f;
        _clouds.densityMultiplier.value = 0.25f;
        _clouds.bottomAltitude.value = 2000f;
    }
    private void SetScatteredPuffyClouds()
    {
        SetBasePuffyCloudSettings();
        _clouds.shapeFactor.value = 0.85f;
        _clouds.densityMultiplier.value = 0.4f;
        _clouds.bottomAltitude.value = 1800f;
    }
    private void SetManyPuffyClouds()
    {
        SetBasePuffyCloudSettings();
        _clouds.multiScattering.value = 1.0f;
        _clouds.sunLightDimmer.value = 1.0f;
        _clouds.shapeFactor.value = 0.7f;
        _clouds.densityMultiplier.value = 0.5f;
        _clouds.bottomAltitude.value = 1600f;
    }
    private void SetFewGreyClouds()
    {
        SetBaseRainyCloudSettings();
        _clouds.bottomAltitude.value = 2200f;
        _clouds.shapeFactor.value = 0.8f;
        _clouds.densityMultiplier.value = 0.3f;
    }
    private void SetManyGreyCloudsHigh()
    {
        SetBaseRainyCloudSettings();
        _clouds.bottomAltitude.value = 2200f;
        _clouds.shapeFactor.value = 0.6f;
        _clouds.densityMultiplier.value = 0.5f;
    }
    private void SetManyGreyCloudsLow()
    {
        SetBaseRainyCloudSettings();
        _clouds.bottomAltitude.value = 800f;
        _clouds.shapeFactor.value = 0.6f;
        _clouds.densityMultiplier.value = 0.5f;
    }
    #endregion
    
}