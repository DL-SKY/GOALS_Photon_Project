using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace GOALS.Test
{
    public class MyTestClass
    {
        public int index;
        public float range;
        public bool isDead;
        public List<float> points;
        public Dictionary<string, int> dic;
    }

    public class TestJSON : MonoBehaviour
    {
        private void Start()
        {
            var test = new MyTestClass()
            {
                index = 999,
                range = 2.5f,
                isDead = false,
                points = new List<float> { 0.5f, 1.5f, 3.5f, },
                dic = new Dictionary<string, int>
                {
                    { "one", 1 },
                    { "five", 5 },
                }
            };

            var json = JsonConvert.SerializeObject(test);
            Debug.LogError(json);

            test = JsonConvert.DeserializeObject<MyTestClass>("{\"index\":123,\"range\":0.5,\"isDead\":true," +
                "\"points\":[10.0,15.75],\"dic\":{\"one-2\":12,\"five-2\":52}}");
            json = JsonConvert.SerializeObject(test);
            Debug.LogError(json);
        }
    }
}
