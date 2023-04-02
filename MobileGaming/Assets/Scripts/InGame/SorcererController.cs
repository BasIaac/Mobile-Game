using System;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SorcererController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameObject sorcererGo;
    [SerializeField] private MeshRenderer[] meshes;
    
    [field:SerializeField,Header("Components")] public TextMeshProUGUI currentProductText { get; private set; }
    [field:SerializeField] public TextMeshProUGUI timeLeftText { get; private set; }
    [field:SerializeField] public TextMeshProUGUI scoreText { get; private set; }
    [field:SerializeField] public TextMeshProUGUI endGameText { get; private set; }
    [field:SerializeField,] public Button endGameButton { get; private set; }
    [field:SerializeField,] public GameObject endGameCanvasGo { get; private set; }

    [Header("Cameras")]
    public GameObject perspCameraGo;
    public Camera orthoCam;
    public Camera perspCam;
    private Camera cam => !perspCameraGo.activeSelf ? orthoCam : perspCam;

    [Header("Joystick")]
    public RectTransform joystickParentTr;
    public RectTransform joystickTr;
    public GameObject joystickParentGo;
    
    [Header("Settings")]
    [SerializeField] private bool isNavMeshControlled = true;
    [SerializeField] private LayerMask layersToHit;
    [SerializeField] private float rayLenght;
    [SerializeField] private float speed = 10f;
    
    private NavMeshAgent agent;
    private Rigidbody rb;

    private bool isPressed;
    private Ray ray;
    private RaycastHit hit;
    
    private void Start()
    {
        endGameCanvasGo.SetActive(false);

        InputService.OnPress += OnScreenTouch;
        InputService.OnRelease += OnScreenRelease;

        if (!isNavMeshControlled) return;
       
        agent.speed = int.MaxValue;
        agent.acceleration = int.MaxValue;
        foreach (var mesh in meshes)
        {
            mesh.enabled = false;
        }
    }

    private void Update()
    {
        AgentMovement();
    }

    private void AgentMovement()
    {
        if(!isNavMeshControlled) return;
        if(isPressed) MoveToPosition(InputService.cursorPosition);
    }

    private void FixedUpdate()
    {
        JoystickMovement();
    }

    public void SetVariables()
    {
        isPressed = false;
        agent = sorcererGo.GetComponent<NavMeshAgent>();
        rb = sorcererGo.GetComponent<Rigidbody>();
        joystickParentGo = joystickParentTr.gameObject;
        joystickParentGo.SetActive(false);
        
        OnInteract = null;
    }

    private void JoystickMovement()
    {
        if(isNavMeshControlled) return;
        Vector3 movement = InputService.movement;
        (movement.y, movement.z) = (movement.z, movement.y);
        rb.velocity = movement * (speed * Time.deltaTime);
    }

    public void OnScreenTouch(Vector2 position)
    {
        isPressed = true;
        if(isNavMeshControlled) return;
        DisplayJoystick();
    }

    private void DisplayJoystick()
    {
        joystickParentTr.position = InputService.cursorPosition;
        joystickTr.localPosition = Vector3.zero;
        joystickParentGo.SetActive(true);
    }

    public void OnScreenRelease(Vector2 position)
    {
        isPressed = false;
        ResetJoystick();
        Interact();
    }

    private void ResetJoystick()
    {
        joystickParentGo.SetActive(false);
        joystickTr.localPosition = Vector3.zero;
    }

    private void MoveToPosition(Vector2 screenPosition)
    {
        ray = cam.ScreenPointToRay(screenPosition);
        Debug.DrawRay(ray.origin,ray.direction*rayLenght);
        if (Physics.Raycast(ray.origin,ray.direction, out hit,rayLenght,layersToHit))
        {
            agent.isStopped = false;
            agent.SetDestination(hit.point);
        }
    }
    
    private void Interact()
    {
        if(isNavMeshControlled) StopAgent();
        OnInteract?.Invoke();
    }

    public event Action OnInteract;  
    
    private void StopAgent()
    {
        agent.ResetPath();
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }
}
