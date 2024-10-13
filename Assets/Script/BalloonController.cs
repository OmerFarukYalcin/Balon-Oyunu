using System.Collections;
using UnityEngine;

namespace BalloonGame
{
    public class BalloonController : MonoBehaviour
    {
        // Audio clip for the balloon burst sound effect
        [SerializeField] private AudioClip balloonBlustSound;
        // Reference to the SpriteRenderer component for the balloon
        private SpriteRenderer spriteRenderer;
        // Reference to the BalloonMover script that controls the balloon's movement
        private BalloonMover balloonMover;
        // Reference to the GameObject that represents the blast animation or effect
        private GameObject blastGameObject;

        private void Awake()
        {
            // Get the first child object (blast animation/effect) and store it
            blastGameObject = transform.GetChild(0).gameObject;
            // Get the SpriteRenderer component attached to this balloon
            spriteRenderer = GetComponent<SpriteRenderer>();
            // Get the BalloonMover script attached to this balloon
            balloonMover = GetComponent<BalloonMover>();
        }

        private void Update()
        {
            // If the balloon goes beyond a certain height (4.5 on the Y axis), count it as a fugitive balloon
            if (this.gameObject.transform.position.y >= 4.5)
            {
                GameControl.instance.FugitiveBalloon();
                // Disable this script to stop further updates for this balloon
                enabled = false;
            }
        }

        private void OnMouseDown()
        {
            // If the balloon is clicked and is below the threshold height (4.5 on the Y axis)
            if (transform.position.y <= 4.5)
            {
                // Notify the game control that a balloon has been blasted
                GameControl.instance.BlastedBalloon();

                // Hide the balloon and trigger the blast effect
                HideGameObject();
            }

        }

        // Method to hide the balloon object with a delay (after the blast effect plays)
        public void HideGameObject()
        {
            // If the balloon is still active in the scene, start the coroutine to hide it
            if (gameObject.activeInHierarchy)
                StartCoroutine(HideBalloon());
        }

        // Coroutine to stop the balloon, show the blast effect, and hide the balloon
        private IEnumerator HideBalloon()
        {
            // Stop the balloon's movement
            balloonMover.StopTheBalloon();
            // Show the blast effect GameObject
            blastGameObject.SetActive(true);
            // Make the balloon sprite invisible by setting its alpha to 0
            spriteRenderer.color = new Color(1, 1, 1, 0);
            // Play the balloon burst sound effect at the balloon's position
            AudioSource.PlayClipAtPoint(balloonBlustSound, transform.position);
            // Wait for the duration of the blast animation (0.267 seconds)
            yield return new WaitForSeconds(0.267f);
            // Deactivate the balloon object after the blast animation finishes
            gameObject.SetActive(false);
        }
    }
}
