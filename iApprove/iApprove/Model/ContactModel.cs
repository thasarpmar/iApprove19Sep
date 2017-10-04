using iApprove.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.ComponentModel;

namespace iApprove.Model
{
    public class ContactModel: INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this,
						new PropertyChangedEventArgs(propertyName));
			}
		}

		private int id;
		[PrimaryKey, AutoIncrement]
		public int ID
		{
			get { return id; }
			set { id = value; }
		}
		private string name;
        public string Name
		{
			get { return name; }
			set { name = value; 
            OnPropertyChanged("Name");}
		}
		private string address;
        public string Address
		{
			get { return address; }
			set { address = value; 
            OnPropertyChanged("Address");}
		}
		private string contactNo;
        public string ContactNo
		{
			get { return contactNo; }
			set { contactNo = value; 
            OnPropertyChanged("ContactNo");}
		}
		private ContactType contType;
        public ContactType ContType
		{
			get { return contType; }
			set { contType = value; 
            OnPropertyChanged("ContType");}
		}
		private string contactType;
		public string ContactType
		{
			get { return contactType; }
			set
			{
				contactType = value;
				OnPropertyChanged("ContactType");
			}
		}
		private string contactIcon;
        public string ContactIcon
		{
			get { return contactIcon; }
			set { contactIcon = value; 
            OnPropertyChanged("ContactIcon");}
		}
    }
}
