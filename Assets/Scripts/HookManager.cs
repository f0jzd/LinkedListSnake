using Unity.Mathematics;
using UnityEngine;

public class HookManager : MonoBehaviour
{
    private SnakeMovement player;
    private Camera mainCamera;

    [SerializeField] private GameObject hook;

    private bool isDead;
    private Vector3 hookBase;
    private bool hookShot;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;

        player = FindObjectOfType<SnakeMovement>();

        hookBase = new Vector3(player.transform.position.x,player.transform.position.y,-0.5f);
        hook = Instantiate(hook, hookBase, quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (!player.isDead)
        {


            hookBase = new Vector3(player.transform.position.x, player.transform.position.y, -0.5f);
            Vector3 mousePos = Input.mousePosition;
            var mouseWorldPosition = mainCamera.ScreenToWorldPoint(mousePos);
            mouseWorldPosition.z = 0;

            Vector3 targetDir = mouseWorldPosition - transform.position;
            float angle = Mathf.Atan2(targetDir.x, targetDir.y) * -Mathf.Rad2Deg;
            hook.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);



            if (Input.GetMouseButton(0))
            {
                hook.transform.position = Vector3.Lerp(hook.transform.position, mouseWorldPosition, 0.01f);
                //_lineRenderer.SetPosition(1, Vector3.Lerp(_lineRenderer.GetPosition(1), mouseWorldPosition, 0.01f));
            }

            if (!Input.GetMouseButton(0))
            {
                hook.transform.position = hookBase;
                //_lineRenderer.SetPosition(1, Vector3.Lerp(_lineRenderer.GetPosition(1), _lineRenderer.GetPosition(0), 0.01f));
            }


            if (Input.GetMouseButton(0))
            {
                hookShot = true;
            }

            if (!Input.GetMouseButton(0))
            {
                hookShot = false;
            }
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            
            Debug.Log("hit fruit");
            player._hasEaten = true;
            Destroy(other.gameObject);
        }
    }
}
