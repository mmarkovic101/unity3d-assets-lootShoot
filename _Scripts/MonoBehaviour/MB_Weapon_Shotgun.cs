using UnityEngine;

public class MB_Weapon_Shotgun : MB_Weapon
{
    private int _pellets = 10;

    void Start()
    {
        MinimumDamage = Mathf.Round(MinimumDamage/_pellets);
        MaximumDamage = Mathf.Round(MaximumDamage/_pellets);
    }

    public override void ConfigureWeapon()
    {
        MinimumDamage = player.TotalStats[0].Value / _pellets;
        MaximumDamage = player.TotalStats[1].Value / _pellets;
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

    public override void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isReadyToShoot && !isReloading)
        {
            isReadyToShoot = false;
            particleShoot.Play();
            Invoke("StopParticleShoot", 0.2f);
            audioShoot.Play();

            for (int i = 1; i <= _pellets; i++)
            {
                FireSinglePellet();
            }

            CurrentAmmo--;
            if(CurrentAmmo <= 0) Reload();
            UpdateAmmoUI();
            Invoke("ResetShot", TimeBetweenShots);
        }
    }

    private void FireSinglePellet()
    {
        Vector3 hitScreenPosition = Input.mousePosition;
        hitScreenPosition.x += Random.Range(-Spread, Spread);
        hitScreenPosition.y += Random.Range(-Spread, Spread);
        Ray ray = Camera.main.ScreenPointToRay(hitScreenPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Range))
        {
            Instantiate(particleHit, hit.point, Quaternion.identity);
            Instantiate(particleHitSmoke, hit.point, Quaternion.identity);

            MB_Enemy enemy;
            if (hit.transform.TryGetComponent<MB_Enemy>(out enemy))
            {
                audioHit.transform.position = hit.point;
                audioHit.Play();
                CalculateDamage(hit, hitScreenPosition);
            }
        }
    }


}
