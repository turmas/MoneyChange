﻿using MoneyChange.Core.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyChange.Core.Model {
	public class CalculateChangeResponse {
		public CalculateChangeResponse() {
			this.OperationReport = new List<Report>();
			this.ChangeDataList = new List<ChangeData>();
		}
		public List<ChangeData> ChangeDataList { get; set; }	
		public long TotalAmount { get; set; }
		public bool Success { get; set; }
		public List<Report> OperationReport { get; set; }
	}

	public class Report {
		public string Field { get; set; }
		public string Message { get; set; }
	}
}
