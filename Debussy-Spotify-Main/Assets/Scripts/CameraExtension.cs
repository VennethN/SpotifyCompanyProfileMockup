using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraExtension : MonoBehaviour
{
    // Start is called before the first frame update
    public void changeCamColor(string colorHex)
    {
        MainCameraManager.Instance.changeCamColor(colorHex);
    }
}
