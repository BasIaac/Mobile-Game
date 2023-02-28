using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDropSystem : MonoBehaviour
{
    public InputAction mouseClick;
    private Camera mainCam;
    
    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void OnEnable()
    {
        mouseClick.Enable();
        mouseClick.performed += MousePressed;
    }

    private void OnDisable()
    {
        mouseClick.performed -= MousePressed;
        mouseClick.Disable();
    }

    private void MousePressed(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider != null) 
                StartCoroutine(DragUpdated(hit.collider.gameObject));
            
        }
    }

    private IEnumerator DragUpdated(GameObject clickObject)
    {
        float initalDst = Vector3.Distance(clickObject.transform.position, Camera.main.transform.position);
        clickObject.TryGetComponent<Rigidbody>(out var rb);
        
        while (mouseClick.ReadValue<float>() != 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (rb != null)
            {
                Vector3 dir = ray.GetPoint(initalDst) - clickObject.transform.position;
                rb.velocity = dir * 10;
                yield return new WaitForFixedUpdate();
            }
            else
            {
                var velocity = Vector3.zero;
                clickObject.transform.position = Vector3.SmoothDamp(clickObject.transform.position, ray.GetPoint(initalDst), ref velocity, .1f);
                yield return null;
            }
        }
    }
    
    /*public bool isDraging;
    public LayerMask triggerMask;

    public Machine machine1;
    public Machine machine2;
    
    private bool b1;
    private bool b2;
    [CanBeNull]
    private Machine? GetClickMachine()
    {
        Ray ray = Camera.main.ScreenPointToRay(InputService.cursorPosition);

        if (Physics.Raycast(ray, out RaycastHit hit, triggerMask))
        {
            Debug.Log(hit.collider.gameObject.name);
            return hit.collider.gameObject.GetComponent<Machine>();
        }
        
        return null;
    }*/
}
