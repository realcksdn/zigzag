using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;
    private Movement movement;
    private float limitDeathY;

    private void Awake()
    {
        movement = GetComponent<Movement>();

        //movement.MoveTo(Vector3.right); 방향 오른쪽 설정

        limitDeathY = transform.position.y - transform.localScale.y * 0.5f;
    }

    private IEnumerator Start()
    {
        while (true)
        {
            if (gameController.IsGameStart == true)
            {
                movement.MoveTo(Vector3.right);

                yield break;
            }

            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.IsGameOver == true) return; // 5-2

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 direction = movement.MoveDirection == Vector3.forward ? Vector3.right : Vector3.forward;
            movement.MoveTo(direction);
            gameController.IncreaseScore();
        }
        if (transform.position.y < limitDeathY)
        {
            //Debug.Log("GameOver");
            gameController.GameOver();
        }
    }
}