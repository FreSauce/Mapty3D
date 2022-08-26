using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapzen;

public class FloodSimulator : MonoBehaviour
{
    // Start is called before the first frame update
    private RegionMap map;
    public GameObject prefab;
    public LineRenderer lineRenderer;
    private List<KeyValuePair<GameObject, float>> heightMap = new List<KeyValuePair<GameObject, float>>();
    public static float RoadSurfaceArea = 0f;
    public static float WaterSurfaceArea = 0f;
    public static float LanduseSurfaceArea = 0f;

    Gradient gradient;
    Gradient rendGrad;
    GradientColorKey[] colorKey;
    GradientColorKey[] rendKey;
    GradientAlphaKey[] alphaKey;
    GradientAlphaKey[] alphaKey1;



    void Start()
    {
        gradient = new Gradient();
        rendGrad = new Gradient();
        map = gameObject.GetComponent<RegionMap>();
        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey = new GradientColorKey[2];
        colorKey[0].color = Color.white;
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.red;
        colorKey[1].time = 1.0f;

        rendKey = new GradientColorKey[2];
        rendKey[0].color = Color.red;
        rendKey[0].time = 0.0f;
        rendKey[1].color = Color.green;
        rendKey[1].time = 1.0f;


        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 0.0f;
        alphaKey[1].time = 1.0f;

        alphaKey1 = new GradientAlphaKey[2];
        alphaKey1[0].alpha = 1.0f;
        alphaKey1[0].time = 0.0f;
        alphaKey1[1].alpha = 0.0f;
        alphaKey1[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);
        rendGrad.SetKeys(rendKey, alphaKey1);

        // What's the color at the relative time 0.25 (25 %) ?
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void CalculateHeights()
    {
        Transform bldg = map.regionMap.transform.Find("Buildings");
        Transform[] temp = bldg.Find("buildings").GetComponentsInChildren<Transform>();
        Debug.Log(temp[0]);
        foreach (Transform item in temp)
        {
            Debug.Log(temp);
            MeshCollider meshCollider = item.gameObject.GetComponent<MeshCollider>();
            float height = meshCollider.bounds.max.y;
            //- meshCollider.bounds.min.y;
            heightMap.Add(new KeyValuePair<GameObject, float>(item.gameObject, height));
            Debug.Log(item + "-> " + height);
        }
        CalculatePath();
        CalculateSurfaceArea();
    }

    public void FloodColoring(float FloodValue)
    {
        foreach (var item in heightMap)
        {
            if (item.Key == null || item.Key.name == "buildings") continue;
            item.Key.GetComponent<MeshRenderer>().material.color = gradient.Evaluate(FloodValue * 4 / item.Value);
        }
    }

    public void CalculateSurfaceArea()
    {
        Transform rd = map.regionMap.transform.Find("Roads");
        Transform[] rd1 = rd.Find("roads").GetComponentsInChildren<Transform>();
        foreach (Transform item in rd1)
        {
            MeshCollider meshCollider = item.gameObject.GetComponent<MeshCollider>();
            if (!meshCollider) continue;
            float len = meshCollider.bounds.max.x - meshCollider.bounds.min.x;
            float wid = meshCollider.bounds.max.z - meshCollider.bounds.min.z;
            RoadSurfaceArea += len * wid;
        }
        rd = map.regionMap.transform.Find("Water");
        rd1 = rd.Find("water").GetComponentsInChildren<Transform>();
        foreach (Transform item in rd1)
        {
            MeshCollider meshCollider = item.gameObject.GetComponent<MeshCollider>();
            if (!meshCollider) continue;
            float len = meshCollider.bounds.max.x - meshCollider.bounds.min.x;
            float wid = meshCollider.bounds.max.z - meshCollider.bounds.min.z;
            WaterSurfaceArea += len * wid;
        }
        WaterSurfaceArea = WaterSurfaceArea / 2;
        rd = map.regionMap.transform.Find("Landuse");
        rd1 = rd.Find("landuse").GetComponentsInChildren<Transform>();
        foreach (Transform item in rd1)
        {
            MeshCollider meshCollider = item.gameObject.GetComponent<MeshCollider>();
            if (!meshCollider) continue;
            float len = meshCollider.bounds.max.x - meshCollider.bounds.min.x;
            float wid = meshCollider.bounds.max.z - meshCollider.bounds.min.z;
            LanduseSurfaceArea += len * wid;
        }
        float total = LanduseSurfaceArea + WaterSurfaceArea + RoadSurfaceArea;
        LanduseSurfaceArea = LanduseSurfaceArea / total;
        RoadSurfaceArea = RoadSurfaceArea / total;
        WaterSurfaceArea = WaterSurfaceArea / total;
        Debug.Log(LanduseSurfaceArea + " " + WaterSurfaceArea + " " + RoadSurfaceArea);
    }

    public void CalculatePath()
    {
        float i = 0;
        heightMap.Sort(delegate (KeyValuePair<GameObject, float> a, KeyValuePair<GameObject, float> b)
        {
            return (int)(a.Value - b.Value);
        });
        lineRenderer.positionCount = 32;
        foreach (var item in heightMap)
        {
            if (i >= 32) break;
            if (item.Key == null || item.Key.name == "buildings") continue;
            MeshCollider temp = item.Key.gameObject.GetComponent<MeshCollider>();
            Vector3 pos = temp.bounds.center;
            pos.y = 400f;
            GameObject pre = Instantiate(prefab, pos, Quaternion.identity);
            pre.GetComponent<MeshRenderer>().material.color = rendGrad.Evaluate(i / 31);
            lineRenderer.SetPosition((int)i, pos);
            i++;
        }
    }

}
