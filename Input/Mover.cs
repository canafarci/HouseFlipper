using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public bool IsActive = true;
    [SerializeField] protected float _speed, _rotateSpeed;
    InputReader _reader;
    Rigidbody _rigidbody;


    private void Awake()
    {
        _reader = FindObjectOfType<InputReader>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!IsActive) return;
        Move(_reader.ReadInput());
    }

    void Move(Vector2 input)
    {
        if (input == Vector2.zero) return;

        Vector3 moveVector = new Vector3(input.x, 0, input.y);
        //Vector3 direction =  transform.position + moveVector;
        

        transform.rotation = Quaternion.LookRotation(moveVector);
        _rigidbody.MovePosition(transform.position + (moveVector.normalized * Time.deltaTime * _speed));
        //transform.position += moveVector * Time.deltaTime * _speed;
    }

}
