using AxGrid;
using UnityEngine;

public class ParticleSystemBind : MonoBehaviour
{
    public ParticleSystem _particleSystem; 
    private void Start()
    {
        Settings.Model.Set("particleSystem", _particleSystem);
    }
}
