using System;
using System.Collections;
using System.Net.Mail;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MagicLinesManager : MonoBehaviour
{
    // Public or Visible 
    [SerializeField] private TextMeshProUGUI debugMode;
    [SerializeField] private TextMeshProUGUI debugTimeScale;
    
    [SerializeField] private LayerMask machineLayerMask;
    [SerializeField] private bool isInMagicMode;
    [SerializeField] private int maxMana;
    [SerializeField] private int bonusMana;
    [SerializeField] private int currentMana;

    [SerializeField] private Vector3 baseCamPos;
    [SerializeField] private Vector3 magicModeCamPos;
    
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
        player.perspCam.transform.rotation = isInMagicMode ? Quaternion.Euler(90,0,0) : Quaternion.Euler(80,0,0);
        
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
        isPressed = true;
        m1 = GetClickMachine(obj);
        if (m1 != null) StartDrag();
    }

    private void OnScreenRelease(Vector2 obj)
    {
        isPressed = false;
        if (!isDraging ) return;
        
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
    }

    private Machine? GetClickMachine(Vector2 mousePos)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out hit, machineLayerMask))
        {
            return hit.collider.gameObject.GetComponent<Machine>();  
        }
        
        return null;
    }
    
    #endregion

    #region Mana
    
    
    #endregion
    
}
