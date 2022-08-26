using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapzen;

public class EarthquakeSimulator : MonoBehaviour
{
    public RegionMap map;
    private List<KeyValuePair<GameObject, Vector3>> vectorMap = new List<KeyValuePair<GameObject, Vector3>>();

    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;
    
    void Start()
    {
        gradient = new Gradient();
        map = gameObject.GetComponent<RegionMap>();
        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey = new GradientColorKey[2];
        colorKey[0].color = Color.white;
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.red;
        colorKey[1].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 0.0f;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CalculateVector3()
    {
        Transform bldg = map.regionMap.transform.Find("Buildings");
        Transform[] temp = bldg.Find("buildings").GetComponentsInChildren<Transform>();
        foreach (Transform item in temp)
        {
            MeshCollider meshCollider = item.gameObject.GetComponent<MeshCollider>();
            vectorMap.Add(new KeyValuePair<GameObject, Vector3>(item.gameObject, meshCollider.bounds.center));
        }

    }

    public bool Region(Vector3 center, Vector3 point, float radius)
    {
        float x = center.x, z = center.z;
        float distance = (point.x - x) * (point.x - x) + (point.z - z) * (point.z - z) - radius * radius;

        if (distance <= 0f) return true;

        return false;
    }

    public float EqScale(Vector3 center, Vector3 point, float chkmag, float buildmag)
    {
        float x = center.x, z = center.z;
        float distance = Mathf.Sqrt((point.x - x) * (point.x - x) + (point.z - z) * (point.z - z));
        float effectivemag = chkmag / distance * distance;
        if (buildmag <= effectivemag) return 100f;

        float percentage = (effectivemag / buildmag) * 100;
        return percentage;
    }

    public void EathquakeColoring(Vector3 point,float radius,float chkmag)
    {
        foreach (var item in vectorMap)
        {
            if (item.Key == null || item.Key.name == "buildings")
            {
                continue;
            }
            if(Region(point,item.Value,radius))
            {
                item.Key.GetComponent<MeshRenderer>().material.color = gradient.Evaluate(EqScale(point,item.Value, chkmag, Random.value*10)/150f);
            }
        }
    }
}
