using UnityEngine;
using UnityEngine.InputSystem;

public class PickupFlashlight : MonoBehaviour
{
    public Transform playerHand;    // ����� ��������� �������� � ���� ������
    public float pickupRange = 2f;  // ���������� ��� �������
    private bool equipped = false;  // ����� �� �������
    private Transform player;
    private Light flashlight;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        flashlight = GetComponent<Light>();
        if (flashlight != null)
            flashlight.enabled = false;  // ������� ���������� ��������
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // ��������� ������� �������� �, ���� ����� � �� ����
        if (!equipped && distance <= pickupRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            PickUp();
        }

        // ���/���� ���� ������� F, ���� ������� � ����
        if (equipped && Keyboard.current.fKey.wasPressedThisFrame)
        {
            if (flashlight != null)
                flashlight.enabled = !flashlight.enabled;
        }
    }

    void PickUp()
    {
        equipped = true;

        // ������� ������� �������� �������� ����
        transform.SetParent(playerHand);

        // �������� ������� � ������� �������� � ����
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        // ��������� ������ � ���������
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = true;

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        // �������� ����
        if (flashlight != null)
            flashlight.enabled = true;

        Debug.Log("������� �������� � �������");
    }
}
