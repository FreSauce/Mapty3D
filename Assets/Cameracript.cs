using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapzen;

public class Cameracript : MonoBehaviour
{

    [SerializeField] Camera pickPoinCamera;
    public RegionMap map;

    // Start is called before the first frame update
    void Start()
    {
        map = gameObject.GetComponent<RegionMap>();
        getMapCeter();

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void getMapCeter()
    {
        Vector3 center = map.transform.position;
        Debug.Log(center);
        pickPoinCamera.transform.position = center;
    }
}
