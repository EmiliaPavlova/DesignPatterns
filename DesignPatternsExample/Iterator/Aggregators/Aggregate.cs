using IteratorPattern.Iterators;

namespace IteratorPattern.Aggregators
{
    public abstract class Aggregate
    {
        public abstract Iterator CreateIterator();
    }
}
