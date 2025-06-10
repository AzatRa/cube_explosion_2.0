using System.Collections;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private EventHandler _eventHandler;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            GameObject hitObject = hitInfo.collider.gameObject;
            Cube cube = hitObject.GetComponent<Cube>();

            if (cube != null)
            {
                _eventHandler.Hit(cube);
            }
        }
    }
}
