namespace ClassLibraryResources
{
    public abstract class ViewModelBase : ObservableObject
    {
    }

    public abstract class ViewModelBase<TModel> : ViewModelBase
    {
        private TModel _model;

        public TModel Model
        {
            get => _model;
            set => SetProperty(ref _model, value, () => Model);
        }
    }
}