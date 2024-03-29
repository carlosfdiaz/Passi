using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Passi.Pages.Models;

namespace Passi.Pages
{
    public class GroupsInfoModel : PageModel
    {
        public string Username { get; set; }
        public List<string> ADGroupsList { get; set; }
        public List<string> GroupMembers { get; set; }
        public ADGroup GroupDetails { get; set; }
        public List<string> MemberTypes { get; set; }
        public string SearchQuery { get; set; }
        public List<string> ADActiveUserList { get; set; }

        public void OnGet()
        {
            DirectoryMethods dirMethods = new DirectoryMethods();
            Username = HttpContext.Session.GetString("Username");
            if (RouteData.Values["searchQuery"] != null)
            {
                SearchQuery = RouteData.Values["searchQuery"].ToString();
                GroupDetails = dirMethods.GetADGroupDetails(SearchQuery);
                GroupMembers = GroupDetails.GroupObjectsNames;
                MemberTypes = new List<string>();
                foreach (ADGroupObject m in GroupDetails.GroupObjects)
                {

                    MemberTypes.Add(m.ObjectTypeString);

                }
                string Domain = HttpContext.Session.GetString("Domain");
                ADActiveUserList = new DirectoryMethods().GetADUserList(Domain);
            }
            else
            {
                Console.WriteLine("string was empty or null");
            }
        }

    }
}