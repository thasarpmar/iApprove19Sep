using System;
using System.Collections;
using System.Collections.Generic;

namespace iApprove
{
    public class DateItem :IEnumerable
	{
		public DateItem()
		{
		}

		public string DateSpanName { get; set; }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
        //public T DateSpanName { get; set; }
    }
}
