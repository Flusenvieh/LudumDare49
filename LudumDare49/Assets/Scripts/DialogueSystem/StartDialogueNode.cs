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
    private DestroyMode stuffToDestroy;

    [SerializeField]
    private DialogueRunner dialogueRunner;
    [SerializeField]
    private Transform player;

    private void Start()
    {
        Assert.IsTrue(dialogueRunner.NodeExists(nodeToRun), "Following node could not be found: " + nodeToRun);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= interactionDistance && Input.GetKeyDown(KeyCode.Space))
        {
            dialogueRunner.StartDialogue(nodeToRun);
            dialogueRunner.onDialogueComplete.AddListener(DestroyEntity);
        }
    }

    void DestroyEntity()
    {
        dialogueRunner.onDialogueComplete.RemoveListener(DestroyEntity);
        switch(stuffToDestroy)
        {
            case DestroyMode.nothing: break;
            case DestroyMode.scriptOnly: Destroy(this); break;
            case DestroyMode.fullGameObject: Destroy(this.gameObject); break;
        }
    }

    enum DestroyMode
    {
        nothing,
        scriptOnly,
        fullGameObject
    }
}
