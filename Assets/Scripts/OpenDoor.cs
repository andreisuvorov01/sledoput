using UnityEngine;

public class OpenDoor : MonoBehaviour
{
	private Vector3 vectorStart;

	private Vector3 vectorEnd;

	public GameObject slime;

	private void Start()
	{
		var collider = transform.GetComponent<Collider>();
		vectorEnd = new Vector3(transform.position.x, transform.position.y - 13, transform.position.z);
	}

	private void Update()
	{
		if (!slime)
		{
            transform.GetComponent<Collider>().enabled = false;
            transform.position = Vector3.Lerp(transform.position, vectorEnd, Time.deltaTime * 3);
			Invoke("Destroy" ,5);
		}
	}

    private void Destroy() => Destroy(gameObject);
    
        
    
}
