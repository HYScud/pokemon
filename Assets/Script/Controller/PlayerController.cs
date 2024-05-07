using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private bool isMoving=false;
    private Vector2 m_Input;
    public float moveSpeed=1;
    private int ContDown;
    private PlayerAnimator playerAnimator;

    private void Awake()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            this.ToMove();
        }
        else
        {
            this.ContDown++;
            if (this.ContDown > 2000)
            {
                Debug.Log("正在移动中");
                this.ContDown = 0;
            }
            
        }
    }

    private void ToMove()
    {
        if (!isMoving)
        {
            m_Input.x = Input.GetAxisRaw("Horizontal");
            m_Input.y = Input.GetAxisRaw("Vertical");
            if (m_Input != Vector2.zero)
            {
                Debug.Log("执行ToMove" + m_Input.x + " " + m_Input.y);
                if (m_Input.x != 0)
                {
                    m_Input.y = 0;
                }
                playerAnimator.ChangeMoveX(m_Input.x);
                playerAnimator.ChangeMoveY(m_Input.y);
                var targetPos = transform.position;
                targetPos.x += m_Input.x;
                targetPos.y += m_Input.y;
                isMoving = true;
                Move(targetPos).Forget();
            }
            else
            {
                if (this.ContDown > 2000)
                {
                    Debug.Log("Player idle");
                }
            }
        }
        playerAnimator.ChangeisMoving(isMoving);
    }

    async UniTaskVoid Move(Vector3 targetPos)
    {
        Debug.Log("moveTargetPos："+targetPos);
        while ((targetPos - transform.position).sqrMagnitude > 0.00001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            await UniTask.Yield();
            await UniTask.NextFrame();
        }
        Debug.Log("Player MoveOver curPos"+ transform.position+"**"+targetPos);
        transform.position = targetPos;
        CommonUtils.CorrectPosition(transform.position);
        isMoving = false;
    }
}
