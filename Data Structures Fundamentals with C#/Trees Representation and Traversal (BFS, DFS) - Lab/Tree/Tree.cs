namespace Tree
{
    using System;
    using System.Collections.Generic;

    public class Tree<T> : IAbstractTree<T>
    {
        private T Value;
        private Tree<T> parent;
        private readonly List<Tree<T>> children;

        public Tree(T value)
        {
            this.Value = value;
            this.children = new List<Tree<T>>();
        }

        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {
            foreach (var ch in children)
            {
                ch.parent = this;
                this.children.Add(ch);
            }
        }

        public void AddChild(T parentKey, Tree<T> child)
        {
            var node = this.FindNode(parentKey);
            if (node != null)
            { 
                node.children.Add(child);
                child.parent = node;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        private Tree<T> FindNode(T parentKey)
        {
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var subtree = queue.Dequeue();


                if (subtree.Value.Equals(parentKey))
                {
                    return subtree;
                }

                foreach (var ch in subtree.children)
                {
                    queue.Enqueue(ch);
                }
            }

            return null;
        }

        public IEnumerable<T> OrderBfs()
        {
            var queue = new Queue<Tree<T>>();
            var result = new List<T>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            { 
                var subtree = queue.Dequeue();
                result.Add(subtree.Value);

                foreach (var ch in subtree.children)
                {
                    queue.Enqueue(ch);
                }
            }

            return result;
        }

        public IEnumerable<T> OrderDfs()
        {
            var stack = new Stack<Tree<T>>();
            var result = new Stack<T>();
            stack.Push(this);

            while (stack.Count > 0)
            {
                var node = stack.Pop();

                foreach (var ch in node.children)
                {
                    stack.Push(ch);
                }

                result.Push(node.Value);
            }

            return result;
        }

        public void RemoveNode(T nodeKey)
        {
            throw new NotImplementedException();
        }

        public void Swap(T firstKey, T secondKey)
        {
            throw new NotImplementedException();
        }
    }
}
