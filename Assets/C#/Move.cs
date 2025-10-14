using JetBrains.Annotations;
using System;
using UnityEngine;
public class Move : MonoBehaviour
{

    // Публичные переменные для настройки в инспекторе Unity
    public Transform cameraTransform; // Перетащите сюда Transform объекта камеры
    Transform tr;
    public float moveSpeed = 5f;
    public float runSpeed = 5f;
    public float sprintSpeed = 8f;
    public float jumpForce = 8f;
    public float force = 8f;
    Vector3 desiredMoveDirection;
    Vector3 cameraForward;
    public float intervalForce = 3f;

    // Приватные переменные
    private Rigidbody rb;
    private bool isGrounded;
    bool isSprint;
    private float timeFoce = 0.5f;
    private bool isSlider;
    public float speed = 20;
    public float rotate = 45;
    private float lastClickTime;
    private float doubleClickThreshold = 0.3f;

    private void Awake()
    {
        tr = GetComponent<Transform>();
    }
    void Start()
    {
        // Получаем ссылку на компонент Rigidbody
        rb = GetComponent<Rigidbody>();
    }

    // Этот метод вызывается каждый кадр и предназначен для обработки ввода
    void Update()
    {
        // Обработка прыжка
        Jamping();
        ResetForce();
        TimeForce();
    }
    // Этот метод вызывается на фиксированном временном интервале для физических расчетов
    void FixedUpdate()
    {
        // Получаем ввод с клавиатуры
        // Создаем вектор, основанный на вводе, но в плоскости мира (без учета вращения)
        Vector3 movementInput = InputAxis();

        // Получаем вектор направления камеры, игнорируя ее наклон по оси Y
        // Vector3.ProjectOnPlane проецирует вектор на плоскость, нормаль которой Vector3.up
        Vector3 desiredMoveDirection = GetDirectionCamera(movementInput);

        // Применяем скорость к Rigidbody в новом направлении
        Moving(desiredMoveDirection);

        // Поворот персонажа в направлении движения (необязательно, но полезно)
        Rotating(cameraForward);


    }
    private void TimeForce()
    {

        if (timeFoce <= Time.time)
        {
            timeFoce = Time.time + intervalForce;
            isSlider = true;
            print("time " + isSlider);
        }

    }
    private void ResetForce()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isSlider)
        {
            rb.AddRelativeForce(Vector3.forward * force, ForceMode.Impulse);
            print("force " + isSlider);
            isSlider = false;
        }
    }
 
    private void Sprinting()
    {
        if (isGrounded)
        {
            isSprint = Input.GetKey(KeyCode.LeftShift);
            moveSpeed = isSprint ? sprintSpeed : runSpeed;
        }

    }

    private void Jamping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void Rotating(Vector3 desiredMoveDirection)
    {
        Quaternion toRotation = Quaternion.LookRotation(desiredMoveDirection, Vector3.up);
        Quaternion r = Quaternion.Slerp(rb.rotation, toRotation, Time.fixedDeltaTime * rotate);
        rb.MoveRotation(r);
    }

    private void Moving(Vector3 desiredMoveDirection)
    {
        rb.MovePosition(rb.position + desiredMoveDirection * moveSpeed * Time.deltaTime);
    }

    private Vector3 GetDirectionCamera(Vector3 movementInput)
    {
        cameraForward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
        Vector3 cameraRight = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized;

        // Вычисляем желаемое направление движения на основе направления камеры
        desiredMoveDirection = cameraForward * movementInput.z + cameraRight * movementInput.x;
        return desiredMoveDirection;
    }

    private Vector3 InputAxis()
    {
       float horizontalInput = Input.GetAxis("Horizontal");
       float verticalInput = Input.GetAxis("Vertical");
       return new Vector3(horizontalInput, 0f, verticalInput);
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