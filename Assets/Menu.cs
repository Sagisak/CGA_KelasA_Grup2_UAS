using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menupanel;
    public GameObject infopanel;
    public GameObject pausepanel;

    // Start is called before the first frame update
    void Start()
    {
        menupanel.SetActive(true);        // Menampilkan panel menu
        infopanel.SetActive(false);       // Menyembunyikan panel info
    }

    // Update is called once per frame
    void Update()
    {
        // Update tidak perlu diubah, karena tidak ada logika yang dilakukan dalam Update
    }

    // Fungsi untuk memulai scene
    public void StartButton(string scenename)
    {
        Debug.Log("Memulai scene: " + scenename); // Debug log untuk memeriksa nama scene
        SceneManager.LoadScene(scenename);  // Memuat scene berdasarkan nama
    }

    // Fungsi untuk menampilkan panel informasi
    public void InfoButton()
    {
        menupanel.SetActive(false);  // Menyembunyikan panel menu
        infopanel.SetActive(true);   // Menampilkan panel informasi
    }

    // Fungsi untuk kembali ke panel menu
    public void BackButton()
    {
        menupanel.SetActive(true);   // Menampilkan panel menu
        infopanel.SetActive(false);  // Menyembunyikan panel informasi
    }

    // Fungsi untuk keluar dari aplikasi
    public void QuitButton()
    {
        Debug.Log("Quit"); // Debug log untuk menampilkan pesan saat keluar
        Application.Quit(); // Keluar dari aplikasi

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // Untuk keluar saat di Unity Editor
        #endif
    }

    public void TogglePauseButton()
    {
        Time.timeScale = 0;
        pausepanel.SetActive(true);
    }
    
    public void ResumeButton()
    {
        Time.timeScale = 1;
        pausepanel.SetActive(false);
    }

    public void RestartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public void BackToMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("HalamanUtama");
    }
}
