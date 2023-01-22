using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraManager : MonoBehaviour
{
    public static MainCameraManager Instance;
    public Camera mainCam;
    private void Awake()
    {
        Instance = this;
    }
    public void changeCamColor(string colorHex)
    {
        Color nC = new Color();
        if (ColorUtility.TryParseHtmlString(colorHex, out nC))
        {
            mainCam.backgroundColor = nC;
        }
    }

}
