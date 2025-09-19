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
    }

    void PickUp()
    {
        equipped = true;
        transform.SetParent(playerHand);

        // Позиционируем пистолет в локальной позиции перед камерой (в руках)
        transform.localPosition = new Vector3(0f, 0f, 0.5f);
        transform.localRotation = Quaternion.identity;

        // Отключаем физику и коллайдер
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = true;

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        // Разрешаем стрельбу
        if (shooter != null)
            shooter.canShoot = true;
    }
}
