using UnityEngine;

namespace TESTING
{


    public class Testing_Architect : MonoBehaviour
    {
        DialogueSystem ds;
        TextArchitect architect;

        string[] lines = new string[5]
        {
            "this is a random line of dialogue",
            "i want to say something, come over here",
            "the world is a crazy place somethings",
            "dont lose hope,things will get better",
            "its a bird? its a plane ? no its super sheltie",
        };
        void Start()
        {
            ds = DialogueSystem.instance;
            architect = new TextArchitect(ds.dialogueContainer.dialogueText);
            architect.buildMethod = TextArchitect.BuildMethod.typewriter;
            architect.speed = 0.5f;
        }
        void Update()
        {
            string longline =
                "this is a very long tetxtthis is a very long tetxtthis is a very long tetxtthis is a very long tetxt";
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                if (architect.isBuilding)
                {
                    if (!architect.hurryUp)
                        architect.hurryUp = true;
                    else
                        architect.ForceComplete();
                }
                else
                {
                    architect.Build(longline);
                    // architect.Build(lines[Random.Range(0, lines.Length)]);
                }
            }else if (Input.GetKeyDown(KeyCode.A) || Input.GetMouseButtonDown(0))
            {
                architect.Append(longline);
            }
        }
    }
}
