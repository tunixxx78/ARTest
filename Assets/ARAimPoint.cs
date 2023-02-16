using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARAimPoint : MonoBehaviour
{
    public GameObject cursorChildObject;
    public GameObject[] objectsToPlace;
    public GameObject carObject;
    public ARRaycastManager raycastManager;
    public Transform carSpawnPoint;

    public int objectIndex = 0;

    public bool useCursor = true, canSpawnCollectibles = true;

    private void Start()
    {
        cursorChildObject.SetActive(useCursor);
        canSpawnCollectibles = true;
    }

    private void Update()
    {
        if (useCursor && canSpawnCollectibles)
        {
            UpdateCursor();
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && canSpawnCollectibles)
        {
            if (useCursor)
            {
                GameObject.Instantiate(objectsToPlace[objectIndex], transform.position, transform.rotation);
            }
            else
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

                if (hits.Count > 0)
                {
                    var currentObj = GameObject.Instantiate(objectsToPlace[objectIndex], hits[0].pose.position, hits[0].pose.rotation);
                    carSpawnPoint.position = currentObj.transform.position;
                }

            }
        }
    }

    void UpdateCursor()
    {
        Vector2 screenpoint = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenpoint, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;

        }
    }

    public void SelectedObject(int sellectionIndex)
    {
        objectIndex = sellectionIndex;
    }

    public void SpawnCarAndStartRace()
    {
        canSpawnCollectibles = false;

        //var carInstance = Instantiate(carObject, carSpawnPoint.position, Quaternion.EulerRotation(90, 0, 0));
    }
}

