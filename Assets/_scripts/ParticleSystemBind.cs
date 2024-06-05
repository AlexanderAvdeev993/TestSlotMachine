using AxGrid;
using UnityEngine;

public class ParticleSystemBind : MonoBehaviour
{
    public ParticleSystem particleSystem; 
    private void Start()
    {
        Settings.Model.Set("particleSystem", particleSystem);
    }
}
