using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASP.NetMVCExample.Models;

namespace ASP.NetMVCExample.Models._SecurityOptions
{
    public class ProjectPrivages
    { 
        public bool Valid { get; set; }
        public bool CanViewProject { get; set; }
        public bool CanPostToProject { get; set; }
        public bool CanDeleteProjectPost { get; set; }
        public bool CanUpLoadToFile { get; set; }
        public bool CanDeleteProjectFile { get; set; }
        public bool CanEditProject { get; set; }
        public bool FullProjectAdmin { get; set; }

        public ProjectPrivages()
        {
            Valid = false;
            CanViewProject = false;
            CanPostToProject = false;
            CanDeleteProjectPost = false;
            CanUpLoadToFile = false;
            CanDeleteProjectFile = false;
            CanEditProject = false;
            FullProjectAdmin = false;
        }

        public void In(ValidateWithProjectViewPriv_Result MakeFrom)
        {
            Valid = MakeFrom.Valid.Value;
            CanViewProject = MakeFrom.CanViewProject.Value;
            CanPostToProject = MakeFrom.CanPostToProject.Value;
            CanDeleteProjectPost = MakeFrom.CanDeleteProjectPost.Value;
            CanUpLoadToFile = MakeFrom.CanDeleteProjectFile.Value;
            CanDeleteProjectFile = MakeFrom.CanDeleteProjectPost.Value;
            CanEditProject = MakeFrom.CanEditProject.Value;
            FullProjectAdmin = MakeFrom.FullProjectAdmin.Value;
        }
    }
}