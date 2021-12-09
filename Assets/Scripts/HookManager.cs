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
    
    void Start()
    {
        mainCamera = Camera.main;

        player = FindObjectOfType<SnakeMovement>();

        hookBase = new Vector3(player.transform.position.x,player.transform.position.y,-0.5f);
        hook = Instantiate(hook, hookBase, quaternion.identity);
    }
    
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
                hook.transform.position = Vector3.Lerp(hook.transform.position, mouseWorldPosition, 0.01f);
            

            if (!Input.GetMouseButton(0))
                hook.transform.position = hookBase;
             


            if (Input.GetMouseButton(0))
                hookShot = true;
            

            if (!Input.GetMouseButton(0))
                hookShot = false;
            
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            player._hasEaten = true;
            Destroy(other.gameObject);
        }
    }
}
