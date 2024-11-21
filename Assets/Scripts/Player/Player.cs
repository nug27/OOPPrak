using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; } // Implementasi Singleton
    public PlayerMovement playerMovement;
    public Animator animator;

    void Awake()
    {
        // Membuat Singleton agar hanya ada satu instance Player di dalam game
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this); // Agar instance tetap ada di seluruh scene
        }
        else
        {
            UnityEditor.SceneVisibilityManager.instance.Show(gameObject, false);// Hapus instance baru jika sudah ada
            return;
        }
    }

    void Start()
    {
        // Mengambil informasi dari PlayerMovement dan menyimpannya di playerMovement
        playerMovement = GetComponent<PlayerMovement>();

        // Mengambil informasi dari Animator dari EngineEffect dan menyimpannya di animator
        GameObject engineEffectTransform = GameObject.Find("EngineEffect");
        if (engineEffectTransform != null)
        {
            animator = engineEffectTransform.GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator tidak ditemukan di dalam EngineEffect.");
            }
        }
        else
        {
            Debug.LogError("EngineEffect tidak ditemukan di dalam Player.");
        }
    }

    void FixedUpdate()
    {
        // Memanggil method Move dari PlayerMovement untuk menggerakkan Player
        playerMovement.Move();
    }

    void LateUpdate()
    {
        // Periksa jika animator tidak null
        if (animator != null)
        {
            // Mengatur nilai Bool "IsMoving" berdasarkan return dari IsMoving pada PlayerMovement
            animator.SetBool("IsMoving", playerMovement.IsMoving());
        }
    }
}