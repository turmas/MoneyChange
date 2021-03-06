﻿using MoneyChange.Core;
using MoneyChange.Core.DataContracts;
using MoneyChange.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoneyChange {
	public partial class MoneyChange : Form {
		public MoneyChange() {
			InitializeComponent();
		}

		private void UxBtnCalculate_Click(object sender, EventArgs e) {
			Calculate();
		}

		private void Calculate() {
			var moneyManager = new MoneyChangeManager();
			try {
				long paidAmountInCents = long.Parse(this.UxTxtPaidAmount.Text);
				long productAmountInCents = long.Parse(this.UxTxtProductAmount.Text);

				CalculateChangeRequest request = new CalculateChangeRequest() {
					PaidAmountInCents = paidAmountInCents,
					ProductAmountInCents = productAmountInCents 
				};

				CalculateChangeResponse response = moneyManager.CalculateChange(request);

				if (response.Success == false) {

					StringBuilder msgs = new StringBuilder();

					foreach (Report report in response.OperationReport) {
						msgs.AppendFormat("Field: {0}{2}Message: {1}{2}{2}", report.Field, report.Message, Environment.NewLine);
					}
					MessageBox.Show(msgs.ToString(),"MoneyChange",MessageBoxButtons.OK,MessageBoxIcon.Warning);

					return;
				}
				
				StringBuilder builder = new StringBuilder();
				builder.AppendFormat("Troco total: {0}\r\n", response.TotalAmount);
				foreach (ChangeData changeData in response.ChangeDataList)
				{
					builder.AppendFormat("Troco em {0}\r\n", changeData.Name);
					foreach (KeyValuePair<long, long> kv in changeData.ChangeDictionary) {
						builder.AppendFormat("{0} : {1}\r\n", kv.Key, kv.Value);		
					}					
				}

				this.UxTxtResult.Text = builder.ToString();
			}
			catch (Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}
	}
}
