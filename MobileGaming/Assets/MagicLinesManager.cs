using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MagicLinesManager : MonoBehaviour
{
    // Public or Visible 
    [SerializeField] private TextMeshProUGUI debugMode;
    [SerializeField] private TextMeshProUGUI debugTimeScale;
    [SerializeField] private TextMeshProUGUI debugMana;

    [SerializeField] private LayerMask machineLayerMask;
    [SerializeField] private bool isInMagicMode;
    
    [SerializeField] private int maxMana;
    [SerializeField] private int currentMana;
    [SerializeField] private int bonusMana;
    public float timeToRecoverMana = 4.5f;

    [SerializeField] private Vector3 baseCamPos;
    [SerializeField] private Vector3 magicModeCamPos;
    
    public GameObject linePrefab;
    public Vector3[] points;
    public List<LineRenderer> magicLinks;

    // Private
    private bool isPressed;
    private Ray ray;
    private RaycastHit hit;
    private Machine m1;
    private Machine m2;
    private bool isDraging;
    private SorcererController player;



    private void Start()
    {
        isInMagicMode = false;
        debugMode.text = $"Magic Mode : {isInMagicMode}";
        player = GetComponent<SorcererController>();

        // Setup Cameras
        baseCamPos = player.perspCam.transform.position;
        magicModeCamPos = new Vector3(baseCamPos.x, baseCamPos.y, 0);

        // Mana
        currentMana = maxMana + bonusMana;
        UpdateManaDebug();
    }
    

    public void ToggleMagic()
    {
        isInMagicMode = !isInMagicMode;
        Debug.Log(isInMagicMode);
        debugMode.text = $"Magic Mode : {isInMagicMode}";

        // Controls
        if (isInMagicMode) OnEnableMagicMode();
        else OnDisableMagicMode();

        // Camera Pos & Rot
        player.perspCam.transform.position = isInMagicMode ? magicModeCamPos : baseCamPos;
        player.perspCam.transform.rotation = isInMagicMode ? Quaternion.Euler(90, 0, 0) : Quaternion.Euler(80, 0, 0);

        // Timescale 
        Time.timeScale = isInMagicMode ? .6f : 1;
        debugTimeScale.text = Time.timeScale.ToString();
    }


    private void OnEnableMagicMode()
    {
        InputService.OnPress += OnScreenTouch;
        InputService.OnRelease += OnScreenRelease;
        InputService.OnPress -= player.OnScreenTouch;
        InputService.OnRelease -= player.OnScreenRelease;
    }

    private void OnDisableMagicMode()
    {
        InputService.OnPress -= OnScreenTouch;
        InputService.OnRelease -= OnScreenRelease;
        InputService.OnPress += player.OnScreenTouch;
        InputService.OnRelease += player.OnScreenRelease;
    }

    #region Drag & Drop

    private void OnScreenTouch(Vector2 obj)
    {
        if (currentMana < 1) return;
        // Sfx can't interact
        // Vfx can't interact
        
        isPressed = true;
        m1 = GetClickMachine(obj);
        if (m1 != null) StartDrag();
    }

    private void OnScreenRelease(Vector2 obj)
    {
        isPressed = false;

        if (!isDraging) return;

        m2 = GetClickMachine(obj);
        if (m2 != null && m1 != m2) LinkMachines();

        UnlinkAll();
    }

    private void UnlinkAll()
    {
        isDraging = false;
        m1 = default;
        m2 = default;
    }

    private void StartDrag()
    {
        isDraging = true;
    }


    private void LinkMachines()
    {
        Debug.Log($"Les machines {m1} & {m2} sont link");
        currentMana -= 1;
        CreateMagicLine();
        StartCoroutine(RecoverMana(timeToRecoverMana));
    }

    private Machine? GetClickMachine(Vector2 mousePos)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        return Physics.Raycast(ray, out hit, machineLayerMask) ? hit.collider.gameObject.GetComponent<Machine>() : null;
    }

    #endregion

    #region Mana

    private void UpdateManaDebug()
    {
        debugMana.text = $"Mana : {currentMana}/{maxMana}";
    }

    private void CreateMagicLine()
    {
        var GO = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        LineRenderer lr = GO.GetComponent<LineRenderer>();
        magicLinks.Add(lr);
        points = new[] { m1.transform.position, m2.transform.position };
        lr.positionCount = points.Length;
        for (int i = 0; i < points.Length; i++)
        {
            lr.SetPosition(i, points[i] + Vector3.up);
        }
        GenerateMeshCollider(lr);
    }

    IEnumerator RecoverMana(float _timeToWait)
    {
        UpdateManaDebug();
        yield return new WaitForSeconds(_timeToWait);
        currentMana++;
        UpdateManaDebug();
    }

    #endregion

    #region Useful Methods

    private void GenerateMeshCollider(LineRenderer _lineRenderer)
    {
        MeshCollider collider;
        collider = _lineRenderer.gameObject.AddComponent<MeshCollider>();

        Mesh mesh = new Mesh();
        _lineRenderer.BakeMesh(mesh, true);
        collider.sharedMesh = mesh;
        _lineRenderer.gameObject.layer = LayerMask.NameToLayer("MagicLink");
    }
    
    /*[Space(10)][Header("Draw Line")]
    public GameObject currentLine;
    public LineRenderer lineRenderer;
    public List<Vector3> fingerPos;
    private void Update() 
    {
        if (!isDraging) return;
        if (Input.GetMouseButtonDown(0))
        {
            CreateLine();
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 uwu = Camera.main.ScreenToWorldPoint(InputService.cursorPosition);
            Vector3 tempFingerPos = new Vector3(uwu.x, 1, uwu.y);
            if (Vector3.Distance(tempFingerPos, fingerPos[^1]) > .1f)
            {
                UpdateLine(tempFingerPos);
            }
        }
    }

    private void CreateLine()
    {
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        fingerPos.Clear();
        var owo = Camera.main.ScreenToWorldPoint(InputService.cursorPosition);
        fingerPos.Add(new Vector3(owo.x, 1, owo.y));
        fingerPos.Add(new Vector3(owo.x, 1, owo.y));
        lineRenderer.SetPosition(0, fingerPos[0]);
        lineRenderer.SetPosition(1, fingerPos[1]);
    }

    private void UpdateLine(Vector3 newFingerPos)
    {
        fingerPos.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);
    }*/
    
    #endregion
}