using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TF.RunSafty.Entry
{
   public class Roles
    {
		#region Model
		private int _id;
		private string _rolesname;
		private string _rolespowers;
		/// <summary>
		/// 
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RolesName
		{
			set{ _rolesname=value;}
			get{return _rolesname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RolesPowers
		{
			set{ _rolespowers=value;}
			get{return _rolespowers;}
		}
		#endregion Model

    }
}
