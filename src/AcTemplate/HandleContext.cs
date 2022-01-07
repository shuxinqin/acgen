namespace AcTemplate
{
    public class HandleContext
    {
        public HandleContext()
        {

        }
        public CodeWriter Writer { get; set; }
        public List<NodeHander> Handlers { get; set; } = new List<NodeHander>();

        public NodeHander LastHandler { get { return this.Handlers.Last(); } }

        public void AddHandler(NodeHander nodeHandler)
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
