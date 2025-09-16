using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Публичные переменные для настройки в инспекторе Unity
    public float moveSpeed = 5f;
    public float jumpForce = 8f;

    // Приватные переменные
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        // Получаем ссылку на компонент Rigidbody, прикрепленный к этому же объекту
        rb = GetComponent<Rigidbody>();
    }

    // Этот метод вызывается каждый кадр и предназначен для обработки ввода
    void Update()
    {
        // Обработка прыжка
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // После прыжка персонаж не на земле
        }
    }

    // Этот метод вызывается на фиксированном временном интервале и используется для физических расчетов
    void FixedUpdate()
    {
        // Получаем ввод с клавиатуры по осям "Horizontal" и "Vertical"
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Создаем вектор движения, игнорируя ось Y, так как гравитация будет работать отдельно
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Устанавливаем скорость Rigidbody
        // Сохраняем текущую вертикальную скорость (rb.velocity.y) для корректной работы прыжка и гравитации
        rb.velocity = new Vector3(movement.x * moveSpeed, rb.velocity.y, movement.z * moveSpeed);
    }

    // Этот метод вызывается, когда коллайдер объекта входит в соприкосновение с другим коллайдером
    void OnCollisionEnter(Collision collision)
    {
        // Проверяем, находится ли персонаж на земле, анализируя нормали контактов
        foreach (ContactPoint contact in collision.contacts)
        {
            // Если нормаль контакта направлена вверх (очень близко к Vector3.up),
            // значит, мы стоим на поверхности
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.7f)
            {
                isGrounded = true;
                return;
            }
        }
    }

    // Этот метод вызывается, когда персонаж перестает касаться земли
    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
