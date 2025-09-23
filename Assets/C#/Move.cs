using UnityEngine;

public class Move : MonoBehaviour
{
    // Публичные переменные для настройки в инспекторе Unity
    public Transform cameraTransform; // Перетащите сюда Transform объекта камеры
    public float moveSpeed = 5f;
    public float jumpForce = 8f;

    // Приватные переменные
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        // Получаем ссылку на компонент Rigidbody
        rb = GetComponent<Rigidbody>();
    }

    // Этот метод вызывается каждый кадр и предназначен для обработки ввода
    void Update()
    {
        // Обработка прыжка
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    // Этот метод вызывается на фиксированном временном интервале для физических расчетов
    void FixedUpdate()
    {
        // Получаем ввод с клавиатуры
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Создаем вектор, основанный на вводе, но в плоскости мира (без учета вращения)
        Vector3 movementInput = new Vector3(horizontalInput, 0f, verticalInput);

        // Получаем вектор направления камеры, игнорируя ее наклон по оси Y
        // Vector3.ProjectOnPlane проецирует вектор на плоскость, нормаль которой Vector3.up
        Vector3 cameraForward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
        Vector3 cameraRight = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized;

        // Вычисляем желаемое направление движения на основе направления камеры
        Vector3 desiredMoveDirection = cameraForward * verticalInput + cameraRight * horizontalInput;

        // Применяем скорость к Rigidbody в новом направлении
        rb.velocity = new Vector3(desiredMoveDirection.x * moveSpeed, rb.velocity.y, desiredMoveDirection.z * moveSpeed);

        // Поворот персонажа в направлении движения (необязательно, но полезно)
        if (desiredMoveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(desiredMoveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.fixedDeltaTime * 10f);
        }
    }

    // Метод для определения, находится ли персонаж на земле
    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.7f)
            {
                isGrounded = true;
                return;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}