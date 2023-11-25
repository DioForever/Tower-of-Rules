using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    public Bow bun;
    private Weapon currentWeapon;
    public List<Weapon> availableWeapons = new List<Weapon>();

    //speed
    public delegate void MoveSpeedChanged();
    public event MoveSpeedChanged OnMoveSpeedChanged;

    //Hp
    public delegate void HpChanged();
    public event HpChanged OnHpChanged;

    //mana
    public delegate void ManaChanged();
    public event ManaChanged OnManaChanged;

    [Header("Hp Settings")]
    [SerializeField] float hp = 100f;
    [Header("Mana Settings")]
    [SerializeField] float mana = 100f;
    [Header("Dash Settings")]
    [SerializeField] float dashSpeed = 5f;
    [SerializeField] float dashDuration = 0.25f;
    [SerializeField] float dashCooldown = 3f;
    bool isDashing;
    bool canDash = true;

    void Start()
    {
        GameObject gunObject = Instantiate(Resources.Load("GunPrefab") as GameObject, transform.position, Quaternion.identity);
        currentWeapon = gunObject.GetComponent<Weapon>();
        currentWeapon.isPickedUp = true;
        availableWeapons.Add(currentWeapon);

        canDash = true;
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        ProcessInputs();

        // Weapon pickup
        if (Input.GetKeyDown(KeyCode.E))
        {
            PickUpWeapon();
        }

        // Weapon switching
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(1);
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    void PickUpWeapon()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);

        foreach (Collider2D collider in colliders)
        {
            Weapon pickedUpWeapon = collider.GetComponent<Weapon>();

            if (pickedUpWeapon != null && !pickedUpWeapon.isPickedUp)
            {
                if (availableWeapons.Count < 2)
                {
                    pickedUpWeapon.isPickedUp = true;
                    availableWeapons.Add(pickedUpWeapon);
                    collider.gameObject.SetActive(false);
                }
                else
                {
                }
            }
        }
    }

    void SwitchWeapon(int index)
    {
        if (index >= 0 && index < availableWeapons.Count)
        {
            currentWeapon = availableWeapons[index];

            currentWeapon.gameObject.SetActive(true);
        }
    }
}