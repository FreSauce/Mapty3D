using UnityEngine;
using TMPro;
using Mapzen;

public class ButtonHandler : MonoBehaviour
{
    public GameObject searchWindow;
    public TMP_InputField inputField;
    public GeoCoder geoCoder;
    public GameObject mainCanvas;
    public RegionMap map;
    public void HideHandler()
    {
        mainCanvas.GetComponent<MenuScript>().HideMainMenu();
    }

    public void SearchHandler()
    {
        geoCoder.Search(inputField.text);
        map.regionMap.GetComponent<Cameracript>().getMapCeter();
    }
}
