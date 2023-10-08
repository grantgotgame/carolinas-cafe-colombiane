using UnityEditor;
using UnityEngine;

namespace cherrydev
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Nodes/Sentence Node", fileName = "New Sentence Node")]
    public class SentenceNode : Node
    {
        [SerializeField] private Sentence sentence;

        [Space(10)]
        public Node parentNode;

        public Node childNode;

        private const float lableFieldSpace = 40f;
        private const float textFieldWidth = 120f;
        public string letter = "";

        /// <summary>
        /// Returning sentence text
        /// </summary>
        /// <returns></returns>
        public string GetSentenceText()
        {
            return sentence.sentence;
        }

#if UNITY_EDITOR

        /// <summary>
        /// Draw Sentence Node method
        /// </summary>
        /// <param name="nodeStyle"></param>
        /// <param name="lableStyle"></param>
        public override void Draw(GUIStyle nodeStyle, GUIStyle lableStyle)
        {
            base.Draw(nodeStyle, lableStyle);

            
            nodeTitle = $"{count}{letter} {character.ToString()}";

            if (nodeTitle != prevTitle) {
                prevTitle = nodeTitle;
                name = nodeTitle;
                AssetDatabase.SaveAssets();
            }

            GUILayout.BeginArea(rect, nodeStyle);
            
            EditorGUILayout.LabelField(nodeTitle, lableStyle);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"Name ", GUILayout.Width(lableFieldSpace));
            character = (CharacterDictionarySO.CharacterID) EditorGUILayout.EnumPopup(character, GUILayout.Width(textFieldWidth));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"Text ", GUILayout.Width(lableFieldSpace));
            sentence.sentence = EditorGUILayout.TextField(sentence.sentence, GUILayout.Width(textFieldWidth));
            EditorGUILayout.EndHorizontal();

            /*
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"Sprite ", GUILayout.Width(lableFieldSpace));
            sentence.characterSprite = (Sprite)EditorGUILayout.ObjectField(sentence.characterSprite,
                typeof(Sprite), false, GUILayout.Width(textFieldWidth));
            EditorGUILayout.EndHorizontal();
            */

            GUILayout.EndArea();
        }

        /// <summary>
        /// Adding nodeToAdd Node to the childNode field
        /// </summary>
        /// <param name="nodeToAdd"></param>
        /// <returns></returns>
        public override bool AddToChildConnectedNode(Node nodeToAdd)
        {
            SentenceNode sentenceNodeToAdd;

            if (nodeToAdd.GetType() == typeof(SentenceNode))
            {
                nodeToAdd = (SentenceNode)nodeToAdd;

                if (nodeToAdd == this)
                {
                    return false;
                }
            }

            if (nodeToAdd.GetType() == typeof(SentenceNode))
            {
                sentenceNodeToAdd = (SentenceNode)nodeToAdd;

                if (sentenceNodeToAdd != null && sentenceNodeToAdd.childNode == this)
                {
                    return false;
                }
                sentenceNodeToAdd.SetLetter(letter);
            }

            childNode = nodeToAdd;
            childNode.count = count + 1;

            return true;
        }

        /// <summary>
        /// Adding nodeToAdd Node to the parentNode field
        /// </summary>
        /// <param name="nodeToAdd"></param>
        /// <returns></returns>
        public override bool AddToParentConnectedNode(Node nodeToAdd)
        {
            SentenceNode sentenceNodeToAdd;

            if (nodeToAdd.GetType() == typeof(SentenceNode))
            {
                nodeToAdd = (SentenceNode)nodeToAdd;

                if (nodeToAdd == this)
                {
                    return false;
                }
            }

            parentNode = nodeToAdd;

            if (nodeToAdd.GetType() == typeof(SentenceNode))
            {
                sentenceNodeToAdd = (SentenceNode)nodeToAdd;

                if (sentenceNodeToAdd.childNode == this)
                {
                    return true;
                }
                else
                {
                    parentNode = null;
                }
            }

            return true;
        }

        public void SetLetter(string letter) {
            this.letter = letter;
        }

#endif
    }
}