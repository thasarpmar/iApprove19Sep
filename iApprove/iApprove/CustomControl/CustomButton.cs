using iApprove.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace iApprove.CustomControl
{
    public class CustomButton : Button
    {
        private ButtonTypeName buttonType = ButtonTypeName.POSITIVE;
        public ButtonTypeName ButtonType
        {
            get
            {
                return this.buttonType;
            }
            set
            {
                this.buttonType = value;
            }
        }

        public CustomButton()
        {

        }
    }

    public enum ButtonTypeName
    {
        POSITIVE=0,
        NEGATIVE=1,
        CONFIRM=2,
        DIAL=3,
        EMAIL=4
    }
}
