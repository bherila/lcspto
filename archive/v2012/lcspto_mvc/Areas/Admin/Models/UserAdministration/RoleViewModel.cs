using System.Collections.Generic;
using System.Web.Security;

namespace ElixCms.Areas.Admin.Models.UserAdministration
{
	public class RoleViewModel
	{
		public string Role { get; set; }
		public IEnumerable<MembershipUser> Users { get; set; }
	}
}