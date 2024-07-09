using System;

namespace WebCore.Domain
{
    public interface IAggregateRoot : IAggregateRoot<int>
    {

    }

    public interface IAggregateRoot<TId>
        where TId : struct
    {
        TId Id { get; set; }
    }

    public interface IAggregateRootDateTime
    {
        DateTime? CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
    }

    public interface IAggregateRootTracking : IAggregateRootDateTime
    {
        int? UpdatedBy { get; set; }
        int? CreatedBy { get; set; }
    }

    public class AggregateRoot
    {
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}