using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;
using UnityEngine.SceneManagement;

public class ARAimPoint : MonoBehaviour
{
    public GameObject cursorChildObject;
    public GameObject[] objectsToPlace;
    public GameObject carObject;
    public ARRaycastManager raycastManager;
    public Transform carSpawnPoint;

    public int objectIndex = 0;

    public bool useCursor = true, canSpawnCollectibles = true, gameHasStarted = false, timerIsOn = false;

    public TMP_Text planetsToCollectText;
    public int planetsToColectIndex = 0;

    public TMP_Text timeToGoText;
    public float timeToGoIndex, carSpeed;
    [SerializeField] float timePerSpawnedPlanet;

    [SerializeField] GameObject WinningPanel;
    public GameObject losePanel, plrControllers;

    private void Awake()
    {
        gameHasStarted = false;
        timerIsOn = false;
    }

    private void Start()
    {
        cursorChildObject.SetActive(useCursor);
        canSpawnCollectibles = true;
        planetsToCollectText.text = planetsToColectIndex.ToString();
        timeToGoText.text = timeToGoIndex.ToString();
    }

    private void Update()
    {

        string tempTimer = string.Format("{0:00}", timeToGoIndex);
        timeToGoText.text = tempTimer.ToString();

        if (useCursor && canSpawnCollectibles)
        {
            UpdateCursor();
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && canSpawnCollectibles)
        {
            if (useCursor)
            {
                GameObject.Instantiate(objectsToPlace[objectIndex], transform.position, transform.rotation);
                planetsToColectIndex++;
                timeToGoIndex += timePerSpawnedPlanet;
            }
            else
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

                if (RaycastBlockedByUI(hits[0].pose.position))
                {
                    Debug.Log("Ei voi instansioida objektia!");
                }
                if (RaycastBlockedByUI(hits[0].pose.position) == false)
                {
                    var currentObj = GameObject.Instantiate(objectsToPlace[objectIndex], hits[0].pose.position, hits[0].pose.rotation);
                    planetsToColectIndex++;
                    timeToGoIndex += timePerSpawnedPlanet;
                }
                /*
                if (hits.Count > 0)
                {
                    
                }
                */
            }
        }

        planetsToCollectText.text = planetsToColectIndex.ToString();


        if (gameHasStarted && planetsToColectIndex <= 0)
        {
            WinningPanel.SetActive(true);
            plrControllers.SetActive(false);
        }

        if (timerIsOn)
        {
            timeToGoIndex -= 1 * Time.deltaTime;
        }

        if(gameHasStarted && timeToGoIndex <= 0)
        {
            timerIsOn = false;
            timeToGoText.text = "0";
            losePanel.SetActive(true);
            plrControllers.SetActive(false);
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
        var carInstance = Instantiate(carObject, carSpawnPoint.position, Quaternion.identity);
        gameHasStarted = true;
    }

    public void QuitThisGame()
    {
        Application.Quit();
    }

    public void StartAgain()
    {
        SceneManager.LoadScene(0);
    }

    private bool RaycastBlockedByUI(Vector2 touchPosition)
    {
        if (PointerOverUI.IsPointerOverUIObject(touchPosition))
        {
            return false;
        }

        return false;
        //return RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon);
    }
}

