using UnityEngine;
using UnityEngine.UI;

public class KeyboardInputHandler : MonoBehaviour
{
    public Button exitButton; // Drag your exit button here in the Inspector

    void Update()
    {
        // Check if Ctrl + Shift + X is pressed
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.X))
        {
            // Call the button’s click handler
            exitButton.onClick.Invoke();
        }
    }
}
