using UnityEngine;

public class BooTrigger : MonoBehaviour, IResetable
{
    [SerializeField] private GameObject Boo;

    private void Awake()
    {
        Init();
    }
    public void Init()
    {
        if (Boo != null)
            Boo.SetActive(false);
    }

    public void ActivateShy()
    {
        if (Boo != null)
            Boo.SetActive(true);
    }

    public void DeactivateShy()
    {
        if (Boo != null)
            Boo.SetActive(false);
    }

    
}
