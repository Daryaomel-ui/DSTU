
using UnityEngine;

public class GolemMovement : MonoBehaviour
{
    private GolemStatus status;
    public Transform player;
    public float moveSpeed = 2f;
    public float stopDistance = 1.5f;

    private CharacterController controller;
    private float verticalVelocity;
    private float gravity = -9.81f;

    void Start()
    {
        status = GetComponent<GolemStatus>();
        controller = GetComponent<CharacterController>();

        if (player == null)
        {
            PlayerMovement foundPlayer =
                FindFirstObjectByType<PlayerMovement>();

            if (foundPlayer != null)
            {
                player = foundPlayer.transform;
            }
        }
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        Vector3 direction = player.position - transform.position;
        direction.y = 0;

        Vector3 movement = Vector3.zero;

        if (direction.magnitude > stopDistance &&
    (status == null || status.currentState != GolemState.Frozen))
        {
            movement = direction.normalized * moveSpeed;
        }

        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;
        movement.y = verticalVelocity;

        controller.Move(movement * Time.deltaTime);
    }
}
