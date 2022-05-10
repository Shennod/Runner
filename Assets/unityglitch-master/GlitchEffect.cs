/**
This work is licensed under a Creative Commons Attribution 3.0 Unported License.
http://creativecommons.org/licenses/by/3.0/deed.en_GB

You are free:

to copy, distribute, display, and perform the work
to make derivative works
to make commercial use of the work
*/

using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/GlitchEffect")]
[RequireComponent(typeof(Camera))]
public class GlitchEffect : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Texture2D _displacementMap;
    [SerializeField] private Shader _shader;
    [Header("Glitch Intensity")]

    [Range(0, 1)]
    [SerializeField] private float _intensity;

    [Range(0, 1)]
    [SerializeField] private float _flipIntensity;

    [Range(0, 1)]
    [SerializeField] private float _colorIntensity;

    private float _glitchup;
    private float _glitchdown;
    private float flicker;
    private float _glitchupTime = 0.05f;
    private float _glitchdownTime = 0.05f;
    private float _flickerTime = 0.1f;
    private Material _material;

    private void Start()
    {
        _material = new Material(_shader);
    }

    private void OnEnable()
    {
        _player.ElectroHurt += OnPlayerHurt;
        _player.Died += OnDied;
    }

    private void OnDisable()
    {
        _player.ElectroHurt -= OnPlayerHurt;
        _player.Died -= OnDied;
    }

    // Called by camera to apply image effect
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        _material.SetFloat("_Intensity", _intensity);
        _material.SetFloat("_ColorIntensity", _colorIntensity);
        _material.SetTexture("_DispTex", _displacementMap);

        flicker += Time.deltaTime * _colorIntensity;
        if (flicker > _flickerTime)
        {
            _material.SetFloat("filterRadius", Random.Range(-3f, 3f) * _colorIntensity);
            _material.SetVector("direction", Quaternion.AngleAxis(Random.Range(0, 360) * _colorIntensity, Vector3.forward) * Vector4.one);
            flicker = 0;
            _flickerTime = Random.value;
        }

        if (_colorIntensity == 0)
            _material.SetFloat("filterRadius", 0);

        _glitchup += Time.deltaTime * _flipIntensity;
        if (_glitchup > _glitchupTime)
        {
            if (Random.value < 0.1f * _flipIntensity)
                _material.SetFloat("flip_up", Random.Range(0, 1f) * _flipIntensity);
            else
                _material.SetFloat("flip_up", 0);

            _glitchup = 0;
            _glitchupTime = Random.value / 10f;
        }

        if (_flipIntensity == 0)
            _material.SetFloat("flip_up", 0);

        _glitchdown += Time.deltaTime * _flipIntensity;
        if (_glitchdown > _glitchdownTime)
        {
            if (Random.value < 0.1f * _flipIntensity)
                _material.SetFloat("flip_down", 1 - Random.Range(0, 1f) * _flipIntensity);
            else
                _material.SetFloat("flip_down", 1);

            _glitchdown = 0;
            _glitchdownTime = Random.value / 10f;
        }

        if (_flipIntensity == 0)
            _material.SetFloat("flip_down", 1);

        if (Random.value < 0.05 * _intensity)
        {
            _material.SetFloat("displace", Random.value * _intensity);
            _material.SetFloat("scale", 1 - Random.value * _intensity);
        }
        else
            _material.SetFloat("displace", 0);

        Graphics.Blit(source, destination, _material);
    }

    private void OnPlayerHurt()
    {
        _intensity = 1f;
        _flipIntensity = 1f;
        _colorIntensity = 1f;

        StartCoroutine(StartGlitch());
    }

    private IEnumerator StartGlitch()
    {       
        yield return new WaitForSeconds(0.6f);

        _intensity = 0f;
        _flipIntensity = 0f;
        _colorIntensity = 0;
    }

    private void OnDied()
    {
        StopCoroutine(StartGlitch());

        _intensity = 0f;
        _flipIntensity = 0f;
        _colorIntensity = 0;
    }
}