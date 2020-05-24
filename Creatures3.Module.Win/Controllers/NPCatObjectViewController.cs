using Creatures3.Module.BusinessObjects;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using System;
using System.Linq;
using System.Windows.Forms;
using ListView = DevExpress.ExpressApp.ListView;
namespace Creatures3.Module.Win.Controllers
{
    public partial class NPCatObjectViewController : ObjectViewController<ListView, NPCat>
    {
        public NPCatObjectViewController()
        {
            InitializeComponent();
            TargetObjectType = typeof(NPCat);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            var os = (NonPersistentObjectSpace)ObjectSpace;
            os.ObjectsGetting += os_ObjectsGetting;
         //   View.CollectionSource.CriteriaApplied += CollectionSource_CriteriaApplied;
            View.CreateCustomCurrentObjectDetailView += View_CreateCustomCurrentObjectDetailView;
            ObjectSpace.Refresh();
            Frame.GetController<FilterController>()?.Active.SetItemValue("Workaround T890466", false);
            Frame.GetController<FilterController>()?.Active.RemoveItem("Workaround T890466");
        }
        private void View_CreateCustomCurrentObjectDetailView(object sender,
            CreateCustomCurrentObjectDetailViewEventArgs e)
        {
             
            if (e.ListViewCurrentObject == null) return;
            if (!(e.ListViewCurrentObject is NPCat currentRec)) { throw new Exception("Unexpected"); }
            var os = Application.CreateObjectSpace(typeof(Cat));
        }
        protected override void OnDeactivated()
        {
            var os = (NonPersistentObjectSpace)ObjectSpace;
            os.ObjectsGetting -= os_ObjectsGetting;
          //  View.CollectionSource.CriteriaApplied -= CollectionSource_CriteriaApplied;
            base.OnDeactivated();
            View.CreateCustomCurrentObjectDetailView -= View_CreateCustomCurrentObjectDetailView;
             
        }
        private void os_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            var filterController = Frame.GetController<FilterController>();
            if (filterController.SetFilterAction.SelectedItem == null) return;
            var filterExpression = (string)filterController.SetFilterAction.SelectedItem.Data;
            //var filter = (BinaryOperator)CriteriaOperator.Parse(filterExpression);
            // var valueOperand = (OperandValue)filter.RightOperand;
            // var filterNum = (ToDoListFilterEnum)valueOperand.Value;
           
            var filterNum = (NPCatFilterEnum)Enum.Parse(typeof(NPCatFilterEnum), filterExpression);
            // todo  make use of the filter
            var collection = new DynamicCollection((IObjectSpace)sender, e.ObjectType, e.Criteria, e.Sorting, e.InTransaction);
            collection.FetchObjects += DynamicCollection_FetchObjects;
            e.Objects = collection;
            

        }

        private void DynamicCollection_FetchObjects(object sender, FetchObjectsEventArgs e)
        {
  
                e.Objects = NPCat.GetNPCats().ToList();
                e.ShapeData = true;
        }
        //private void CollectionSource_CriteriaApplied(object sender, EventArgs e)
        //{
        //    ObjectSpace.Refresh();
        //}
    }
}
