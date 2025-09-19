using UnityEngine;
using UnityEngine.InputSystem;

public class PickupFlashlight : MonoBehaviour
{
    public Transform playerHand;    // Точка крепления фонарика в руке игрока
    public float pickupRange = 2f;  // Расстояние для подбора
    private bool equipped = false;  // Взяли ли фонарик
    private Transform player;
    private Light flashlight;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        flashlight = GetComponent<Light>();
        if (flashlight != null)
            flashlight.enabled = false;  // Фонарик изначально выключен
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

        // Вкл/выкл свет кнопкой F, если фонарик в руке
        if (equipped && Keyboard.current.fKey.wasPressedThisFrame)
        {
            if (flashlight != null)
                flashlight.enabled = !flashlight.enabled;
        }
    }

    void PickUp()
    {
        equipped = true;
        transform.SetParent(playerHand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = true;

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        if (flashlight != null)
            flashlight.enabled = true;

        Debug.Log("Фонарик подобран и включен");
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

        if (flashlight != null)
            flashlight.enabled = false;

        Debug.Log("Фонарик убран");
    }
}
