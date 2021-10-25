using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolderingIronIcon : MonoBehaviour
{
    [SerializeField] GameObject solderIron;
    GameObject newSolderIron;
    public static Queue<Vector2> noOfSolders;

    private void Start()
    {
        noOfSolders = new Queue<Vector2>() { };
    }

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
        if (!StaticData.isSoldering)
        {
            //noOfSolders.Enqueue(position);
            StaticData.isSoldering = true;
            newSolderIron = Instantiate<GameObject>(solderIron);
            newSolderIron.transform.position = Camera.main.ScreenToWorldPoint(gameObject.transform.position);
            newSolderIron.GetComponent<SolderingIron>().Solder(position);
        }
        else
        {
            noOfSolders.Enqueue(position);
        }
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
