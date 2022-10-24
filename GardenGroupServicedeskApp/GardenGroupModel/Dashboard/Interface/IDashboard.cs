namespace GardenGroupModel
{
    public interface IDashboard
    {
        //NOTE: methods here↓

        //NOTE: readonly properties here↓

        //NOTE: add/remove observer here↓

        public void AddObserver(IWidgetListObserver observer);
        public void RemoveObserver(IWidgetListObserver observer);
    }
}
