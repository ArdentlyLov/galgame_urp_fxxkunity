using UnityEngine;

namespace TESTING
{


    public class Testing_Architect : MonoBehaviour
    {
        DialogueSystem ds;
        TextArchitect architect;

        public TextArchitect.BuildMethod bm = TextArchitect.BuildMethod.instent;
        
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
            architect.buildMethod = TextArchitect.BuildMethod.effects4;
            architect.speed = 0.5f;
        }
        void Update()
        {
            if (bm != architect.buildMethod)
            {
                architect.buildMethod = bm;
                architect.Stop();
            }

            if (Input.GetKeyDown(KeyCode.S))
                architect.Stop();
            
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
                    // architect.Build(longline);
                    architect.Build(lines[Random.Range(0, lines.Length)]);
                }
            }else if (Input.GetKeyDown(KeyCode.A))
            {
                architect.Append(lines[Random.Range(0, lines.Length)]);
                // architect.Append(longline);
            }
        }
    }
}
