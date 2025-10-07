using UnityEngine;
using TMPro;

public class InstructionsController : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI instructionsText; 

    [Header("Object References")]
    public CameraController cameraController; 
    
    private const string INSTRUCTIONS_TEMPLATE = 
        "KONTROLE:\n" +
        "Predefinisani režimi: 0-6 (Keypad)\n" +
        "Kretanje: WASD\n" +
        "Gore/Dole: E / Q\n" +
        "Rotacija: Strelice\n" +
        "Zumiranje: Točkić miša\n" +
        "Promena brzine: + / - (Keypad)\n\n" +
        "Trenutna brzina: {0}";

    void Start()
    {
        if (instructionsText == null)
        {
            Debug.LogError("UIController skripti nedostaje referenca na TextMeshProUGUI element.");
            this.enabled = false;
            return;
        }
        if (cameraController == null)
        {
            Debug.LogError("UIController skripti nedostaje referenca na CameraController skriptu.");
            this.enabled = false;
            return;
        }
    }

    void Update()
    {
        float currentSpeed = cameraController.moveSpeed;
        instructionsText.text = string.Format(INSTRUCTIONS_TEMPLATE, currentSpeed.ToString("F0"));
    }
}