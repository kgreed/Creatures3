using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
namespace Creatures3.Module.BusinessObjects
{
    [NavigationItem("Creatures")]
    [XafDisplayName("OPCats")]
    [DefaultClassOptions]
    [DomainComponent]
    public class OPCat : NPCat
    {

        public OPCat()
        {
            TagC = "OP";
        }


    }
}