using Observer.Items;

namespace Observer
{
    public interface IInvestor
    {
        void Update(Stock stock);
    }
}
