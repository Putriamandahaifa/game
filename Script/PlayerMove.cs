using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyboardMovement : MonoBehaviour
{
    public float speed = 5f;
    public float gravity = -9.81f;
    private CharacterController controller;
    private Vector3 velocity;
    public float rotationSpeed = 10f;

    private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Ambil input WASD
        float x = Input.GetAxis("Horizontal"); // A/D atau ←/→
        float z = Input.GetAxis("Vertical");   // W/S atau ↑/↓

        // Gerakan berdasarkan arah karakter
        Vector3 move = transform.right * x + transform.forward * z;

        // Jalankan karakter
        controller.Move(move * speed * Time.deltaTime);

        // Atur animasi jalan/diam
        bool isMoving = move.magnitude > 0.1f;
        if (animator != null)
            animator.SetBool("isMoving", isMoving);

        // ROTASI ke arah gerakan
        if (isMoving && !(z < 0 && Mathf.Abs(x) < 0.1f))
        {
            Vector3 lookDir = new Vector3(move.x, 0, move.z);
            Quaternion targetRotation = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Gravity
        if (!controller.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else if (velocity.y < 0)
        {
            velocity.y = -2f;
        }

        controller.Move(velocity * Time.deltaTime);
    }
}

