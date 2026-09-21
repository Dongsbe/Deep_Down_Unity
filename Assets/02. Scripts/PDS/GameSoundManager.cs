using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameSoundManager : MonoBehaviour {
    public DoorState ShowDoorSound;
    public HorrorSoundTrigger ShowHorrorSound;
    public AreaSound ShowAreaSound;
    public ObjectSound ShowObjectSound;

    public AudioSource BGMSound;

    public AudioSource CharacterSound;

    public List<AudioClip> DoorSound;
    public List<AudioClip> AreaSound;
    public List<AudioClip> HorrorTriggerSound;
    public List<AudioClip> ObjectSound;

    AudioSource gameSoundSource;
    public static GameSoundManager instance;

    //게임 매니저 싱글톤
    public static GameSoundManager GetInstance()
    {
        if (!instance)
        {
            instance = (GameSoundManager)GameObject.FindObjectOfType(typeof(GameSoundManager));
            if (!instance)
            {
                Debug.LogError("There needs to be one active GameControl script on a GameObject in your scene.");
            }
        }
        return instance;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SoundFadeOut()
    {
        gameSoundSource.volume -= gameSoundSource.volume * Time.deltaTime;

        if(gameSoundSource.volume <= 0.01f)
        {
            gameSoundSource.Stop();
        }
    }

    public void GetCharacterSound()
    {
        gameSoundSource = CharacterSound;
    }

    public void GetAudioSource(AudioSource audioSource)
    {
        gameSoundSource = audioSource;
    }

    public void StopAudio()
    {
        if(gameSoundSource != null)
        {
            gameSoundSource.Stop();
        }
    }

    public void OtherSoundOn()
    {
        BGMSound.volume = 0.1f;
    }

    public void PlayObjectSound(ObjectSound objectSound)
    {
        for (int i = 0; i < ObjectSound.Count; i++)
        {
            if (i == (int)objectSound)
            {
                gameSoundSource.PlayOneShot(ObjectSound[i]);
            }
        }
    }

    public void PlayAreaSound(AreaSound areaSound)
    {
        OtherSoundOn();
        for (int i = 0; i < AreaSound.Count; i++)
        {
            if (i == (int)areaSound)
            {
                gameSoundSource.PlayOneShot(AreaSound[i]);
            }
        }
    }

    public void PlayTriggerSound(HorrorSoundTrigger horrorTirgger)
    {
        OtherSoundOn();
        for (int i = 0; i < HorrorTriggerSound.Count; i++)
        {
            if(i == (int)horrorTirgger)
            {
                gameSoundSource.PlayOneShot(HorrorTriggerSound[i]);
            }
        }
    }

    public void PlayDoorSound(DoorState doorState)
    {
        for (int i = 0; i < DoorSound.Count; i++)
        {
            if (i == (int)doorState)
            {
                gameSoundSource.PlayOneShot(DoorSound[i]);
            }
        }
    }
}
