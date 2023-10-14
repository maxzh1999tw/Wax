namespace Wax.MVC.Models.Shared
{
    public struct VuetifySelectItem
    {
        public string Title { get; set; }
        public object Value { get; set; }

        public VuetifySelectItem(string title, object value)
        {
            Title = title;
            Value = value;
        }
    }
}
