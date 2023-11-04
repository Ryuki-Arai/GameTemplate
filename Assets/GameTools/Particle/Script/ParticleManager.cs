using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : SingletonMonoBehaviour<ParticleManager>
{
    [SerializeField] private ParticleSystem particle;

    private Dictionary<ParticleType, ParticleSystem> particleData = new Dictionary<ParticleType,ParticleSystem>();

    public void OnInitialized()
    {
        Entry();
    }

    public void Play(ParticleType type, Vector3 position)
    {
        if (!particleData.ContainsKey(type))
        {
            return;
        }

        var particle = Instantiate(particleData[type], position, Quaternion.identity);
        particle.Play();
        Destroy(particle, particle.particleCount);
    }

    private void Entry()
    {
        particleData.Add(ParticleType.none, particle);
    }
}

public enum ParticleType
{
    none,
}
