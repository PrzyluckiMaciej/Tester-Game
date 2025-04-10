using UnityEngine;

public class PlatformControllerLevel7 : MonoBehaviour
{
    [SerializeField] private Transform waterContainer; 
    [SerializeField] private Transform platform;       
    private Material waterMaterial;                   
    private float minFill = -1.3f;                   
    private float maxFill = 1.3f;                     
    private float minHeight = 0f;                     
    private float maxHeight = 5.2f;                   

    private void Start()
    {
        waterMaterial = waterContainer.GetComponent<Renderer>().material;
    }

    private void Update()
    {
        float fill = waterMaterial.GetFloat("_Fill");

        float newHeight = Mathf.Lerp(minHeight, maxHeight, (fill - minFill) / (maxFill - minFill));

        Vector3 platformPosition = platform.position;
        platformPosition.y = newHeight;
        platform.position = platformPosition;
    }
}