using System.Collections.Generic;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.XtraCharts.Design;
using Microsoft.CodeAnalysis.CSharp.Syntax;
namespace Creatures3.Module.BusinessObjects
{
    [NavigationItem("Creatures")]
    [XafDisplayName("NPCats")]
    [DefaultClassOptions]
    [DomainComponent]
    [ListViewFilter("TagA", "true", "TagA", true, Index = 0)]
    [ListViewFilter("TagB", "true", "TagB", true, Index = 1)]
    [ListViewFilter("TagC", "true", "TagC", true, Index = 2)]
    public class NPCat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TagA { get; set; }

        public string TagB { get; set; }

        public string TagC { get; set; }

        public static NPCat[] GetNPCats()
        {
            var db = DataHelper.MakeConnect();
            var cats = db.Cats;
            var nps = new List<NPCat>();
            foreach (var n in cats)
            {
                var npcat = new NPCat {Name = n.Name, TagA = n.TagA, Id = n.Id, TagB = n.TagB,  TagC = n.TagC };
                nps.Add(npcat);
            }
            return nps.ToArray();
        }
    }
}