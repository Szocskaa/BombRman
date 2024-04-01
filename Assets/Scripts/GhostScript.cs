using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostScript : MonoBehaviour
{
    private Animator Anim;
    private CharacterController Ctrl;
    private Vector3 MoveDirection = Vector3.zero;

    private static readonly int IdleState = Animator.StringToHash("Base Layer.idle");
    private static readonly int MoveState = Animator.StringToHash("Base Layer.move");

    [SerializeField] private SkinnedMeshRenderer[] MeshR;
    [SerializeField] public float Speed = 4;
    void Start()
    {
        Anim = this.GetComponent<Animator>();
        Ctrl = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        GRAVITY();
        MOVE();
    }


    private void GRAVITY()
    {
        if(Ctrl.enabled)
        {
            if(CheckGrounded())
            {
                if(MoveDirection.y < -0.1f)
                {
                    MoveDirection.y = -0.1f;
                }
            }
            MoveDirection.y -= 0.1f;
            Ctrl.Move(MoveDirection * Time.deltaTime);
        }
    }


    private bool CheckGrounded()
    {
        if (Ctrl.isGrounded && Ctrl.enabled)
        {
            return true;
        }
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.1f, Vector3.down);
        float range = 0.2f;
        return Physics.Raycast(ray, range);
    }

        
    private void MOVE ()
    {
        if(Anim.GetCurrentAnimatorStateInfo(0).fullPathHash == MoveState)
        {
            if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                MOVE_Velocity(new Vector3(0, 0, Speed), new Vector3(0, 0, 0));
            }
            else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                MOVE_Velocity(new Vector3(0, 0, -Speed), new Vector3(0, 180, 0));
            }
            else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            {
                MOVE_Velocity(new Vector3(-Speed, 0, 0), new Vector3(0, 270, 0));
            }
            else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A))
            {
                MOVE_Velocity(new Vector3(Speed, 0, 0), new Vector3(0, 90, 0));
            }
        }
        KEY_DOWN();
        KEY_UP();
    }


    private void MOVE_Velocity (Vector3 velocity, Vector3 rot)
    {
        MoveDirection = new Vector3 (velocity.x, MoveDirection.y, velocity.z);
        if(Ctrl.enabled)
        {
            Ctrl.Move(MoveDirection * Time.deltaTime);
        }
        MoveDirection.x = 0;
        MoveDirection.z = 0;
        this.transform.rotation = Quaternion.Euler(rot);
    }


    private void KEY_DOWN ()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Anim.CrossFade(MoveState, 0.1f, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Anim.CrossFade(MoveState, 0.1f, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Anim.CrossFade(MoveState, 0.1f, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Anim.CrossFade(MoveState, 0.1f, 0, 0);
        }
    }


    private void KEY_UP ()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            if(!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                Anim.CrossFade(IdleState, 0.1f, 0, 0);
            }
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                Anim.CrossFade(IdleState, 0.1f, 0, 0);
            }
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            {
                Anim.CrossFade(IdleState, 0.1f, 0, 0);
            }
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A))
            {
                Anim.CrossFade(IdleState, 0.1f, 0, 0);
            }
        }
    }
}