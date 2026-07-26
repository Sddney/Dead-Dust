using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float lifetime = 1f;

    private void Awake()
    {
        if (text == null)
            text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void SetText(string value, Color color)
    {
        text.text = value;
        text.color = color;
        Debug.Log(value);
    }
    private void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }
}