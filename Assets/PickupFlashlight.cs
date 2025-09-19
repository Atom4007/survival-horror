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

        // Подобрать фонарик нажатием Е, если рядом и не взят
        if (!equipped && distance <= pickupRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            PickUp();
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

        // Сделать фонарик дочерним объектом руки
        transform.SetParent(playerHand);

        // Обнулить позицию и поворот локально в руке
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        // Отключить физику и коллайдер
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = true;

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        // Включить свет
        if (flashlight != null)
            flashlight.enabled = true;

        Debug.Log("Фонарик подобран и включен");
    }
}
