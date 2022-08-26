using UnityEngine;
using UnityEngine.UI;

public class FloodControlScript : MonoBehaviour
{
    [SerializeField]
    private Slider floodSlider;
    [SerializeField]
    private Slider roadSlider;
    [SerializeField]
    private Slider waterSlider;
    [SerializeField]
    private Slider soilSlider;
    [SerializeField]
    private GameObject waterObject;
    [SerializeField]
    private FloodSimulator flood;

    // Start is called before the first frame update
    void Start()
    {
        floodSlider.onValueChanged.AddListener((v) =>
        {
            if (v == 0)
            {
                waterObject.SetActive(false);
            }
            else
            {
                waterObject.SetActive(true);
                flood.FloodColoring(v);
                waterObject.transform.position = new Vector3(waterObject.transform.position.x, v, waterObject.transform.position.z);
            }
        });

        roadSlider.onValueChanged.AddListener((v) =>
        {
            floodSlider.value -= FloodSimulator.RoadSurfaceArea*v;
            flood.FloodColoring(floodSlider.value);
            waterObject.transform.position = new Vector3(waterObject.transform.position.x, floodSlider.value, waterObject.transform.position.z);
        }
        );

        waterSlider.onValueChanged.AddListener((v) =>
        {
            floodSlider.value -= FloodSimulator.WaterSurfaceArea * v;
            flood.FloodColoring(floodSlider.value);
            waterObject.transform.position = new Vector3(waterObject.transform.position.x, floodSlider.value, waterObject.transform.position.z);

        }
        );

        soilSlider.onValueChanged.AddListener((v) =>
        {
            floodSlider.value -= FloodSimulator.LanduseSurfaceArea* v;
            flood.FloodColoring(floodSlider.value);
            waterObject.transform.position = new Vector3(waterObject.transform.position.x, floodSlider.value, waterObject.transform.position.z);
        }
        );
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
