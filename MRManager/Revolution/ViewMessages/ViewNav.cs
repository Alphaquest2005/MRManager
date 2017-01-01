using System.ComponentModel.Composition;

namespace ViewMessages
{
    [Export]
    public class ViewNav
    {
        public ViewNav(string view)
        {
            View = view;
        }

        public string View { get; }

    }
}
