using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

/// <summary>
/// 씬에 맞는 BGM과 게임 효과음을 재생하고 BGM,SFX 볼륨을 관리
/// 씬이 바뀌어도 유지되며, 저장한 볼륨을 다음 실행 때 불러와 유지
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    public enum SfxType
    { 
        ButtonClick,
        LevelUp,
        WarriorAttack,
        ArcherAttack,
        AssasinAttack,
        MagicianAttack,
        EnemyDeath,
        EnemyHit,
        MinerHit
    }

    [Header("오디오 믹서")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioMixerGroup bgmGroup;
    [SerializeField] private AudioMixerGroup sfxGroup;

    [Header("씬 이름")]
    [SerializeField] private string mainSceneName = "Title";
    [SerializeField] private string gameSceneName = "Game";

    [Header("BGM")]
    [SerializeField] private AudioClip mainBgm;
    [SerializeField] private AudioClip gameBgm;

    [Header("SFX")]
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip levelup;
    [SerializeField] private AudioClip warriorAttack;
    [SerializeField] private AudioClip archerAttack;
    [SerializeField] private AudioClip mageAttack;
    [SerializeField] private AudioClip assasinAttack;
    [SerializeField] private AudioClip enemyDeath;
    [SerializeField] private AudioClip enemyHit;
    [SerializeField] private AudioClip minerHit;

    private const string BgmParameter = "BGMVolume";
    private const string SfxParameter = "SFXVolume";
    private const string BgmSaveKey = "Audio.BGMVolume";
    private const string SfxSaveKey = "Audio.SFXVolume";

    private AudioSource bgmSource;
    private AudioSource sfxSource;
    private bool isInitialized;

    public float BgmVolume { get; private set; }
    public float SfxVolume { get; private set; }

    /// <summary>
    /// 생성 시 중복 매니저를 제거하고 재생에 사용할 AudioSource를 준비
    /// 저장된 볼륨은 먼저 읽은 뒤 믹서에는 Start에서 적용
    /// </summary>
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.playOnAwake = false;
        bgmSource.loop = true;
        bgmSource.spatialBlend = 0f;
        bgmSource.outputAudioMixerGroup = bgmGroup;

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.playOnAwake = false;
        sfxSource.spatialBlend = 0f;
        sfxSource.outputAudioMixerGroup = sfxGroup;

        BgmVolume = Mathf.Clamp01(PlayerPrefs.GetFloat(BgmSaveKey, 1f));
        SfxVolume = Mathf.Clamp01(PlayerPrefs.GetFloat(SfxSaveKey, 1f));
    }

    /// <summary>
    /// 초기화 후 저장된 볼륨을 적용하고 현재 씬의 BGM을 재생
    /// </summary>
    private void Start()
    {
        if (instance != this)
            return;

        isInitialized = true;
        ApplyVolume(BgmParameter, BgmVolume);
        ApplyVolume(SfxParameter, SfxVolume);

        //이후 씬 전환에도 BGM을 바꾸기 위해 씬 로드 이벤트를 구독
        SceneManager.sceneLoaded += HandleSceneLoaded;
        PlaySceneBgm(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// 씬 로드가 끝나면 해당 씬의 BGM으로 변경
    /// </summary>
    private void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (mode == LoadSceneMode.Single)
            PlaySceneBgm(scene.name);
    }

    private void PlaySceneBgm(string SceneName)
    {
        if (SceneName == mainSceneName)
            PlayBgm(mainBgm);
        else if (SceneName == gameSceneName)
            PlayBgm(gameBgm);
    }

    private void PlayBgm(AudioClip clip)
    {
        if (bgmSource.clip == clip && bgmSource.isPlaying)
            return;

        bgmSource.Stop();
        bgmSource.clip = clip;

        if (clip != null)
            bgmSource.Play();
    }

    /// <summary>
    /// 전달받은 종류의 효과음을 한 번 재생
    /// 다른 효과음이 재생 중이여도 함께 재생
    /// </summary>
    public void PlaySfx(SfxType type)
    {
        AudioClip clip = type switch
        {
            SfxType.ButtonClick => buttonClick,
            SfxType.LevelUp => levelup,
            SfxType.WarriorAttack => warriorAttack,
            SfxType.ArcherAttack => archerAttack,
            SfxType.AssasinAttack => assasinAttack,
            SfxType.MagicianAttack => mageAttack,
            SfxType.EnemyDeath => enemyDeath,
            SfxType.EnemyHit => enemyHit,
            SfxType.MinerHit => minerHit,
            _ => null
        };

        if (clip == null)
            return;

        float volumeScale = type == SfxType.AssasinAttack ? 0.4f : 1f;
        sfxSource.PlayOneShot(clip, volumeScale);
    }

    /// <summary>
    /// 전달받은 0~1 값으로 BGM 볼륨을 변경하고 저장할 값을 갱신
    /// </summary>
    public void SetBgmVolume(float value)
    {
        BgmVolume = Mathf.Clamp01(value);
        PlayerPrefs.SetFloat(BgmSaveKey, BgmVolume);

        if (isInitialized)
            ApplyVolume(BgmParameter, BgmVolume);
    }

    /// <summary>
    /// 전달받은 0~1 값으로 SFX 볼륨을 변경하고 저장할 값을 갱신
    /// </summary>
    public void SetSfxVolume(float value)
    {
        SfxVolume = Mathf.Clamp01(value);
        PlayerPrefs.SetFloat(SfxSaveKey, SfxVolume);

        if (isInitialized)
            ApplyVolume(SfxParameter, SfxVolume);
    }

    private void ApplyVolume(string parameter, float volume)
    {
        if (audioMixer == null)
        {
            Debug.LogWarning("[AudioManager] : AudioManager에 AudioMixer를 연결해 주세요");
            return;
        }


        float decibels = volume <= 0.0001f ? -80f : Mathf.Log10(volume) * 20f;
        if (!audioMixer.SetFloat(parameter, decibels))
            Debug.LogWarning($"믹서의 노출된 파라미터 이름을 확인해 주세요 : {parameter}", this);
    }

    /// <summary>
    /// 변경한 설정을 디스크에 저장
    /// 설정 UI를 닫을 때 호출
    /// 슬라이더를 움직일 때마다 디스크에 쓰지 않도록 볼륨 변경과 분리
    /// </summary>
    public void SaveVolumeSettings()
    {
        PlayerPrefs.Save();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (instance == this && pauseStatus)
            SaveVolumeSettings();
    }

    private void OnApplicationQuit()
    {
        if (instance == this)
            SaveVolumeSettings();
    }

    private void OnDestroy()
    {
        if (instance != this)
            return;

        SceneManager.sceneLoaded -= HandleSceneLoaded;
        instance = null;
    }
}
