using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Epicenter : MonoBehaviour
{
    [SerializeField] Camera droneCamera;
    [SerializeField] GameObject pinObject;
    [SerializeField] Camera pickPointCamera;
    int scrollValue = 10;

    private Vector3 dragOrigin;

    // Start is called before the first frame update
    void Start()
    {
        pickPointCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            droneCamera.enabled = !droneCamera.enabled;
            pickPointCamera.enabled = !pickPointCamera.enabled;
        }

        if (Input.GetMouseButtonDown(0) && pickPointCamera.enabled)
        {
            Ray ray = pickPointCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10000))
            {
                //pickPointCamera.enabled = !pickPointCamera.enabled;
                Debug.Log(hit.point);
                pinObject.transform.position = hit.point;
                //droneCamera.transform.position = hit.transform.gameObject.transform.position;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && pickPointCamera.enabled) // forward
        {
            //Debug.Log("uppp");
            //pickPointCamera.orthographicSize++;
            pickPointCamera.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - scrollValue, gameObject.transform.position.z);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f && pickPointCamera.enabled) // backwards
        {
            //Debug.Log("down");
            pickPointCamera.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + scrollValue, gameObject.transform.position.z);
            //pickPointCamera.orthographicSize--;
        }

        panCamera();
    }

    private void panCamera()
    {
        if (Input.GetMouseButtonDown(2))
            dragOrigin = pickPointCamera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(2))
        {
            //Debug.Log("fasdg");
            Vector3 diff = dragOrigin - pickPointCamera.ScreenToWorldPoint(Input.mousePosition);
            pickPointCamera.transform.position += diff;
        }
    }
}
