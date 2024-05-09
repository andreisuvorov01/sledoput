using UnityEngine;

public class Weapon : MonoBehaviour
{
	public GameObject bullet;

	public Camera mainCamera;

	public Transform spawnBullet;

	public float shootForce;

	public float spread;

	public int Damage = 10;

	private void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			Shoot();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		Enemy component = other.GetComponent<Enemy>();
		if (component != null)
		{
			component.TakeDamage(Damage);
		}
		Object.Destroy(base.gameObject);
	}

	private void Shoot()
	{
		Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
		if (Input.GetMouseButtonDown(0) && (bool)bullet)
		{
			RaycastHit hitInfo;
			Vector3 vector = ((!Physics.Raycast(ray, out hitInfo)) ? ray.GetPoint(75f) : hitInfo.point);
			Vector3 vector2 = vector - spawnBullet.position;
			float x = Random.Range(0f - spread, spread);
			float y = Random.Range(0f - spread, spread);
			Vector3 vector3 = vector2 + new Vector3(x, y, 0f);
			GameObject obj = Object.Instantiate(bullet, spawnBullet.position, Quaternion.identity);
			obj.transform.forward = vector3.normalized;
			obj.GetComponent<Rigidbody>().AddForce(base.transform.forward * shootForce, ForceMode.Impulse);
		}
	}
}
