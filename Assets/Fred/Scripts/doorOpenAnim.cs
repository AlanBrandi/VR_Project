using UnityEngine;

public class doorOpenAnim : MonoBehaviour
{
    public Transform portaTransform;
    public Vector3 rotacaoFinal;
    public float velocidadeRotacao = 5f;

    private Quaternion rotacaoInicial;
    private bool portaAberta = false;
    bool chaveDentro;

    private void Start()
    {
        rotacaoInicial = portaTransform.rotation;
    }

    private void Update()
    {
        if (chaveDentro)
        {
            if (portaAberta)
                return;
            if (Quaternion.Angle(portaTransform.rotation, Quaternion.Euler(rotacaoFinal)) < 0.1f)
            {
                portaAberta = true;
                Debug.Log("Door open.");
            }
            else
            {
                portaTransform.rotation = Quaternion.RotateTowards(portaTransform.rotation, Quaternion.Euler(rotacaoFinal), velocidadeRotacao * Time.deltaTime);
            }
        }
    }

    public void ResetarPorta()
    {
        portaAberta = false;
        portaTransform.rotation = rotacaoInicial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            other.gameObject.SetActive(false);
            InventoryManager.Instance.PlaySound();
            chaveDentro = true;
        }
    }
}