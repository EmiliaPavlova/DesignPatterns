namespace Composite.Elements
{
    /// <summary>
    /// The 'Component' Treenode
    /// </summary>
    public abstract class DrawingElement
    {
        protected string name;

        public DrawingElement(string name)
        {
            this.name = name;
        }

        public abstract void Add(DrawingElement d);
        public abstract void Remove(DrawingElement d);
        public abstract void Display(int indent);
    }
}
