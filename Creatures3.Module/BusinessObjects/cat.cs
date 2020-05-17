using System.Linq;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
namespace Creatures3.Module.BusinessObjects
{
    [NavigationItem("Creatures")]
    [DefaultClassOptions]
    [DomainComponent]
   
    [ListViewFilter("TagA", "TagA <>''", "TagA", true, Index = 0)]
    [ListViewFilter("TagB", "TagB <>''", "TagB", true, Index = 1)]
    [ListViewFilter("TagC", "TagC <>''", "TagC", true, Index = 2)]
    public class Cat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TagA { get; set; }

        public string TagB { get; set; }

        public string TagC { get; set; }

       
     
    }
}