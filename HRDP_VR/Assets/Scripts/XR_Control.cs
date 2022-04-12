using System.Collections;
using System.Collections.Generic;


using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
public class XR_Control : MonoBehaviour
{
    public InputDevice rightHand;
    public InputDevice leftHand;

    public GameObject UI;

    public bool leftTriggerValue = false;
    public bool cur_UI_state = false;
    // Start is called before the first frame update
    void Start()
    {

        List<InputDevice> rightdevices = new List<InputDevice>();
        List<InputDevice> leftdevices = new List<InputDevice>();
        InputDeviceRole righthand = InputDeviceRole.RightHanded;
        InputDeviceRole lefthand = InputDeviceRole.LeftHanded;

        InputDevices.GetDevicesWithRole(righthand, rightdevices);
        if (rightdevices.Count > 0)
        {
            rightHand = rightdevices[0];
        }
        InputDevices.GetDevicesWithRole(lefthand, leftdevices);
        if (leftdevices.Count > 0)
        {
            leftHand = leftdevices[0];
        }
        Debug.Log(leftHand);
        Debug.Log(rightHand);
    }

    // Update is called once per frame
    void Update()
    {
            //Right hand trigger
            leftHand.TryGetFeatureValue(CommonUsages.gripButton, out bool test);
        
            
            //leftHand.TryGetFeatureValue(CommonUsages.triggerButton, out bool test1);
            //Debug.Log(test1);
            //Debug.Log(test);
            //rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out bool test3);
            //rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out bool test4);
            //Debug.Log(test3);
            //Debug.Log(test4);

            leftTriggerValue = test;
            if (leftTriggerValue)
            {
                if (!cur_UI_state)
                {
                    cur_UI_state = true;
                    UI.SetActive(true);
                }
            }
            else
            {
                if (cur_UI_state)
                {
                    cur_UI_state = false;
                    //UI.SendMessage("Control_Whole_UI", false);
                    UI.SetActive(false);
                }
            }

        
    }
}
