using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;
using static UnityEngine.Rendering.GPUSort;

public class Mediator : MonoBehaviour
{
    public static Mediator Instance;

    public AudioClip EngineSound;
    public AudioClip Music;
    public AudioClip StartSound;

    public string PlayerTag;
    public string[] ScenesWithMusic;

    private AudioSource _globalAudio;
    private Dictionary<GameObject, AudioSource> _playerAudioSourceMapper;
    private Dictionary<GameObject, Coroutine> _playerCoroutineMapper;
    private float _sfxVolume = 1,_musicVolume=0.25f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    
    void Awake()
    {
        var instances = FindObjectsByType<Mediator>(FindObjectsSortMode.None);

        if (instances.Length == 1)
        {
            Instance = this;
            Init();
            GameObject.DontDestroyOnLoad(instances[0]);
        }
        else Destroy(this);

    }

    
    public void StartRace() {
        GameObject[] players = GameObject.FindGameObjectsWithTag(PlayerTag);
        foreach (GameObject player in players) {
        CarInputHandler inputs =    player.GetComponent<CarInputHandler>();
            if(inputs!=null)inputs.enabled= false;

        }
        
        StartLight[] lights = FindObjectsByType<StartLight>(FindObjectsSortMode.InstanceID);
        StartCoroutine(StartRaceRoutine(players,lights));
    }

    public IEnumerator StartRaceRoutine(GameObject[] players, StartLight[] lights,float time = 1f)
    {
        yield return new WaitForSeconds(2f);
        _globalAudio.PlayOneShot(StartSound, 2 * _sfxVolume / _musicVolume);
        yield return new WaitForSeconds(1f);

        float _t = 0;
        for(int i =0; i < lights.Length; i++)
        {
            _t = 0;
            LightOn(lights,i);
            while (_t < time)
            {
                yield return null;
                _t += Time.deltaTime;
            }
            

        }

        _t = 0;
        while (_t < time)
        {
            yield return null;
            _t += Time.deltaTime;
        }
        
        ActualRaceStart(players);

    }

    private void ActualRaceStart(GameObject[] players)
    {
        GameObject lightPanel= GameObject.FindWithTag("LightPanel");
        lightPanel?.SetActive(false);
        foreach (GameObject player in players)
        {
            CarInputHandler inputs = player.GetComponent<CarInputHandler>();
            if (inputs != null) inputs.enabled = true;

        }
    }

    private void LightOn(StartLight[] lights,int id)
    {
        foreach (StartLight light in lights)
            if (light.Id == id) light.TurnOn();
    }

    private void Init()
    {

        SceneManager.sceneLoaded += OnSceneLoaded;
        
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);

        


    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        _playerAudioSourceMapper = new Dictionary<GameObject, AudioSource>();
        _playerCoroutineMapper = new Dictionary<GameObject, Coroutine>();



        GameObject[] players = GameObject.FindGameObjectsWithTag(PlayerTag);
        int playerCount = players.Length;



        GameObject go = new GameObject("GlobalSound");
        go.transform.SetParent(this.transform);

        _globalAudio = go.AddComponent<AudioSource>();
        _globalAudio.clip = Music;
        _globalAudio.volume = _musicVolume;
        _globalAudio.loop = true;
        

        for (uint i = 0; i < playerCount; i++)
        {
            go = new GameObject($"Player{i + 1}Sound");
            go.transform.SetParent(players[i].transform);
            AudioSource newSource = go.AddComponent<AudioSource>();
            newSource.volume = _sfxVolume;
            newSource.clip = EngineSound;
            newSource.loop = true;
            _playerAudioSourceMapper.Add(players[i], newSource);

        }


