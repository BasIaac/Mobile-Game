using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDropManager : MonoBehaviour
{
    [SerializeField] private LayerMask machineLayerMask;
    
    private bool isPressed;
    private Ray ray;
    private RaycastHit hit;
    private Machine m1;
    private Machine m2;
    private bool isDraging;

    private void Start()
    {
        InputService.OnPress += OnScreenTouch;
        InputService.OnRelease += OnScreenRelease;
    }
    
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
        if (m2 != null) LinkMachines();
        
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
            Debug.Log(hit.collider.gameObject.name);    
            return hit.collider.gameObject.GetComponent<Machine>();  
        }
        
        return null;
    }
}
