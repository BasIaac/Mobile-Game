using UnityEngine;
using UnityEngine.AI;

public class SorcererController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameObject sorcererGo;
    [SerializeField] private Camera cam;
    
    [Header("Settings")]
    [SerializeField] private bool isNavMeshControlled = true;
    [SerializeField] private LayerMask layersToHit;
    [SerializeField] private float rayLenght;
    
    private NavMeshAgent agent;
    private Rigidbody rb;

    private Ray ray;
    private RaycastHit hit;
    
    private void Start()
    {
       SetVariables();

       InputService.OnPress += OnScreenTouch;
       InputService.OnRelease += OnScreenRelease;
    }

    private void SetVariables()
    {
        agent = sorcererGo.GetComponent<NavMeshAgent>();
        rb = sorcererGo.GetComponent<Rigidbody>();
    }

    private void OnScreenTouch(Vector2 position)
    {
        if (isNavMeshControlled)
        {
            MoveToPosition(position);
            return;
        }
    }

    private void OnScreenRelease(Vector2 position)
    {
        Interact();
    }

    private void MoveToPosition(Vector2 screenPosition)
    {
        ray = cam.ScreenPointToRay(screenPosition);
        Debug.DrawRay(ray.origin,ray.direction*rayLenght,Color.green,1f);
        if (Physics.Raycast(ray.origin,ray.direction, out hit,rayLenght,layersToHit))
        {
            agent.isStopped = false;
            agent.SetDestination(hit.point);
            
            Debug.Log("Hit");
            Debug.DrawLine(ray.origin,hit.point,Color.yellow);
        }
    }

    private void Interact()
    {
        if(isNavMeshControlled) StopAgent();
    }

    private void StopAgent()
    {
        agent.ResetPath();
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }
}
