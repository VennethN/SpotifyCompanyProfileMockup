using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class URLButton : MonoBehaviour
{
    public string urlToOpen;
    public void onOpenClick()
    {
        Application.OpenURL(urlToOpen);
    }

}
