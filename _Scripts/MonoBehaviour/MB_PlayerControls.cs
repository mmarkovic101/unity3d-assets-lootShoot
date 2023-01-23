using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MB_PlayerControls : MonoBehaviour
{
    [field: SerializeField] private SO_Player _player;
    [field: SerializeField] private float _mouseSensitivity;
    [field: SerializeField] private float _movementSpeed;
    [field: SerializeField] private float _sprintMultiplier;
    [field: SerializeField] private float _jumpHeight;
    [field: SerializeField] private float _gravity;
    
    private GameObject _inventory;
    private CharacterController _characterController;
    private float _rotationX = 0f;
    private float _rotationY = 0f;
    private Vector3 _velocity = Vector3.zero;
    private bool _isGrounded;
    private List<GameObject> _weapons;

    void Start()
    {
        _weapons = GameObject.FindGameObjectsWithTag("Weapon").OrderBy(x => x.transform.name).ToList();
        _inventory = GameObject.FindGameObjectWithTag("Inventory");
        _characterController = GetComponent<CharacterController>();
        SwitchWeapon();
    }

    void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Look();
            Move();
            Sprint();
            Jump();
        }
        Fall();
        OpenInventory();
        Exit();
    }

    private void Exit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    private void Look()
    {
        _rotationX -= Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;
        _rotationY += Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;

        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);

        Camera.main.transform.localEulerAngles = Vector3.right * _rotationX;
        transform.localEulerAngles = Vector3.up * _rotationY;
    }

    private void Move()
    {
        Vector3 direction = new Vector3();

        if (Input.GetKey(KeyCode.W)) direction += transform.forward;
        if (Input.GetKey(KeyCode.S)) direction -= transform.forward;
        if (Input.GetKey(KeyCode.D)) direction += transform.right;
        if (Input.GetKey(KeyCode.A)) direction -= transform.right;

        _characterController.Move(direction * _movementSpeed * Time.deltaTime);
    }

    private void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _movementSpeed = _movementSpeed * _sprintMultiplier;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _movementSpeed = _movementSpeed / _sprintMultiplier;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(-2f * _jumpHeight * _gravity) * Time.deltaTime;
            _isGrounded = false;
        }
    }

    private void Fall()
    {
        _velocity.y += 0.5f * _gravity * Time.deltaTime * Time.deltaTime;
        _characterController.Move(_velocity);
        if (_characterController.isGrounded)
        {
            _velocity.y = 0f;
            _isGrounded = true;
        }
    }

    private void OpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _inventory.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void SwitchWeapon()
    {
        DisableWeapons();
        if (_player.EquippedWeapon != null)
        {
            switch ((_player.EquippedWeapon.ItemData as SO_ItemData_Weapon).WeaponType)
            {
                case WeaponType.Handgun:
                    _weapons[0].SetActive(true);
                    _weapons[0].GetComponent<MB_Weapon_Handgun>().ConfigureWeapon();
                    break;
                case WeaponType.Shotgun:
                    _weapons[1].SetActive(true);
                    _weapons[1].GetComponent<MB_Weapon_Shotgun>().ConfigureWeapon();
                    break;
                case WeaponType.AssaultRifle:
                    _weapons[2].SetActive(true);
                    _weapons[2].GetComponent<MB_Weapon_AssaultRifle>().ConfigureWeapon();
                    break;
            }
        }
    }

    private void DisableWeapons()
    {
        _weapons.ForEach(x => x.SetActive(false));
    }
}
