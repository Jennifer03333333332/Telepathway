// This example uses the Leap Motion Controller to place a point on the user's hand.
// The emitter will stop when a hand is not detected or the Leap Motion Controller is not connected.

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ultrahaptics;

public class Unity_AMHandTracking : MonoBehaviour
{
    AmplitudeModulationEmitter _emitter;
    Alignment _alignment;
    Leap.Controller _leap;
    public bool flag = true;

    void Start()
    {
        // Initialize the emitter
        _emitter = new AmplitudeModulationEmitter();
        _emitter.initialize();
        _leap = new Leap.Controller();
        StartCoroutine(WaitAndPrint(0.2f));
        // NOTE: This example uses the Ultrahaptics.Alignment class to convert Leap coordinates to the Ultrahaptics device space.
        // You can either use this as-is for Ultrahaptics development kits, create your own alignment files for custom devices,
        // or replace the Alignment references in this example with your own coordinate conversion system.

        // Load the appropriate alignment file for the currently-used device
        _alignment = _emitter.getDeviceInfo().getDefaultAlignment();
        // Load a custom alignment file (absolute path, or relative path from current working directory)
        // _alignment = new Alignment("my_custom.alignment.xml");
    }


    // Converts a Leap Vector directly to a UH Vector3
    Ultrahaptics.Vector3 LeapToUHVector(Leap.Vector vec)
    {
        return new Ultrahaptics.Vector3 (vec.x, vec.y, vec.z);
    }


    // Update on every frame
    void Update()
    {
        if (_leap.IsConnected)
        {
            var frame = _leap.Frame ();
            if (frame.Hands.Count > 0)
            {
                // The Leap Motion can see a hand, so get its palm position
                Leap.Vector leapPalmPosition = frame.Hands[0].PalmPosition;
                Leap.Vector leapFingerPosiion = frame.Hands[0].Fingers[1].TipPosition;
                Leap.Vector leapFingerPosiion1 = frame.Hands[0].Fingers[1].bones[0].Center;
                Leap.Vector leapFingerPosiion2 = frame.Hands[0].Fingers[1].bones[1].Center;
                Leap.Vector leapFingerPosiion3 = frame.Hands[0].Fingers[1].bones[2].Center;
                Leap.Vector leapFingerPosiion4 = frame.Hands[0].Fingers[1].bones[3].Center;
                //Leap.Vector leapFingerPosiion5 = frame.Hands[0].Fingers[1].bones[4].Center;
                // Convert to our vector class, and then convert to our coordinate space
                Ultrahaptics.Vector3 uhPalmPosition = _alignment.fromTrackingPositionToDevicePosition(LeapToUHVector(leapPalmPosition));
                Ultrahaptics.Vector3 fingerpositon = _alignment.fromTrackingPositionToDevicePosition(LeapToUHVector(leapFingerPosiion));
                Ultrahaptics.Vector3 fingerpositon1 = _alignment.fromTrackingPositionToDevicePosition(LeapToUHVector(leapFingerPosiion1));
                Ultrahaptics.Vector3 fingerpositon2 = _alignment.fromTrackingPositionToDevicePosition(LeapToUHVector(leapFingerPosiion2));
                Ultrahaptics.Vector3 fingerpositon3 = _alignment.fromTrackingPositionToDevicePosition(LeapToUHVector(leapFingerPosiion3));
                Ultrahaptics.Vector3 fingerpositon4 = _alignment.fromTrackingPositionToDevicePosition(LeapToUHVector(leapFingerPosiion4));
                //Ultrahaptics.Vector3 fingerpositon5 = _alignment.fromTrackingPositionToDevicePosition(LeapToUHVector(leapFingerPosiion5));
                Ultrahaptics.Vector3 up = uhPalmPosition + new Ultrahaptics.Vector3((float)(2f * Units.centimetres), 0, 0);
                Ultrahaptics.Vector3 down = uhPalmPosition + new Ultrahaptics.Vector3((float)(-2f * Units.centimetres), 0, 0);
                Ultrahaptics.Vector3 left = fingerpositon3;
                Ultrahaptics.Vector3 right = fingerpositon4;
               // Ultrahaptics.Vector3 up1 = fingerpositon5;
                Ultrahaptics.Vector3 down1 = fingerpositon1;
                Ultrahaptics.Vector3 left1 = fingerpositon2;
                Ultrahaptics.Vector3 right1 = fingerpositon;
                // Create a control point object using this position, with full intensity, at 200Hz
                AmplitudeModulationControlPoint point = new AmplitudeModulationControlPoint(uhPalmPosition, 1.0f, 70.0f);
                AmplitudeModulationControlPoint point1 = new AmplitudeModulationControlPoint(up, 1.0f, 70.0f);
                AmplitudeModulationControlPoint point2 = new AmplitudeModulationControlPoint(down, 1.0f, 70.0f);
                AmplitudeModulationControlPoint point3 = new AmplitudeModulationControlPoint(left, 1.0f, 70.0f);
                AmplitudeModulationControlPoint point4 = new AmplitudeModulationControlPoint(right, 1.0f, 70.0f);
                //AmplitudeModulationControlPoint point5 = new AmplitudeModulationControlPoint(up1, 1.0f, 70.0f);
                AmplitudeModulationControlPoint point6 = new AmplitudeModulationControlPoint(down1, 1.0f, 70.0f);
                AmplitudeModulationControlPoint point7 = new AmplitudeModulationControlPoint(left1, 1.0f, 70.0f);
                AmplitudeModulationControlPoint point8 = new AmplitudeModulationControlPoint(right1, 1.0f, 70.0f);
                // Output this point
                if (flag)
                {
                    _emitter.update(new List<AmplitudeModulationControlPoint> {   point3, point4,point6,point7,point8 });
                }
                else
                {
                    _emitter.stop();
                }
                
            }
            else
            {
                Debug.LogWarning ("No hands detected");
                _emitter.stop();
            }
        }
        else
        {
            Debug.LogWarning ("No Leap connected");
            _emitter.stop();
        }
    }


    // Ensure the emitter is stopped when disabled
    void OnDisable()
    {
        _emitter.stop();
    }

    // Ensure the emitter is immediately disposed when destroyed
    void OnDestroy()
    {
        _emitter.Dispose();
        _emitter = null;
        _alignment.Dispose();
        _alignment = null;
    }

    IEnumerator WaitAndPrint(float waittime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waittime);
            flag = !flag;
        }
        
    }


}
