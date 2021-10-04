using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Yarn.Unity;

public class StartDialogueNode : MonoBehaviour
{
    [SerializeField]
    private string nodeToRun;
    [SerializeField]
    private float interactionDistance;

    [SerializeField]
    private DialogueRunner dialogueRunner;
    [SerializeField]
    private Transform player;

    private void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, interactionDistance);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= interactionDistance && Input.GetKeyDown(KeyCode.E) && !dialogueRunner.IsDialogueRunning)
        {
            dialogueRunner.StartDialogue(nodeToRun);
        }
    }
}
