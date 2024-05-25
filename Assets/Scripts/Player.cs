using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 rawInput;
    [SerializeField] float moveSpeed = 3f;

    [SerializeField] float paddingBottom;
    [SerializeField] float paddingTop;
    Vector2 minBounds;
    Vector2 maxBounds;

    Shooting shooting;

    private void Awake()
    {
        shooting = GetComponent<Shooting>();
    }

    private void Start()
    {
        InitializeBounds();
    }

    void Update()
    {
        Move();
    }

    void InitializeBounds()
    {
        Camera mainCamera = Camera.main;

        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0,0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1,1));
    }

    private void Move()
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x, maxBounds.x);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        transform.position = newPos;
    }

    private void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if(shooting != null) { shooting.isFiring = value.isPressed; }
    }
}
