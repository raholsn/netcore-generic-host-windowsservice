using System;

namespace Domain
{
    public class CleanUp : IEntity
    {
        public CleanUp(Guid id, DateTime executionTime)
        {
            this.Id = id;
            this.ExecutionTime = executionTime;
        }

        public Guid Id { get; set; }
        public DateTime ExecutionTime { get; private set; }
    }
}
