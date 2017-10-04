using Xamarin.Forms;

namespace iApprove.Model
{
    public class MenuEachItem
    {
        private string _menuText;

        public string MenuText
        {
            get { return _menuText; }
            set { _menuText = value; }
        }

        private ImageSource _imageName;

        public ImageSource ImageName
        {
            get { return _imageName; }
            set { _imageName = value; }
        }
    }
}
