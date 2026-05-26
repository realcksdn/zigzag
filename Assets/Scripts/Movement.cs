using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float increaseAmount;
    [SerializeField]
    private float increaseCycleTime;

    private Vector3 moveDirection;


    public Vector3 MoveDirection => moveDirection; //외부에서 이동방향 확인

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(increaseCycleTime);
            moveSpeed += increaseAmount;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector3 direction) // 이동방향 설정
    {
        moveDirection = direction;
    }
}