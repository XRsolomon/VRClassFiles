using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

[RequireComponent(typeof(XRTracker))]

public class ControllerInput : MonoBehaviour
{



    public UnityEvent triggerPressed;
    public UnityEvent gripPressed;

    XRTracker tracker;
    float triggerInputValue;
    float gripInputValue;

    bool triggerState;
    bool gripState;

    private void Start()
    {
        tracker = GetComponent<XRTracker>();
    }

    private void Update()
    {
        tracker.trackedDevice.TryGetFeatureValue(CommonUsages.trigger, out triggerInputValue);

        if (triggerInputValue > .5f && triggerState == false)
        {
            triggerPressed.Invoke();
            triggerState = true;
        }
        else if(triggerInputValue < .5f)
        {
            triggerState = false;
        }

        tracker.trackedDevice.TryGetFeatureValue(CommonUsages.grip, out gripInputValue);

        if(gripInputValue > .5f && gripState == false)
        {
            gripState = true;
            gripPressed.Invoke();
        }
        else if (gripInputValue < .5f)
        {
            gripState = false;
        }

    }
}
