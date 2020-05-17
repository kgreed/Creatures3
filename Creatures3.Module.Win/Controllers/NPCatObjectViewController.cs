using Creatures3.Module.BusinessObjects;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using System;
using System.Linq;

namespace Creatures3.Module.Win.Controllers
{
    public partial class NPCatObjectViewController : ObjectViewController<ListView, NPCat>
    {
        public NPCatObjectViewController()
        {
            InitializeComponent();
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            var os = (NonPersistentObjectSpace)ObjectSpace;
            os.ObjectsGetting += os_ObjectsGetting;
            View.CollectionSource.CriteriaApplied += CollectionSource_CriteriaApplied;
            View.CreateCustomCurrentObjectDetailView += View_CreateCustomCurrentObjectDetailView;
            ObjectSpace.Refresh();
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
            View.CollectionSource.CriteriaApplied -= CollectionSource_CriteriaApplied;
            base.OnDeactivated();
            View.CreateCustomCurrentObjectDetailView -= View_CreateCustomCurrentObjectDetailView;
            // ObjectSpaceFunctions.DisposeAdditionalPersistentObjectSpace(Application, os);
        }
        private void os_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            var filterController = Frame.GetController<FilterController>();
            if (filterController.SetFilterAction.SelectedItem == null) return;
            var filterExpression = (string)filterController.SetFilterAction.SelectedItem.Data;
            var filter = (BinaryOperator)CriteriaOperator.Parse(filterExpression);
            var valueOperand = (OperandValue)filter.RightOperand;
            // var filterNum = (ToDoListFilterEnum)valueOperand.Value;
            // todo  make use of the filter
            var objects = NPCat.GetNPCats().ToList();
            e.Objects = objects;

        }
        private void CollectionSource_CriteriaApplied(object sender, EventArgs e)
        {
            ObjectSpace.Refresh();
        }
    }
}
