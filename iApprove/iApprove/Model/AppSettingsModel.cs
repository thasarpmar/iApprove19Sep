using iApprove.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace iApprove.Model
{
    public class AppSettingsModel
    {
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
        public bool IsVibrateEnabled { get; set; }
    }
}
