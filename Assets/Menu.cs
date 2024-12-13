using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menupanel;
    public GameObject infopanel;

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
        SceneManager.LoadScene(scenename);  // Memuat scene berdasarkan nama
    }

    // Fungsi untuk menampilkan panel informasi
    public void InfoButton()
    {
        menupanel.SetActive(false);  // Menyembunyikan panel menu
        infopanel.SetActive(true);   // Menampilkan panel informasi
    }

    public void BackButton()
    {
        menupanel.SetActive(true);   // Menampilkan panel menu
        infopanel.SetActive(false);  // Menyembunyikan panel informasi
    }

    // Fungsi untuk keluar dari aplikasi

    public void QuitButton()
    {
        Application.Quit(); // Keluar dari aplikasi
    }
}
