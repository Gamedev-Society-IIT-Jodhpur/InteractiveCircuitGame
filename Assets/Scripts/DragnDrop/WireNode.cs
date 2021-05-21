using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireNode : MonoBehaviour
{
    float originalRadius;
    public bool colliderCheck = false;
    Transform[] childs;
    float distance;
    float height;
    Canvas canvas;
    RectTransform rectTransform;
    GameObject CircuitSim;
    CircuitSim Sim;
    GameObject startingFrom;
    [SerializeField]GameObject simArea;

    // Start is called before the first frame update
    void Start()
    {
        originalRadius = GetComponent<CircleCollider2D>().radius;
        childs = GetComponentInParent<WireComponent>().childs;
        canvas = GetComponentInParent<Canvas>();
        rectTransform = childs[1].GetComponent<RectTransform>();
        CircuitSim = GameObject.FindGameObjectWithTag("CircuitSim");
        Sim = CircuitSim.GetComponent<CircuitSim>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        startingFrom = GetComponentInParent<WireComponent>().startingFrom;
        if (colliderCheck && (collision.gameObject.tag == "in" || collision.gameObject.tag == "out") &&
            collision.transform.parent.gameObject!=startingFrom)
        {
            colliderCheck = false;
            //Connect to Wire
            if (collision.transform.parent.gameObject.tag == "Resistor")
            {
                ResistorComponent resistorComponent = collision.transform.parent.GetComponent<ResistorComponent>();
                if (collision.gameObject.tag == "in")
                {
                    resistorComponent.ConnectToWire(0, transform.parent.GetComponent<WireObject>(), 1);
                }
                else
                {
                    resistorComponent.ConnectToWire(1, transform.parent.GetComponent<WireObject>(), 1);
                }

            }
            else if (collision.transform.parent.gameObject.tag == "Diode")
            {
                DiodeComponent Component = collision.transform.parent.GetComponent<DiodeComponent>();
                if (gameObject.tag == "in")
                {
                    Component.ConnectToWire(0, transform.parent.GetComponent<WireObject>(), 1);
                }
                else
                {
                    Component.ConnectToWire(1, transform.parent.GetComponent<WireObject>(), 1);

                }
            }
            else if (collision.transform.parent.gameObject.tag == "Zener")
            {
                ZenerDiode Component = collision.transform.parent.GetComponent<ZenerDiode>();
                if (gameObject.tag == "in")
                {
                    Component.ConnectToWire(0, transform.parent.GetComponent<WireObject>(), 1);
                }
                else
                {
                    Component.ConnectToWire(1, transform.parent.GetComponent<WireObject>(), 1);

                }

            }
            else if (collision.transform.parent.gameObject.tag == "DCBattery")
            {
                DCBattery Component = collision.transform.parent.GetComponent<DCBattery>();

                if (gameObject.tag == "in")
                {
                    Component.ConnectToWire(0, transform.parent.GetComponent<WireObject>(), 1);
                }
                else
                {
                    Component.ConnectToWire(1, transform.parent.GetComponent<WireObject>(), 1);

                }

            }


            gameObject.GetComponent<CircleCollider2D>().radius = originalRadius;
            //gameObject.transform.position = collision.transform.position;
            
            distance = Mathf.Pow(Mathf.Pow(transform.parent.position.x - collision.transform.position.x, 2) +
                Mathf.Pow(transform.parent.position.y - collision.transform.position.y, 2), 0.5f) / canvas.transform.localScale.x;
            transform.localPosition = new Vector3(0, -distance, transform.localPosition.z);
            height = Vector2.Distance(gameObject.transform.parent.position, transform.position)
                / canvas.transform.localScale.x;
            rectTransform.sizeDelta = new Vector2(rectTransform.rect.width, height);
            childs[1].transform.localPosition = new Vector3(0, -height / 2,
                childs[1].transform.localPosition.z);

            Vector2 angleVector = new Vector2(collision.transform.position.x - transform.parent.position.x,
                collision.transform.position.y - transform.parent.position.y);

            if (angleVector.x < 0)
            {
                float angle = Mathf.Atan(-angleVector.y / angleVector.x) * (180 / Mathf.PI) + 90;
                transform.parent.eulerAngles = new Vector3(0, 0, -angle);
                transform.eulerAngles = childs[2].transform.eulerAngles * 0.5f;
            }
            else
            {
                float angle = Mathf.Atan(angleVector.y / angleVector.x) * (180 / Mathf.PI) + 90;
                transform.parent.eulerAngles = new Vector3(0, 0, angle);
                transform.eulerAngles = childs[2].transform.eulerAngles * 0.5f;

            }
            if (rectTransform.rect.height <= 1)
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
            GetComponentInParent<WireComponent>().isDrawing = false;


        }
    }
}
