using System.Collections;
using UnityEngine;
using System;

public class GridMove : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float gridSize = 1f;
    private enum Orientation
    {
        Horizontal,
        Vertical
    };
    private Orientation gridOrientation = Orientation.Vertical;
    private bool allowDiagonals = false;
    private bool correctDiagonalSpeed = true;
    private Vector2 input;
    private bool isMoving = false;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float t;
    private float factor;

    public EntityAnimationController animatorController;
    public MapEntity entity;


    public void Move(Vector2 input)
    {
        if (!isMoving)
        {
            if (!allowDiagonals)
            {
                if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                {
                    input.y = 0;
                }
                else
                {
                    input.x = 0;
                }
            }

            if (input != Vector2.zero)
            {
                StartCoroutine(move(transform,input));
            }
        }
    }

    public IEnumerator move(Transform transform, Vector2 input)
    {
        isMoving = true;
        startPosition = transform.position;
        t = 0;

        animatorController.InputDir = input;
        animatorController.AnimateMovement();

        if (gridOrientation == Orientation.Horizontal)
        {
            endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize,
                startPosition.y, startPosition.z + System.Math.Sign(input.y) * gridSize);
        }
        else
        {
            endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize,
                startPosition.y + System.Math.Sign(input.y) * gridSize, startPosition.z);
        }

        if (allowDiagonals && correctDiagonalSpeed && input.x != 0 && input.y != 0)
        {
            factor = 0.7071f;
        }
        else
        {
            factor = 1f;
        }

        while (t < 1f)
        {
            t += Time.deltaTime * (moveSpeed / gridSize) * factor;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        isMoving = false;
        animatorController.InputDir = Vector2.zero;
        animatorController.AnimateMovement();

        if(entity is IMovable)
        {
            (entity as IMovable).Move(input);
        }

        entity.turnStatus = TurnStatus.Idle;

        yield return 0;
    }
}