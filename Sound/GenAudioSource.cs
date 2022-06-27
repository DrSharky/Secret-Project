//using UnityEngine;

//public class GenAudioSource : MonoBehaviour
//{
//    [SerializeField]
//    private RandomAudioClipPlayer clipPlayer;
//    private AudioSource source;
//    private bool done = false;

//    void Awake()
//    {
//        source = GetComponent<AudioSource>();
//    }

//    void Start()
//    {
//        clipPlayer.Play(source);
//    }

//    void Update()
//    {
//        if (source != null && !source.isPlaying)
//            Destroy(gameObject);
//    }
//}