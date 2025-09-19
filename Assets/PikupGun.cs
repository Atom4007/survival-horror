using UnityEngine;
using UnityEngine.InputSystem;

public class PickupGun : MonoBehaviour
{
    public Transform playerHand;  // точка крепления пистолета (например, пустой объект в руках игрока)
    public float pickupRange = 2f;
    private bool equipped = false; // пистолет взят или нет
    private Transform player;
    private Shooter shooter;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        shooter = GetComponent<Shooter>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (!equipped && distance <= pickupRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            PickUp();
        }
        else if (equipped && Keyboard.current.gKey.wasPressedThisFrame)
        {
            PutDown();
        }
    }

    void PickUp()
    {
        equipped = true;
        transform.SetParent(playerHand);
        transform.localPosition = new Vector3(0f, 0f, 0.5f);
        transform.localRotation = Quaternion.identity;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = true;

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        if (shooter != null)
            shooter.canShoot = true;
    }

    void PutDown()
    {
        equipped = false;
        transform.SetParent(null);

        transform.position = player.position + player.forward * 1f;
        transform.rotation = Quaternion.identity;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = false;

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = true;

        if (shooter != null)
            shooter.canShoot = false;
    }
}
