using System;

namespace XunitOrderer {
    public class TestPriorityAttribute : Attribute {
        public TestPriorityAttribute(int priority) => Priority = priority;

        public int Priority { get; }
    }
}
