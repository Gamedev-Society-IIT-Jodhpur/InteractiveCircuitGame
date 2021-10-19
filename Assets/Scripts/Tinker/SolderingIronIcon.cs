using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolderingIronIcon : MonoBehaviour
{
    [SerializeField] GameObject solderIron;
    GameObject newSolderIron;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Solder(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            //print(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }


    public void Solder(Vector2 position)
    {
        newSolderIron = Instantiate<GameObject>(solderIron);
        newSolderIron.transform.position = Camera.main.ScreenToWorldPoint(gameObject.transform.position);
        newSolderIron.GetComponent<SolderingIron>().Solder(position);
    }

    public void DestroySolderingIron()
    {
        newSolderIron.GetComponent<SolderingIron>().DestroySolder();
    }

    public void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }








}
