using UnityEngine;
using DIALOGUE;

namespace TESTING
{
    public class Testing_Architect : MonoBehaviour
    {
        [Header(" Settings ")] 
        [SerializeField] private float setPlaySpeed;
        DialogueSystem ds;
        TextArchitect architect;

        public TextArchitect.BuildMethod bm = TextArchitect.BuildMethod.instent;
        
        string[] lines = new string[5]
        {
            "this is a random line of dialogue",
            "i want to say something, come over here",
            "the world is a crazy place somethings",
            "dont lose hope,things will get better",
            "its a bird? its a plane ? no its super sheltie, i am so tried",
        };
        void Start()
        {
            ds = DialogueSystem.instance;
            architect = new TextArchitect(ds.dialogueContainer.dialogueText);
            architect.buildMethod = TextArchitect.BuildMethod.fade;
        }
        void Update()
        {
            //播放速度
            architect.speed = setPlaySpeed;

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
