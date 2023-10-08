using UnityEngine;

namespace cherrydev
{
    [System.Serializable]
    public class Sentence
    {
        [TextArea(3, 10)]
        public string sentence;
    }

    [System.Serializable]
    public class Answer {
        [TextArea(3, 10)]
        public string answer;
    }
}