        if (ScenesWithMusic.Contains(arg0.name))
            _globalAudio.Play();
        else
            _globalAudio.Stop();


    }

    public void NotifyPlayerAction(GameObject sender, PlayerAction action)
    {
        if (!_playerAudioSourceMapper.ContainsKey(sender)) return;
        switch (action)
        {
            case PlayerAction.ACCELERATE:
                StartAccelerating(sender, _playerAudioSourceMapper[sender]);
                return;
            case PlayerAction.DECELERATE:
                StopAccelerating(sender, _playerAudioSourceMapper[sender]);
                return;

            case PlayerAction.REVERSE:
                Reverse(sender, _playerAudioSourceMapper[sender]);
                return;

        }

    }

    public void Connect(GameObject sender)
    {
        if (_playerAudioSourceMapper.ContainsKey(sender)) return;
       var go = new GameObject($"PlayerSound");
        go.transform.SetParent(sender.transform);
        AudioSource newSource = sender.AddComponent<AudioSource>();
        newSource.volume = _sfxVolume;
        newSource.clip = EngineSound;
        newSource.loop = true;
        _playerAudioSourceMapper.Add(sender, newSource);
    }

    private void Reverse(GameObject carObject, AudioSource source)
    {

        if (_playerCoroutineMapper.ContainsKey(carObject))
        {
            StopCoroutine(_playerCoroutineMapper[carObject]);
            _playerCoroutineMapper[carObject] = StartCoroutine(ReverseCoroutine(carObject, source));

        }
        else
        {
            _playerCoroutineMapper.Add(carObject, StartCoroutine(ReverseCoroutine(carObject, source)));
        }
    }

    private void StartAccelerating(GameObject carObject, AudioSource source)
    {






        if (_playerCoroutineMapper.ContainsKey(carObject))
        {
            StopCoroutine(_playerCoroutineMapper[carObject]);
            _playerCoroutineMapper[carObject] = StartCoroutine(AcceleratingCoroutine(carObject, source));

        }
        else
        {
            _playerCoroutineMapper.Add(carObject, StartCoroutine(AcceleratingCoroutine(carObject, source)));
        }
    }

    IEnumerator AcceleratingCoroutine(GameObject carObject, AudioSource source, float startPitch = 0.8f, float endPitch = 1f, float startVolume = 0.5f, float endVolume = 1f, float duration = 4f)
    {
        float t = 0f;
        if (!source.isPlaying)
            source.Play();
        while (t < duration)
        {
            if (carObject == null || source == null) yield break;

            source.volume = _sfxVolume * Mathf.Lerp(startVolume, endVolume, t / duration);
            source.pitch = Mathf.Lerp(startPitch, endPitch, t / duration);
            yield return null;
            t += Time.deltaTime;
        }

    }

    IEnumerator ReverseCoroutine(GameObject carObject, AudioSource source, float startPitch = 1, float endPitch = 0.6f, float startVolume = 1f, float endVolume = 1f, float duration = 1f)
    {
        float t = 0f;
        if (!source.isPlaying)
            source.Play();
        while (t < duration)
        {
            if (carObject == null || source == null) yield break;

            source.volume = _sfxVolume * Mathf.Lerp(startVolume, endVolume, t / duration);
            source.pitch = Mathf.Lerp(startPitch, endPitch, t / duration);
            yield return null;
            t += Time.deltaTime;
        }

    }

    public void SetVolume(int volume) { this._sfxVolume = Mathf.Clamp(volume, 0, 1); }

    IEnumerator StopAcceleratingCoroutine(GameObject carObject, AudioSource source, float startPitch = 1f, float endPitch = 0.8f, float startVolume = 0.8f, float endVolume = 0f, float duration = 2f)
    {
        float t = 0f;
        if (!source.isPlaying)
            source.Play();
        while (t < duration)
        {
            if (carObject == null || source == null) yield break;

            source.volume = _sfxVolume * Mathf.Lerp(startVolume, endVolume, t / duration);
            source.pitch = Mathf.Lerp(startPitch, endPitch, t / duration);
            yield return null;
            t += Time.deltaTime;
        }
        source.Pause();

    }

    private void StopAccelerating(GameObject carObject, AudioSource source)
    {




        if (_playerCoroutineMapper.ContainsKey(carObject))
        {
            StopCoroutine(_playerCoroutineMapper[carObject]);
            _playerCoroutineMapper[carObject] = StartCoroutine(StopAcceleratingCoroutine(carObject, source));

        }
        else
        {
            _playerCoroutineMapper.Add(carObject, StartCoroutine(StopAcceleratingCoroutine(carObject, source)));
        }

    }



}