using UnityEngine;

public class MB_Weapon_Handgun : MB_Weapon
{
    public override void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isReadyToShoot && !isReloading)
        {
            isReadyToShoot = false;
            particleShoot.Play();
            Invoke("StopParticleShoot", 0.2f);
            audioShoot.Play();

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

            CurrentAmmo--;
            if (CurrentAmmo <= 0) Reload();
            UpdateAmmoUI();
            Invoke("ResetShot", TimeBetweenShots);
        }
    }
}
