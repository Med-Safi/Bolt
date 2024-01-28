using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DrawingManager : MonoBehaviour
{
    public LineRenderer lineRendererPrefab;
    public RawImage drawingArea; 
    public GameObject prefab; 
    public static int maxPrefabs = 20;
    private LineRenderer currentLineRenderer;
    private List<Vector2> fingerPositions;
    private Camera mainCamera;
    private RectTransform canvasRectTransform;
    private RectTransform drawingAreaRectTransform;
    private List<GameObject> currentPrefabs; 
    private Vector3 basePosition = new Vector3(0, -11, 10); 

    void Start()
    {
        {
            mainCamera = Camera.main;
            fingerPositions = new List<Vector2>();
            currentPrefabs = new List<GameObject>();

            float horizontalSpacing = 1.0f; 
            float verticalSpacing = 1.0f; 

            for (int row = 0; row < 2; row++)
            {
                for (int i = 0; i < maxPrefabs; i++)
                {
                    Vector3 position = new Vector3(i * horizontalSpacing -10 , -11 , row * verticalSpacing +8);
                    GameObject newPrefab = Instantiate(prefab, position, Quaternion.identity);
                    currentPrefabs.Add(newPrefab);
                }
            }
        }


        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            canvasRectTransform = canvas.GetComponent<RectTransform>();
        }
        else
        {
            Debug.LogError("Canvas not found in parent objects!");
        }

        if (drawingArea != null)
        {
            drawingAreaRectTransform = drawingArea.GetComponent<RectTransform>();
        }
        else
        {
            Debug.LogError("Drawing Area (Raw Image) not assigned!");
        }
    }
    

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && IsPointerOverDrawingArea())
        {
            CreateLine();
        }
        if (Input.GetMouseButton(0) && currentLineRenderer != null)
        {
            if (IsPointerOverDrawingArea())
            {
                Vector2 localCursor;
                if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, Input.mousePosition, mainCamera, out localCursor))
                    return;

                Vector2 tempFingerPos = canvasRectTransform.TransformPoint(localCursor);
                if (Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > .1f)
                {
                    UpdateLine(tempFingerPos);
                }
            }
            else
            {
                FinalizeLine();
            }
        }
        if (Input.GetMouseButtonUp(0) && currentLineRenderer != null)
        {
            FinalizeLine();
        }
    }

    private bool IsPointerOverDrawingArea()
    {
        Camera usedCamera = canvasRectTransform.GetComponentInParent<Canvas>().renderMode == RenderMode.ScreenSpaceOverlay ? null : mainCamera;
        return RectTransformUtility.RectangleContainsScreenPoint(drawingAreaRectTransform, Input.mousePosition, usedCamera);
    }

    void CreateLine()
    {
        currentLineRenderer = Instantiate(lineRendererPrefab, Vector3.zero, Quaternion.identity, transform);
        currentLineRenderer.startWidth = 0.1f;
        currentLineRenderer.endWidth = 0.1f;
        fingerPositions.Clear();
        fingerPositions.Add(GetCanvasPosition(Input.mousePosition));
        fingerPositions.Add(GetCanvasPosition(Input.mousePosition));
        currentLineRenderer.SetPosition(0, fingerPositions[0]);
        currentLineRenderer.SetPosition(1, fingerPositions[1]);
    }

    void UpdateLine(Vector2 newFingerPos)
    {
        fingerPositions.Add(newFingerPos);
        currentLineRenderer.positionCount++;
        currentLineRenderer.SetPosition(currentLineRenderer.positionCount - 1, newFingerPos);
    }

    private Vector3 GetCanvasPosition(Vector3 screenPosition)
    {

        Vector2 localCursor;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, screenPosition, mainCamera, out localCursor);
        return canvasRectTransform.TransformPoint(localCursor);

    }
    void FinalizeLine()
    {
        if (currentLineRenderer != null)
        {
            Destroy(currentLineRenderer.gameObject);
        }
        CreatePrefabLine(fingerPositions);
        fingerPositions.Clear();
    }

    void CreatePrefabLine(List<Vector2> linePoints)
    { 
        foreach (GameObject obj in currentPrefabs)
        {
            Destroy(obj);
        }
        currentPrefabs.Clear();

        for (int i = 0; i < Mathf.Min(maxPrefabs, linePoints.Count); i++)
        {
            Vector3 position = new Vector3(linePoints[i].x, 0, linePoints[i].y) + basePosition;
            GameObject newPrefab = Instantiate(prefab, position, Quaternion.identity);
            currentPrefabs.Add(newPrefab);
        }
    }
    public void CollectPrefab()
    {
        Debug.Log("first stage collect");
        if (currentPrefabs.Count > 0)
        {
            Debug.Log("first stage collect");
            Debug.Log(maxPrefabs);
            Vector3 lastPrefabPosition = currentPrefabs[currentPrefabs.Count - 1].transform.position;

            Vector3 newPosition = lastPrefabPosition + new Vector3(0, 0, -1.0f); 

            GameObject newPrefab = Instantiate(prefab, newPosition, Quaternion.identity);
            currentPrefabs.Add(newPrefab);
        }
        else
        {
            Vector3 startPosition = new Vector3(0, 0, 0); // Adjust as needed
            GameObject newPrefab = Instantiate(prefab, startPosition, Quaternion.identity);
            currentPrefabs.Add(newPrefab);
        }
    }

    public void DestroyPrefab()
    {
        Debug.Log("first stage destroy");
        Debug.Log(maxPrefabs);
        if (currentPrefabs.Count > 0)
        {
            Debug.Log("2 stage destroy");
            GameObject prefabToDestroy = currentPrefabs[currentPrefabs.Count - 1];
            currentPrefabs.Remove(prefabToDestroy);
            Destroy(prefabToDestroy);
        }
    }

}

