using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    public Color Color { get; private set; }

    private void Awake()
    {
        Color = GetComponent<Image>().color;
    }
}