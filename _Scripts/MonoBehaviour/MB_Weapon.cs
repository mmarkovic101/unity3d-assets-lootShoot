using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MB_Weapon : MonoBehaviour
{
    [field: SerializeField] protected SO_Player player;
    [field: SerializeField] protected ParticleSystem particleShoot;
    [field: SerializeField] protected ParticleSystem particleHit;
    [field: SerializeField] protected ParticleSystem particleHitSmoke;
    [field: SerializeField] protected AudioSource audioShoot;
    [field: SerializeField] protected AudioSource audioHit;
    [field: SerializeField] protected AudioSource audioReload;
    [field: SerializeField] protected GameObject floatingDamage;

    protected float MinimumDamage;
    protected float MaximumDamage;
    protected float CriticalHitMultiplier;
    protected float CriticalHitChance;
    protected float TimeBetweenShots;
    protected float ReloadTime;
    protected float Spread;
    protected float Range;
    protected int MaximumAmmo;
    protected int CurrentAmmo;
    protected bool isReadyToShoot = true;
    protected bool isReloading = false;

    private Text _uiAmmo;
    private GameObject _uiReloading;
    private GameObject _uiEnemy;

    void Awake()
    {
        _uiAmmo = GameObject.FindGameObjectWithTag("UIAmmo").GetComponent<Text>();
        _uiReloading = GameObject.FindGameObjectWithTag("UIReloading");
        _uiEnemy = GameObject.FindGameObjectWithTag("UIEnemy");
    }

    void Start()
    {
        _uiReloading.SetActive(false);
        _uiEnemy.SetActive(false);
    }

    void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Shoot();
            if (Input.GetKeyDown(KeyCode.R) && !isReloading) Reload();
            ShowEnemyUI();
        }
    }

    public virtual void ConfigureWeapon()
    {
        MinimumDamage = player.TotalStats[0].Value;
        MaximumDamage = player.TotalStats[1].Value;
        CriticalHitMultiplier = player.TotalStats[2].Value;
        CriticalHitChance = player.TotalStats[3].Value;
        TimeBetweenShots = 1f / player.TotalStats[4].Value;
        ReloadTime = player.TotalStats[5].Value;
        Spread = Mathf.Clamp(100 - player.TotalStats[6].Value, -100f, 100f);
        Range = player.TotalStats[7].Value;
        MaximumAmmo = (int)Mathf.Round(player.TotalStats[8].Value);
        CurrentAmmo = MaximumAmmo;
        UpdateAmmoUI();
    }

    public virtual void Shoot() { }

    protected void StopParticleShoot()
    {
        particleShoot.Stop();
    }

    protected void UpdateAmmoUI()
    {
        _uiAmmo.text = CurrentAmmo + "/" + MaximumAmmo;
    }

    protected void Reload()
    {
        isReloading = true;
        Invoke("EndReload", ReloadTime);
        audioReload.Play();
        _uiReloading.SetActive(true);
    }
    protected void CalculateDamage(RaycastHit hit, Vector3 hitScreenPosition)
    {
        float damage = MinimumDamage + (1 - (Vector3.Distance(transform.position, hit.transform.position)) / Range * (MaximumDamage - MinimumDamage));
        if (UnityEngine.Random.Range(0, 101) <= CriticalHitChance)
        {
            damage *= CriticalHitMultiplier;
            ShowFloatingDamage(hitScreenPosition, damage, true);
        }
        else
        {
            ShowFloatingDamage(hitScreenPosition, damage, false);
        }
    }

    private void EndReload()
    {
        CurrentAmmo = MaximumAmmo;
        isReloading = false;
        audioReload.Stop();
        _uiReloading.SetActive(false);
        UpdateAmmoUI();
    }

    private void ResetShot()
    {
        isReadyToShoot = true;
    }

    private void ShowFloatingDamage(Vector3 hitScreenPosition, float damage, bool isCritical)
    {
        GameObject fdUI = Instantiate(floatingDamage, new Vector3(0f, 0f, 0f), Quaternion.identity);
        fdUI.transform.GetChild(0).position = hitScreenPosition;

        if (isCritical)
        {
            fdUI.GetComponentInChildren<Text>().text = Mathf.Round(damage).ToString();
            fdUI.GetComponentInChildren<Text>().color = Color.red;
            fdUI.GetComponentInChildren<Text>().fontStyle = FontStyle.Bold;
        }
        else
        {
            fdUI.GetComponentInChildren<Text>().text = Mathf.Round(damage).ToString();
        }
    }

    private void ShowEnemyUI()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Range))
        {
            MB_Enemy enemy;
            if (hit.transform.TryGetComponent<MB_Enemy>(out enemy))
            {
                _uiEnemy.SetActive(true);
            }
            else
            {
                _uiEnemy.SetActive(false);
            }
        }
        else
        {
            _uiEnemy.SetActive(false);
        }
    }
}
