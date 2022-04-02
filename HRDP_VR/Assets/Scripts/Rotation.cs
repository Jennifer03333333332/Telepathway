using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float rotationSpeed = 1f;
    private float Max_Rotation = 80;
    // Start is called before the first frame update
    public void SetRotationSpeed(float rotateSpeedUpdate)
    {
        rotationSpeed = rotateSpeedUpdate;
    }

    // Update is called once per frame
    void Update()
    {
        // float Y = Mathf.PingPong(Time.time,Max_Rotation * 2);
        // Y -= Max_Rotation;
        // transform.SetPositionAndRotation(this.transform.position, new Quaternion(0, Y , 0, 1));
        transform.Rotate(new Vector3(0, rotationSpeed, 0), Space.Self);
    }
    public void HaltRotation()
    {
        transform.rotation = Quaternion.identity;
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
    public void ZoomChanged(float zoomAmount)
    {
        var zoomVal = zoomAmount * zoomAmount;
        transform.localScale = new Vector3(zoomVal, zoomVal, zoomVal);
    }
}
