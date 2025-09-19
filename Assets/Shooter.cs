using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    public GameObject ballPrefab;   // префаб шарика
    public Transform shootPoint;    // точка выстрела
    public float shootForce = 500f;

    private Mouse mouse;
    public bool canShoot = false;   // разрешена ли стрельба

    void Start()
    {
        mouse = Mouse.current;
    }

    void Update()
    {
        if (canShoot && mouse.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject ball = Instantiate(ballPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
            rb.AddForce(shootPoint.forward * shootForce);
        Destroy(ball, 5f);  // удаляем шарик через 5 секунд
    }
}
