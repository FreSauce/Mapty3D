using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject searchMenu;
    public GameObject zoomMenu;
    public GameObject floodMenu;
    public GameObject earthquakeMenu;
    private bool isMainMenuOpen = false;
    private bool isFloodMenuOpen = false;
    private bool isEarthquakeMenuOpen = false;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if (isMainMenuOpen)
            {
                HideMainMenu();
            }
            else if (!isMainMenuOpen && !isFloodMenuOpen && !isEarthquakeMenuOpen)
            {
                ShowMainMenu();
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isFloodMenuOpen)
            {
                HideFloodMenu();
            }
            else if (!isMainMenuOpen && !isFloodMenuOpen && !isEarthquakeMenuOpen)
            {
                ShowFloodMenu();
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isEarthquakeMenuOpen)
            {
                HideEarthquakeMenu();
            }
            else if (!isMainMenuOpen && !isFloodMenuOpen && !isEarthquakeMenuOpen)
            {
                ShowEarthquakeMenu();
            }
        }
    }

    public void ShowMainMenu()
    {
        if (!isMainMenuOpen)
        {
            searchMenu.SetActive(true);
            zoomMenu.SetActive(true);
            isMainMenuOpen = true;
        }
    }

    public void HideMainMenu()
    {
        if (isMainMenuOpen)
        {
            searchMenu.SetActive(false);
            zoomMenu.SetActive(false);
            isMainMenuOpen = false;
        }
    }

    public void ShowFloodMenu()
    {
        if (!isFloodMenuOpen)
        {
            floodMenu.SetActive(true);
            isFloodMenuOpen = true;
        }
    }

    public void HideFloodMenu()
    {
        if (isFloodMenuOpen)
        {
            floodMenu.SetActive(false);
            isFloodMenuOpen = false;
        }
    }

    public void ShowEarthquakeMenu()
    {
        if (!isEarthquakeMenuOpen)
        {
            earthquakeMenu.SetActive(true);
            isEarthquakeMenuOpen = true;
        }
    }

    public void HideEarthquakeMenu()
    {
        if (isEarthquakeMenuOpen)
        {
            earthquakeMenu.SetActive(false);
            isEarthquakeMenuOpen = false;
        }
    }
}
