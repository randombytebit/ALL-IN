using TMPro;
using UnityEngine;

public class PokerCard : Interactable
{
    [SerializeField] private TextMeshPro textMeshPro;
    [SerializeField] private string pokerCardMessage;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void Interact()
    {
        textMeshPro.text = pokerCardMessage;
        Vector3 originalPosition = new Vector3(-0.34f, 2.53f, -0.25f);
        textMeshPro.transform.position = originalPosition;
        originalPosition.x += transform.position.z - 0.43f;
        textMeshPro.transform.position = originalPosition;
        Debug.Log(textMeshPro.transform.position);
    }
}
