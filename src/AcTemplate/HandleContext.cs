namespace AcTemplate
{
    public class HandleContext
    {
        public HandleContext()
        {

        }
        public CodeWriter Writer { get; set; }
        public List<NodeHandler> Handlers { get; set; } = new List<NodeHandler>();

        public NodeHandler LastHandler { get { return this.Handlers.Last(); } }

        public void AddHandler(NodeHandler nodeHandler)
        {
            this.Handlers.Add(nodeHandler);
        }
        public void RemoveLastHandler()
        {
            this.Handlers.RemoveAt(this.Handlers.Count - 1);
        }
        public void SuspendLastHandler()
        {
            this.LastHandler.Suspend();
        }
    }
}
