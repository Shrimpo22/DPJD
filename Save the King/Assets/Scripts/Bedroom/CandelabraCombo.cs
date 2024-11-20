using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandelabraCombo : MonoBehaviour
{
    // Start is called before the first frame update
    public bool[] targetCombination = { true, false, true };
    public GameObject candle1;
    public GameObject candle2;
    public GameObject candle3;
    public GameObject drawer;
    public AudioSource audioSource;
    public CamInteractable cam;

    public bool solved = false;

    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    public void CheckCombo(){
        bool[] currentCombination = {
            candle1.GetComponent<CandelabraLights>().lit,
            candle2.GetComponent<CandelabraLights>().lit,
            candle3.GetComponent<CandelabraLights>().lit
        };

        if (AreCombinationsEqual(currentCombination, targetCombination))
        {
            Debug.Log("The objects are in the target combination: " + targetCombination[0] + ", " + targetCombination[1] + ", " + targetCombination[2]);
            drawer.transform.position = drawer.transform.position + drawer.transform.forward * 0.3f;
            candle1.GetComponent<CandelabraLights>().solved = true;
            candle2.GetComponent<CandelabraLights>().solved = true;
            candle3.GetComponent<CandelabraLights>().solved = true;
            StartCoroutine(PlaySoundAndExit());   
        }

    }

    bool AreCombinationsEqual(bool[] current, bool[] target)
    {
        if (current.Length != target.Length)
            return false;

        for (int i = 0; i < current.Length; i++)
        {
            if (current[i] != target[i])
                return false;
        }

        return true;
    }

    IEnumerator PlaySoundAndExit(){
        Debug.Log("Opening Drawer");

        if (audioSource.clip != null)
        {
            audioSource.Play();

            yield return new WaitForSeconds(audioSource.clip.length + 1f);

            cam.ExitCam();
        }
        else
        {
            Debug.LogWarning("No audio clip assigned to AudioSource.");
        }

        Debug.Log("Finished");
    }
}
