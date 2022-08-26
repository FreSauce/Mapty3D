using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapzen;

public class Cameracript : MonoBehaviour
{

    [SerializeField] public Camera pickPoinCamera;
    public RegionMap map;

    void Start()
    {
        map = gameObject.GetComponent<RegionMap>();
    }
    
    public void getMapCeter()
    {
        Vector3 center = map.regionMap.GetComponent<MeshCollider>().bounds.max;
        Debug.Log(center);
        GameObject.FindGameObjectWithTag("TopCamera").transform.position = new Vector3(center.x,center.y,center.z);
    }
}
