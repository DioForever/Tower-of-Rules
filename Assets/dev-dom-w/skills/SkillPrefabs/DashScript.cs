using UnityEngine;
using Skills;
using System.Collections;

public class DashScript : MonoBehaviour
{
   

    private float travelDistance;
    private float travelSpeed;
    private bool isDashing = false;
    private SkillManager mySkillManager;
    private PlayerControl playercontrol;

    private void Start()
    {
        // kde v listu je dany spell
        MovementSkill movementSkill = mySkillManager.skills[4] as MovementSkill;

        travelDistance = movementSkill.TravelDistance;
        travelSpeed = movementSkill.TravelSpeed;
        StartCoroutine(Dash(travelDistance, travelSpeed));
    }
    private IEnumerator Dash(float distance, float speed)
    {
        isDashing = true;

        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + transform.right * distance; // Assuming dash direction is along the player's right

        while (elapsedTime < distance / speed)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / (distance / speed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the player reaches the exact end position
        transform.position = endPosition;

        isDashing = false;
    }
}