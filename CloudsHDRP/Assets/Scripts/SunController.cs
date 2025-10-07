using UnityEngine;
using System.Collections;

public class SunController : MonoBehaviour
{
    [Header("Object Reference")]
    public Light sunLight;

    [Header("Animation Settings")]
    public float dayDuration = 10f;

    [Header("Intensity Settings")]
    public float sunriseSunsetIntensity = 40000f;
    public float noonIntensity = 70000f;

    // --- Private rotation definitions ---
    private readonly Vector3 _sunriseEuler = new Vector3(10f, -90f, 0f);
    private readonly Vector3 _noonEuler = new Vector3(75f, 0f, 0f);
    private readonly Vector3 _sunsetEuler = new Vector3(10f, 90f, 0f);

    private bool _isAnimating = false;
    private Coroutine _dayCycleCoroutine;

    void Start()
    {
        if (sunLight == null)
        {
            Debug.LogError("SunController is missing a reference to the Sun Light!");
            this.enabled = false;
        }
    }

    void Update()
    {
        HandleSunInput();
    }

    private void HandleSunInput()
    {
        // P key for Sunrise
        if (Input.GetKeyDown(KeyCode.P))
        {
            StopAnimation();
            SetSunPosition(_sunriseEuler, sunriseSunsetIntensity);
            Debug.Log("Sun set to: Sunrise");
        }

        // Y key for Sunset
        if (Input.GetKeyDown(KeyCode.Y))
        {
            StopAnimation();
            SetSunPosition(_sunsetEuler, sunriseSunsetIntensity);
            Debug.Log("Sun set to: Sunset");
        }

        // I key for Noon
        if (Input.GetKeyDown(KeyCode.I))
        {
            StopAnimation();
            SetSunPosition(_noonEuler, noonIntensity);
            Debug.Log("Sun set to: Noon");
        }
        
        // O key for Morning
        if (Input.GetKeyDown(KeyCode.O))
        {
            StopAnimation();
            Vector3 morningEuler = (_sunriseEuler + _noonEuler) / 2f;
            float morningIntensity = (sunriseSunsetIntensity + noonIntensity) / 2f;
            SetSunPosition(morningEuler, morningIntensity);
            Debug.Log("Sun set to: Morning");
        }
        
        // U key for Afternoon
        if (Input.GetKeyDown(KeyCode.U))
        {
            StopAnimation();
            Vector3 afternoonEuler = (_noonEuler + _sunsetEuler) / 2f;
            float afternoonIntensity = (noonIntensity + sunriseSunsetIntensity) / 2f;
            SetSunPosition(afternoonEuler, afternoonIntensity);
            Debug.Log("Sun set to: Afternoon");
        }

        // R key to start the day cycle animation
        if (Input.GetKeyDown(KeyCode.R) && !_isAnimating)
        {
            _dayCycleCoroutine = StartCoroutine(AnimateDayCycle());
        }
    }

    private void SetSunPosition(Vector3 eulerAngles, float intensity)
    {
        sunLight.transform.rotation = Quaternion.Euler(eulerAngles);
        sunLight.intensity = intensity;
    }
    
    private void StopAnimation()
    {
        if (_dayCycleCoroutine != null)
        {
            StopCoroutine(_dayCycleCoroutine);
        }
        _isAnimating = false;
    }

    private IEnumerator AnimateDayCycle()
    {
        _isAnimating = true;
        Debug.Log("Starting day cycle animation...");

        float halfDay = dayDuration / 2f;
        float elapsedTime = 0f;

        Quaternion sunriseRotation = Quaternion.Euler(_sunriseEuler);
        Quaternion noonRotation = Quaternion.Euler(_noonEuler);
        Quaternion sunsetRotation = Quaternion.Euler(_sunsetEuler);
        
        // Phase 1: Sunrise -> Noon
        while (elapsedTime < halfDay)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / halfDay;
            
            sunLight.transform.rotation = Quaternion.Slerp(sunriseRotation, noonRotation, t);
            sunLight.intensity = Mathf.Lerp(sunriseSunsetIntensity, noonIntensity, t);
            
            yield return null;
        }
        
        SetSunPosition(_noonEuler, noonIntensity);
        
        // Phase 2: Noon -> Sunset
        elapsedTime = 0f;
        while (elapsedTime < halfDay)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / halfDay;
            
            sunLight.transform.rotation = Quaternion.Slerp(noonRotation, sunsetRotation, t);
            sunLight.intensity = Mathf.Lerp(noonIntensity, sunriseSunsetIntensity, t);
            
            yield return null;
        }
        
        SetSunPosition(_sunsetEuler, sunriseSunsetIntensity);
        
        _isAnimating = false;
        Debug.Log("Day cycle animation finished.");
    }
}