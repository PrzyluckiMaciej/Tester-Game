using UnityEngine;

public class Diode : MonoBehaviour
{
    [SerializeField] private Transform waterContainer;
    [SerializeField] private Renderer diodeRenderer;

    [SerializeField] private float lowThreshold = -1.1f;
    [SerializeField] private float highThreshold = -0.8f;

    [SerializeField] private Color startColor = new Color(0f, 140f / 255f, 0f);
    [SerializeField] private Color targetColor = new Color(0f, 255f / 255f, 0f);

    private Material waterMaterial;

    private float transitionTime = 1f; // czas trwania zmiany koloru w sekundach
    private float transitionProgress = 0f; // postêp przejœcia od 0 do 1
    private bool isWithinThreshold = false;
    private Color currentColor;

    private void Start()
    {
        waterMaterial = waterContainer.GetComponent<Renderer>().material;
        diodeRenderer.material.color = startColor;
        currentColor = startColor;
    }

    private void Update()
    {
        float fill = waterMaterial.GetFloat("_Fill");

        bool newIsWithinThreshold = fill >= lowThreshold && fill <= highThreshold;

        if (newIsWithinThreshold != isWithinThreshold)
        {
            isWithinThreshold = newIsWithinThreshold;
            transitionProgress = 0f; 
        }

        if (transitionProgress < 1f)
        {
            transitionProgress += Time.deltaTime / transitionTime;
            if (isWithinThreshold)
            {
                currentColor = Color.Lerp(startColor, targetColor, transitionProgress);
            }
            else
            {
                currentColor = Color.Lerp(targetColor, startColor, transitionProgress);
            }
            diodeRenderer.material.color = currentColor;
        }
        else
        {
            diodeRenderer.material.color = isWithinThreshold ? targetColor : startColor;
        }
    }
}
