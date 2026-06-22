using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{
    float forwardSpeed = 8f;
    float sideSpeed = 6f;
    Rigidbody rb;

    Vector2 moveInput;

    float currentSideInput;
    float sideVelocity;
    float originalForwardSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalForwardSpeed = forwardSpeed;
    }

    public void ActivateSpeedBoost(float boostAmount, float duration)
    {
        StartCoroutine(SpeedBoostRoutine(boostAmount, duration));
    }

    private IEnumerator SpeedBoostRoutine(float boostAmount, float duration)
    {
        forwardSpeed = boostAmount;
        yield return new WaitForSeconds(duration);
        forwardSpeed = originalForwardSpeed;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        currentSideInput = Mathf.SmoothDamp(currentSideInput, moveInput.x, ref sideVelocity, 0.1f);
        Vector3 movement = new Vector3(currentSideInput * sideSpeed, rb.linearVelocity.y, forwardSpeed);
        rb.linearVelocity = movement;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
