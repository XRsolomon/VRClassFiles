using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class XRTracker : MonoBehaviour
{
    //Keep this around to avoid creating heap garbage
    static List<InputDevice> devices = new List<InputDevice>();

    public InputDeviceRole role;
    [HideInInspector]
    public InputDevice trackedDevice;
    void OnEnable()
    {
        InputDevices.deviceConnected += OnDeviceConnected;
        InputDevices.deviceDisconnected += OnDeviceDisconnected;
        Application.onBeforeRender += OnBeforeRender;
        InputDevices.GetDevicesWithRole(role, devices);
        if (devices.Count > 0)
            OnDeviceConnected(devices[0]);
    }
    void OnDisable()
    {
        InputDevices.deviceConnected -= OnDeviceConnected;
        InputDevices.deviceDisconnected -= OnDeviceDisconnected;
        Application.onBeforeRender -= OnBeforeRender;
    }
    void Update()
    {
        if (trackedDevice.isValid)
            TrackToDevice(trackedDevice);
    }
    void OnDeviceConnected(InputDevice device)
    {
        if (!trackedDevice.isValid && device.role == role)
            trackedDevice = device;
    }
    void OnDeviceDisconnected(InputDevice device)
    {
        if (device == trackedDevice)
            trackedDevice = new InputDevice();
    }
    void OnBeforeRender()
    {
        if (trackedDevice.isValid)
            TrackToDevice(trackedDevice);
    }
    void TrackToDevice(InputDevice trackedDevice)
    {
        Vector3 position;
        if (trackedDevice.TryGetFeatureValue(CommonUsages.devicePosition, out position))
            this.transform.position = position;
        Quaternion rotation;
        if (trackedDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out rotation))
            this.transform.rotation = rotation;
    }
}
