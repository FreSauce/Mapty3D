using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EarthquakeHandler : MonoBehaviour
{
    public TMP_InputField X_Coordn;
    public TMP_InputField Z_Coordn;
    public TMP_InputField magInput;
    public GameObject regionMap;

    public void SetCoord(float x, float z)
    {
        X_Coordn.text = x.ToString();
        Z_Coordn.text = z.ToString();
    }

    public void applyEarthquake()
    {
        float x = float.Parse(X_Coordn.text);
        float z = float.Parse(Z_Coordn.text);
        float mag = float.Parse(magInput.text);
        Debug.Log(x + " " + z);
        regionMap.GetComponent<EarthquakeSimulator>().EathquakeColoring(new Vector3(x, 0, z), mag*100, mag);
    }

    public void Update()
    {
        if (Input.GetKey("r"))
        {
            applyEarthquake();
        }
    }
}
