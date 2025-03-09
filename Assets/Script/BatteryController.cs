using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryController : MonoBehaviour
{
    [SerializeField] private GameObject _switch;
    [SerializeField] private InteractableUnityEventWrapper _switchListener;

    private bool isON;

    private void Start()
    {
        // Add a listener to detect switch interactions
        _switchListener.WhenSelect.AddListener(ToggleSwitch);
    }

    private void ToggleSwitch()
    {
        isON = !isON;

        Vector3 newRotation = _switch.transform.localEulerAngles;
        newRotation.x = isON ? 90f : 0f;
        _switch.transform.localEulerAngles = newRotation;
        AudioManager.Instance.Snap();
    }
}